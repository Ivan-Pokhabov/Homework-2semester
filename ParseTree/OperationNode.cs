namespace ParseTree;

public class OperationNode(char operation) : IParseTreeNode
{
    public char Operation { private get; set; } = operation;
    public IParseTreeNode? LeftChild { get; set; }
    public IParseTreeNode? RightChild { get; set; }

    public double CalclulateSubtree()
    {
        ArgumentNullException.ThrowIfNull(LeftChild);
        ArgumentNullException.ThrowIfNull(RightChild);

        var leftPart = LeftChild.CalclulateSubtree();
        var rightPart = RightChild.CalclulateSubtree();
        try
        {
            return CalclulateExpression(Operation, leftPart, rightPart);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Operation isn't declared in tree");
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Tree contains division by zero");
        }
    }

    public void Print()
    {
        Console.Write($"( {Operation} ");
        LeftChild?.Print();
        RightChild?.Print();
        Console.Write(") ");
    }

    static private double CalclulateExpression(char Operation, double leftPart, double rightPart) =>
        Operation switch
        {
            '+' => leftPart + rightPart,
            '-' => leftPart - rightPart,
            '*' => leftPart * rightPart,
            '/' => (rightPart == 0) ? throw new DivideByZeroException() : leftPart / rightPart,
            _ => throw new ArgumentException("Operation is incorrect ", nameof(Operation))
        };
}