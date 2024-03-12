namespace Containers;

public class EncodeByteContainer
{
    public int SymbolBitSize { set; get; } = 8;

    public int MaxSymbols  { set; get; } = 256;

    private List<byte> container = new ();

    private byte currentByte = 0;

    private const int BITS_IN_BYTE = 8;

    private int currentByteSize = 0;

    public void Add(int number)
    {
        var representation = IntToByteRepresentation(number);

        foreach (var bit in representation)
        {
            currentByte = (byte)((currentByte << 1) + bit);

            ++currentByteSize;
            
            if (currentByteSize == BITS_IN_BYTE)
            {
                AddByteToContainer();
            }
        }
    }

    public byte[] GetByteArray()
    {
        PrepareContainerToTransformToArray();
        return container.ToArray();
    }
        

    private void PrepareContainerToTransformToArray()
    {
        currentByte <<=  BITS_IN_BYTE - currentByteSize;

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