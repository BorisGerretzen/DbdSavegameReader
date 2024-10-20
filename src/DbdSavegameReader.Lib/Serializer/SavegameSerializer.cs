using System.Data;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using DbdSavegameReader.Lib.Models;

namespace DbdSavegameReader.Lib.Serializer;

public static class SavegameSerializer
{
    /// <summary>
    /// Got this key from a forum post.
    /// https://www.unknowncheats.me/forum/other-fps-games/226728-deadbydaylight-savegame-format.html
    /// </summary>
    private static readonly byte[] EncryptionKey = Crypto.Base64Decode("NUJDQzJENkE5NUQ0REYwNEEwMDU1MDRFNTlBOUIzNkU=");
    
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    private const string GlobalMagicInner = "DbdDAQEB";
    private const string GlobalMagicOuter = "DbdDAgAC";
    
    /// <summary>
    /// Decodes a savegame file and returns the deserialized object.
    /// </summary>
    /// <param name="fileContent">Content read from save file, should look like a base64 string.</param>
    /// <returns>Deserialized save file.</returns>
    /// <exception cref="SerializationException">If json cannot be deserialized.</exception>
    /// <exception cref="DataException">If magic bytes are not found.</exception>
    public static DbdSavegame Read(string fileContent)
    {
        var json = ReadJson(fileContent);
        return DeserializeJson(json);
    }
    
    /// <summary>
    /// Reads a savegame file and returns the JSON string contained within.
    /// </summary>
    /// <param name="fileContent">Content read from save file, should look like a base64 string.</param>
    /// <returns>Decoded save file.</returns>
    /// <exception cref="DataException">If magic bytes are not found.</exception>
    public static string ReadJson(string fileContent)
    {
        // Find outer magic byts
        var content = fileContent.AsSpan();
        var magicOuter = content.Slice(4, 8).ToString();
        if (magicOuter != GlobalMagicOuter) throw new DataException($"Outer magic bytes not found, expected '{GlobalMagicOuter}' but found '{magicOuter}'.");

        // Decrypt payload
        var payload = content.Slice(0xC, content.Length - 0xC - 1);
        var rawPayload = Crypto.Base64Decode(payload);
        var decryptedPayload = Crypto.Decrypt(rawPayload, EncryptionKey).AsSpan();
        
        // Increment each byte by 1, some kind of obfuscation
        for(var i = 0; i < decryptedPayload.Length; i++) decryptedPayload[i] += 1;

        // Find inner magic bytes
        var magicInner = Encoding.UTF8.GetString(decryptedPayload[..8]);
        if (magicInner != GlobalMagicInner) throw new DataException($"Inner magic bytes not found, expected '{GlobalMagicInner}' but found '{magicInner}'.");
        
        // Extract inner payload
        var payloadInner = Encoding.UTF8.GetString(decryptedPayload[8..^4]);
        var rawInner = Crypto.Base64Decode(payloadInner);
        var gzippedProfile = rawInner.Skip(4).ToArray();
        
        return Compression.Decompress(gzippedProfile);
    }
    
    /// <summary>
    /// Deserializes a JSON string into a DbdSavegame object.
    /// </summary>
    /// <param name="json">JSON string to deserialize.</param>
    /// <returns>Deserialized save file.</returns>
    /// <exception cref="SerializationException">If json cannot be deserialized.</exception>
    public static DbdSavegame DeserializeJson(string json)
    {
        return JsonSerializer.Deserialize<DbdSavegame>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new SerializationException("Failed to deserialize JSON.");
    }
}