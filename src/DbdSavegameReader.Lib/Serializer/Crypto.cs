using System.Security.Cryptography;

namespace DbdSavegameReader.Lib.Serializer;

public static class Crypto
{
    /// <summary>
    ///     Decodes a base64 encoded string into a byte array.
    /// </summary>
    /// <param name="data">The base64 encoded string to decode.</param>
    /// <returns>Decoded bytes.</returns>
    /// <exception cref="FormatException">Thrown when the input data is not a valid base64 string.</exception>
    public static byte[] Base64Decode(ReadOnlySpan<char> data)
    {
        var outputLength = data.Length / 4 * 3;
        var decodedData = new byte[outputLength];
        var bytesWritten = Convert.TryFromBase64Chars(data, decodedData, out var decodedLength);

        if (!bytesWritten) throw new FormatException("Invalid base64 data");
        return decodedData[..decodedLength];
    }

    /// <summary>
    ///     Decrypts the given data using AES.
    /// </summary>
    /// <param name="data">The data to decrypt.</param>
    /// <param name="key">Key to use for decryption, is truncated to 32 bytes.</param>
    /// <returns>Decrypted data.</returns>
    public static byte[] Decrypt(byte[] data, byte[] key)
    {
        key = key[..32];

        using var aes = Aes.Create();
        aes.Key = key;
        aes.BlockSize = 128;
        aes.Padding = PaddingMode.None;
        aes.Mode = CipherMode.ECB;

        using var decryptor = aes.CreateDecryptor();
        return decryptor.TransformFinalBlock(data, 0, data.Length);
    }
}