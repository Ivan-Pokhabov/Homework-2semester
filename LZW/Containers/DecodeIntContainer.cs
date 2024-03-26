namespace Containers;

/// <summary>
/// Class of container bytes in int representation.
/// </summary>
public class DecodeIntContainer
{
    private const int BitsInByte = 8;

    private readonly List<int> container = new ();

    private int currentInt = 0;

    private int currentIntSize = 0;

    /// <summary>
    /// Gets or sets current int size in bytes.
    /// </summary>
    public int IntBitSize { get; set; } = 8;

    /// <summary>
    /// Gets or sets max size of int size.
    /// </summary>
    public int MaxInt { get; set; } = 256;

    /// <summary>
    /// Add byte of int.
    /// </summary>
    /// <param name="codeByte">Byte of some int code.</param>
    /// <returns>True if new int added into container.</returns>
    public bool Add(byte codeByte)
    {
        var representation = ByteToBitRepresentation(codeByte);

        var newNumber = false;

        foreach (var bit in representation)
        {
            currentInt = (currentInt << 1) + bit;

            ++currentIntSize;

            if (currentIntSize == IntBitSize)
            {
                AddIntToContainer();
                newNumber = true;
            }
        }

        return newNumber;
    }

    /// <summary>
    /// Transform container into array of ints.
    /// </summary>
    /// <returns>Int array.</returns>
    public int[] GetIntArray()
    {
        PrepareContainerToTransformIntoArray();
        return container.ToArray();
    }

    private void PrepareContainerToTransformIntoArray()
    {
        AddIntToContainer();
    }

    private void AddIntToContainer()
    {
        container.Add(currentInt);

        currentIntSize = 0;
        currentInt = 0;
    }

    private byte[] ByteToBitRepresentation(byte convertByte)
    {
        var representation = new byte[BitsInByte];

        for (var i = BitsInByte - 1; i >= 0; --i)
        {
            representation[i] = (byte)(convertByte % 2);
            convertByte >>= 1;
        }

        return representation;
    }
}