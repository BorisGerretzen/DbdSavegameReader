using System.IO.Compression;
using System.Text;

namespace DbdSavegameReader.Lib.Serializer;

public static class Compression
{
    /// <summary>
    /// Decompresses the given data using the zlib algorithm.
    /// </summary>
    /// <param name="compressedData">The compressed data to decompress.</param>
    /// <returns>Decompressed data as a string.</returns>
    public static string Decompress(byte[] compressedData)
    {
        using var compressedStream = new MemoryStream(compressedData);
        using var decompressedStream = new MemoryStream();
        using (var deflateStream = new ZLibStream(compressedStream, CompressionMode.Decompress))
        {
            deflateStream.CopyTo(decompressedStream);
        }

        var decompressedBytes = decompressedStream.ToArray();
        
        return Encoding.Unicode.GetString(decompressedBytes);
    }
}