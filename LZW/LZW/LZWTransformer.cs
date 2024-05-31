using BWT;

namespace LZW;

/// <summary>
/// Class that encode or decode file by LZW.
/// </summary>
public static class LZWTransformer
{
    /// <summary>
    /// Function of encoding file with or without BWT.
    /// </summary>
    /// <param name="filePath">Relative path to file.</param>
    /// <param name="withBWT">Should we use bwt or not.</param>
    /// <returns>Compress ratio.</returns>
    /// <exception cref="ArgumentException">File should exists and can't be null or empty.</exception>
    public static double Encode(string filePath, bool withBWT = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(filePath));

        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File with this path doesn't exist", nameof(filePath));
        }

        var fileByteContent = File.ReadAllBytes(filePath);
        ArgumentException.ThrowIfNullOrEmpty(nameof(fileByteContent));

        if (withBWT)
        {
            var (result, index) = BWTTransformer.Transform(fileByteContent);

            var indexInBytes = BitConverter.GetBytes(index);

            fileByteContent = new byte[result.Length + indexInBytes.Length];

            result.CopyTo(fileByteContent, 0);
            indexInBytes.CopyTo(fileByteContent, result.Length);
        }

        var newFilePath = filePath + ".zipped";

        File.WriteAllBytes(newFilePath, LZWEncoder.Encode(fileByteContent));

        var firstFileByteSize = new FileInfo(filePath).Length;
        var secondFileByteSize = new FileInfo(newFilePath).Length;

        return (double)firstFileByteSize / secondFileByteSize;
    }

    /// <summary>
    /// Function of decoding file.
    /// </summary>
    /// <param name="filePath">Relative path to decoding file.</param>
    /// <param name="wasBWT">Was used bwt transform into encoding or not.</param>
    /// <exception cref="ArgumentException">Decoding file should be correct.</exception>
    public static void Decode(string filePath, bool wasBWT = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(filePath));

        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File with this path doesn't exist", nameof(filePath));
        }

        var newFilePath = filePath[..filePath.LastIndexOf('.')];

        var code = File.ReadAllBytes(filePath);

        code = LZWDecoder.Decode(code);

        var decodeBWTBytes = code;

        if (wasBWT)
        {
            var bwtCodeLength = 4;
            var indexInBytes = new byte[bwtCodeLength];

            for (var i = 0; i < bwtCodeLength; ++i)
            {
                indexInBytes[i] = code[i + code.Length - bwtCodeLength];
            }

            Array.Resize(ref code, code.Length - bwtCodeLength);
            var index = BitConverter.ToInt32(indexInBytes);

            decodeBWTBytes = BWTTransformer.ReverseTransform(code, index);
        }

        File.WriteAllBytes(newFilePath, decodeBWTBytes);
    }
}