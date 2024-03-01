namespace Trie;

public class Trie
{
    private class TrieVertex
    {
        public bool IsTerminal = false;

        public int PrefixCount = 0;

        public Dictionary<char, TrieVertex> childrens = new ();
    }

    public int Size = 1;

    private TrieVertex root = new ();

    public bool Add(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        TrieVertex current = this.root;
        ++current.PrefixCount;

        foreach (var symbol in element)
        {
            if (!current.childrens.ContainsKey(symbol))
            {
                current.childrens.Add(symbol, new TrieVertex());
            }

            current = current.childrens[symbol];
            ++current.PrefixCount;
        }

        if (current.IsTerminal)
        {
            return false;
        }

        ++this.Size;
        current.IsTerminal = true;
        return true;
    }

    public bool Contains(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        TrieVertex current = this.root;

        foreach (var symbol in element)
        {
            if (!current.childrens.ContainsKey(symbol))
            {
                return false;
            }
        }

        return current.IsTerminal;
    }

    public bool Remove(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Can't be null");
        }

        if (!this.Contains(element))
        {
            return false;
        }

        --this.Size;

        TrieVertex current = this.root;
        --current.PrefixCount;

        foreach (var symbol in element)
        {
            if (current.childrens[symbol].PrefixCount == 1)
            {
                current.childrens.Remove(symbol);
                return true;
            }

            current = current.childrens[symbol];
            --current.PrefixCount;
        }

        current.IsTerminal = false;
        return true;
    }

    public int HowManyStartsWithPrefix(string prefix)
    {
        if (prefix == null)
        {
            throw new ArgumentNullException(nameof(prefix), "Can't be null");
        }

        TrieVertex current = this.root;

        foreach (var symbol in prefix)
        {
            if (!current.childrens.ContainsKey(symbol))
            {
                return 0;
            }
        }

        return current.PrefixCount;
    }
}