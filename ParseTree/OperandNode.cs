namespace ParseTree;

public class OperandNode(double number) : IParseTreeNode
{
    public double Number { private get; set; } = number;
    public double CalclulateSubtree() => Number;

    public void Print()
    {
        Console.Write(Number);
    }
}