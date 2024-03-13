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
    public static double Encode(string filePath, bool withBWT = true)
    {
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

        return (double)firstFileByteSize / (double)secondFileByteSize;
    }

    /// <summary>
    /// Function of decoding file.
    /// </summary>
    /// <param name="filePath">Relative path to decoding file.</param>
    /// <param name="wasBWT">Was used bwt transform into encoding or not.</param>
    /// <exception cref="ArgumentException">Decoding file should be correct.</exception>
    public static void Decode(string filePath, bool wasBWT = false)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("No file with this path exists", nameof(filePath));
        }

        ArgumentException.ThrowIfNullOrEmpty(nameof(filePath));

        var newFilePath = filePath.Substring(0, filePath.LastIndexOf('.'));

        if (newFilePath == string.Empty)
        {
            throw new ArgumentException("File name comprises only of extension", nameof(filePath));
        }

        var code = File.ReadAllBytes(filePath);

        code = LZWDecoder.Decode(code);

        byte[] decodeBWTBytes = code;

        if (wasBWT)
        {
            var indexInBytes = new byte[4];

            for (var i = 0; i < 4; ++i)
            {
                indexInBytes[i] = code[i + code.Length - 4];
            }

            Array.Resize(ref code, code.Length - 4);
            var index = BitConverter.ToInt32(indexInBytes);

            decodeBWTBytes = BWTTransformer.ReverseTransform(code, index);
        }

        File.WriteAllBytes(newFilePath, decodeBWTBytes);
    }
}