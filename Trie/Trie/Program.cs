using Trie;

Console.WriteLine("Trie");

var trie = new Trie.Trie();

while (true)
{
    Console.WriteLine("""
        Choose command:
        1 - Add word to tree
        2 - Remove word from tree
        3 - Check does trie contain word
        4 - Get number of words with prefix
        5 - Get number of words in trie
        6 - Exit
    """);

    var command = Console.ReadLine();

    switch (command)
    {
        case "1":
        {
            Console.WriteLine("Enter word: ");

            var word = Console.ReadLine();
            if (!trie.Add(word))
            {
                Console.WriteLine("Word is in trie already");
                break;
            }

            Console.WriteLine("Word is successfully add to trie");

            break;
        }

        case "2":
        {
            Console.WriteLine("Enter word: ");

            var word = Console.ReadLine();
            if (!trie.Remove(word))
            {
                Console.WriteLine("Word isn't in trie");
                break;
            }

            Console.WriteLine("Word is successfully remove from trie");

            break;
        }

        case "3":
        {
            Console.WriteLine("Enter word: ");

            var word = Console.ReadLine();
            if (trie.Contains(word))
            {
                Console.WriteLine("Word is in trie");
                break;
            }

            Console.WriteLine("Word isn't in trie");

            break;
        }

        case "4":
        {
            Console.WriteLine("Enter word: ");

            var prefix = Console.ReadLine();

            Console.WriteLine($"Number of words in trie with this prefix is {trie.HowManyStartsWithPrefix(prefix)}");

            break;
        }

        case "5":
        {
            Console.WriteLine($"Number of words in tree is {trie.Size}");

            break;
        }

        case "6":
        {
            return 0;
        }

        default:
        {
            Console.WriteLine("You entered wrong number, please repeat and read instructions.");

            break;
        }
    }
}