namespace Containers;

/// <summary>
/// Class of container ints in byte representation.
/// </summary>
public class EncodeByteContainer
{
    private const int BitsInByte = 8;

    private readonly List<byte> container = [];

    private byte currentByte = 0;

    private int currentByteSize = 0;

    /// <summary>
    /// Gets or sets symbol bit size.
    /// </summary>
    public int SymbolBitSize { get; set; } = 8;

    /// <summary>
    /// Gets or sets max number of different symbol.
    /// </summary>
    public int MaxSymbols { get; set; } = 256;

    /// <summary>
    /// Add int number in byte container.
    /// </summary>
    /// <param name="number">Int number.</param>
    public void Add(int number)
    {
        var representation = IntToByteRepresentation(number);

        foreach (var bit in representation)
        {
            currentByte = (byte)((currentByte << 1) + bit);

            ++currentByteSize;

            if (currentByteSize == BitsInByte)
            {
                AddByteToContainer();
            }
        }
    }

    /// <summary>
    /// Transform container into byte array.
    /// </summary>
    /// <returns>Byte array.</returns>
    public byte[] GetByteArray()
    {
        PrepareContainerToTransformToArray();
        return [.. container];
    }

    private void PrepareContainerToTransformToArray()
    {
        currentByte <<= BitsInByte - currentByteSize;

        AddByteToContainer();
    }

    private void AddByteToContainer()
    {
        container.Add(currentByte);

        currentByteSize = 0;
        currentByte = 0;
    }

    private byte[] IntToByteRepresentation(int number)
    {
        var representation = new byte[SymbolBitSize];

        for (var i = SymbolBitSize - 1; i >= 0; --i)
        {
            representation[i] = (byte)(number % 2);
            number >>= 1;
        }

        return representation;
    }
}