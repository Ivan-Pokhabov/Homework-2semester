namespace Containers;

public class DecodeIntContainer
{
    public int SymbolBitSize { set; get; } = 8;

    public int MaxSymbols { set; get; } = 256;

    private List<int> container = new ();

    private int currentInt = 0;

    private const int BITS_IN_BYTE = 8;

    private int currentIntSize = 0;

    public bool Add(byte codeByte)
    {
        var representation = ByteToBitRepresentation(codeByte);

        var newNumber = false;

        foreach (var bit in representation)
        {
            currentInt = (currentInt << 1) + bit;

            ++currentIntSize;
            
            if (currentIntSize == SymbolBitSize)
            {
                AddIntToContainer();
                newNumber = true;
            }
        }

        return newNumber;
    }

    public void AddLastInt()
    {
        AddIntToContainer();
    }

    public int[] GetIntArray()
        => container.ToArray();

    private void AddIntToContainer()
    {
        container.Add(currentInt);

        currentIntSize = 0;
        currentInt = 0;
    }

    private byte[] ByteToBitRepresentation(byte convertByte)
    {
        var representation = new byte[BITS_IN_BYTE];

        for (var i = BITS_IN_BYTE - 1; i >= 0; --i)
        {
            representation[i] = (byte)(convertByte % 2);
            convertByte >>= 1;
        }

        return representation;
    }
}