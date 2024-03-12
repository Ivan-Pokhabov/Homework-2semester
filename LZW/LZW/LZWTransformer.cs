namespace LZW;

public static class LZWTransformer
{
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

    public static void Decode(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("No file with this path exists", nameof(filePath));
        }

        var newFilePath = filePath.Substring(0, filePath.LastIndexOf('.'));
        if (newFilePath == string.Empty)
        {
            throw new ArgumentException("File name comprises only of extension", nameof(filePath));
        }

        File.Create(newFilePath).Close();

        var code = File.ReadAllBytes(filePath);

        try
        {
           code = LZWDecoder.Decode(code);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Trying to decompress empty file", nameof(filePath));
        }

        File.WriteAllBytes(newFilePath, code);
    }
}