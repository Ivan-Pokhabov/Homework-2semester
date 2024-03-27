// Copyright (c) Ivan-Pokhabov. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace ParseTree;

/// <summary>
/// Class of parse tree.
/// </summary>
public class ParseTree()
{
    private IParseTreeNode? root;

    /// <summary>
    /// Build tree by expression.
    /// </summary>
    /// <param name="expression">String.</param>
    /// <exception cref="ArgumentException">Expression should be not null, not empty and correct.</exception>
    public void BuildTree(string expression)
    {
        ArgumentException.ThrowIfNullOrEmpty(expression);

        expression = expression.Replace('(', ' ');
        expression = expression.Replace(')', ' ');
        var expressionElements = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var index = 0;
        root = Build(expressionElements, ref index);

        if (index != expressionElements.Length)
        {
            throw new ArgumentException("Exception is incorrect", nameof(expression));
        }
    }

    /// <summary>
    /// Print parse tree into console.
    /// </summary>
    /// <exception cref="InvalidOperationException">Tree should be built.</exception>
    /// <returns>String of expression.</returns>
    public string GetExpressionToString()
    {
        if (root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        return root.PrintSubtree();
    }

    /// <summary>
    /// Calculate tree.
    /// </summary>
    /// <returns>Double result.</returns>
    /// <exception cref="InvalidOperationException">Tree should be built.</exception>
    /// <exception cref="ArgumentException">Expression should be correct.</exception>
    public double CalculateTree()
    {
        if (root is null)
        {
            throw new InvalidOperationException("Tree is empty");
        }

        try
        {
            return root.CalculateSubtree();
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

    private IParseTreeNode? Build(string[] expression, ref int index)
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
}