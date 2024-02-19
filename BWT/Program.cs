using System.Linq.Expressions;
using System.Numerics;

int[] SuffixArrayBuild(string str)
{
    int[] shifts = new int[str.Length];
    int[] c = new int[str.Length];
    for (int i = 0; i < str.Length; i++)
    {
        shifts[i] = i;
    }
    Array.Sort(shifts, (int a, int b) => { if (str[a] < str[b]) return -1; if (str[a] > str[b]) return 1; return 0; });
    for (int i = 1; i < str.Length; ++i)
    {
        c[shifts[i]] = c[shifts[i - 1]];
        if ((str[shifts[i]] != str[shifts[i - 1]]))
        {
            ++c[shifts[i]];
        }
    }
    for (int k = 1; k < str.Length; k *= 2)
    {
        List<(int, int)>[] countingSortArray = new List<(int, int)>[str.Length];
        for(int h = 0; h < str.Length; ++h)
        {
            countingSortArray[h] = new List<(int, int)>();
        }
        foreach(var secondHalf in shifts)
        {
            int firstHalf = secondHalf - k;
            if (firstHalf < 0)
            {
                firstHalf += str.Length;
            }
            countingSortArray[c[firstHalf]].Add((c[secondHalf], firstHalf));
        }
        int cn = 0;
        int shiftIndex = 0;
        foreach(var element in countingSortArray)
        {
            int x = -1;
            foreach(var (ci, j) in element)
            {
                shifts[shiftIndex++] = j;
                if (ci != x)
                {
                    ++cn;
                    x = ci;
                }
                c[j] = cn - 1;
            }
        }
        if (cn == str.Length)
        {
            break;
        }
    }
    return shifts;
}


foreach(var element in SuffixArrayBuild(Console.ReadLine() + "$"))
{
    Console.Write($"{element} ");
}