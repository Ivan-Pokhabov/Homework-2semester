// Copyright (c) Ivan-Pokhabov, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace MyVector;

/// <summary>
/// Class of Vector.
/// </summary>
public class Vector
{
    private readonly List<(int, int)> vector = [];

    /// <summary>
    /// Gets size of Vector.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets a value indicating whether gets is null vector or not.
    /// </summary>
    public bool IsNull
    {
        get
        {
            foreach (var (_, number) in vector)
            {
                if (number != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector"/> class.
    /// </summary>
    /// <param name="array">Array of ints.</param>
    public Vector(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);

        int zerosCount = 0;

        foreach (var number in array)
        {
            if (number == 0)
            {
                ++zerosCount;
                continue;
            }

            if (zerosCount > 0)
            {
                vector.Add((zerosCount, 0));
                zerosCount = 0;
            }

            vector.Add((1, number));
        }

        if (zerosCount > 0)
        {
            vector.Add((zerosCount, 0));
        }

        Size = array.Length;
    }

    /// <summary>
    /// Indexer.
    /// </summary>
    /// <param name="index">int index.</param>
    /// <returns>value by index.</returns>
    /// <exception cref="IndexOutOfRangeException">index should be correct.</exception>
    public int this[int index]
    {
        get
        {
            if (Size <= index || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            int vectorIndex = 0;
            int numberIndex = 0;

            for (int i = 0; i < vector.Count; ++i)
            {
                if (vectorIndex >= index)
                {
                    numberIndex = int.Max(0, (vectorIndex == index) ? i : i - 1);
                    break;
                }

                vectorIndex += vector[i].Item1;
            }

            return vector[numberIndex].Item2;
        }

        set
        {
            if (Size <= index || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            int vectorIndex = 0;
            int numberIndex = 0;

            for (int i = 0; i < vector.Count; ++i)
            {
                if (vectorIndex >= index)
                {
                    numberIndex = int.Max(0, (vectorIndex == index) ? i : i - 1);
                    break;
                }

                vectorIndex += vector[i].Item1;
            }

            if (vectorIndex == index)
            {
                vector[numberIndex] = (1, value);
                return;
            }

            if (numberIndex == 0 && index != 0)
            {
                numberIndex = vector.Count - 1;
            }

            vector[numberIndex] = (vector[numberIndex].Item1 - (vectorIndex - index), 0);
            vector.Insert(numberIndex + 1, (1, value));
            vector.Insert(numberIndex + 2, (vectorIndex - index - 2, 0));
        }
    }

    /// <summary>
    /// Add vector to vector.
    /// </summary>
    /// <param name="addVector">adding vector.</param>
    /// <exception cref="ArgumentException">Adding vector can't be null.</exception>
    public void Add(Vector addVector)
    {
        ArgumentNullException.ThrowIfNull(addVector);

        if (Size != addVector.Size)
        {
            throw new ArgumentException();
        }

        for (int i = 0; i < Size; ++i)
        {
            this[i] += addVector[i];
        }
    }

    /// <summary>
    /// Substractikng vectors.
    /// </summary>
    /// <param name="substractionVector">vector that we substract.</param>
    /// <exception cref="ArgumentException">vector should be not null.</exception>
    public void Substraction(Vector substractionVector)
    {
        ArgumentNullException.ThrowIfNull(substractionVector);

        if (Size != substractionVector.Size)
        {
            throw new ArgumentException();
        }

        for (int i = 0; i < Size; ++i)
        {
            this[i] -= substractionVector[i];
        }
    }

    /// <summary>
    /// Scalar product of vectors.
    /// </summary>
    /// <param name="productVector">Second vector.</param>
    /// <returns>Int result.</returns>
    /// <exception cref="ArgumentException">Second vector should be not null.</exception>
    public int ScalarProduct(Vector productVector)
    {
        ArgumentNullException.ThrowIfNull(productVector);

        if (Size != productVector.Size)
        {
            throw new ArgumentException();
        }

        int result = 0;

        for (int i = 0; i < Size; ++i)
        {
            result += this[i] * productVector[i];
        }

        return result;
    }
}