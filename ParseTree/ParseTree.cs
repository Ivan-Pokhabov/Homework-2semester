namespace ParseTree;

public class ParseTree()
{
    private IParseTreeNode? root;

    public void BuildTree(string expression)
    {
        ArgumentException.ThrowIfNullOrEmpty(expression);

        expression = expression.Replace('(', ' ');
        expression = expression.Replace(')', ' ');
        var expressionElements = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var index = 0;
        root = Build(expressionElements, ref index);

        IParseTreeNode? Build(string[] expression, ref int index)
        {
            if (index == expression.Length)
            {
                return null;
            }

            if (IsOperation(expression[index]))
            {
                var operationNode = new OperationNode(expression[index].ToCharArray()[0]);
                ++index;

                operationNode.LeftChild = Build(expression, ref index) ?? throw new ArgumentException("Exception is incorrect", nameof(expression));
                operationNode.RightChild = Build(expression, ref index) ?? throw new ArgumentException("Exception is incorrect", nameof(expression));

                return operationNode;
            }

            if (double.TryParse(expression[index], out double result))
            {
                ++index;
                return new OperandNode(result);
            }

            throw new ArgumentException("Exception is incorrect", nameof(expression));
        }

        if (index != expressionElements.Length)
        {
            throw new ArgumentException("Exception is incorrect", nameof(expression));
        }
    }

    public void Print()
    {
        if (root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        root.Print();
    }

    public double CalclulateExpression()
    {
        if (root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        try
        {
            return root.CalclulateSubtree();
        }
        catch (DivideByZeroException)
        {
            throw new ArgumentException("Expression contains division by zero");
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Expression contains none declared operation");
        }
    }

    private bool IsOperation(string symbol) => symbol == "+" || symbol == "/" || symbol == "-" || symbol == "*";
}