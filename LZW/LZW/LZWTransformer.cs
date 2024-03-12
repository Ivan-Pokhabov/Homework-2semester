namespace LZW;

/// <summary>
/// Class that encode or decode file by LZW.
/// </summary>
public static class LZWTransformer
{
    /// <summary>
    /// Function of encoding file.
    /// </summary>
    /// <param name="filePath">Relative path to file.</param>
    /// <returns>Compress ratio.</returns>
    /// <exception cref="ArgumentException">File should exists and can't be null or empty.</exception>
    public static double Encode(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File with this path doesn't exist", nameof(filePath));
        }

        var fileByteContent = File.ReadAllBytes(filePath);
        ArgumentException.ThrowIfNullOrEmpty(nameof(fileByteContent));

        var newFilePath = filePath + ".zipped";

        File.WriteAllBytes(newFilePath, LZWEncoder.Encode(fileByteContent.ToArray()));

        var firstFileByteSize = new FileInfo(filePath).Length;
        var secondFileByteSize = new FileInfo(newFilePath).Length;

        return (double)firstFileByteSize / (double)secondFileByteSize;
    }

    /// <summary>
    /// Function of decoding file.
    /// </summary>
    /// <param name="filePath">Relative path to decoding dile.</param>
    /// <exception cref="ArgumentException">Decoding file should be correct.</exception>
    public static void Decode(string filePath)
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

        File.Create(newFilePath).Close();

        var code = File.ReadAllBytes(filePath);

        code = LZWDecoder.Decode(code);

        File.WriteAllBytes(newFilePath, code);
    }
}