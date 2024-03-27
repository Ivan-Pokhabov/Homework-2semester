// Copyright (c) Ivan-Pokhabov. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace ParseTree;

/// <summary>
/// Class of node with operation in parse tree.
/// </summary>
/// <param name="operation">Type of binary operation.</param>
public class OperationNode(char operation) : IParseTreeNode
{
    /// <summary>
    /// Gets or sets type of operation.
    /// </summary>
    public char Operation { get; set; } = operation;

    /// <summary>
    /// Gets or sets left child of node.
    /// </summary>
    public IParseTreeNode? LeftChild { get; set; }

    /// <summary>
    /// Gets or sets right child of node.
    /// </summary>
    public IParseTreeNode? RightChild { get; set; }

    /// <inheritdoc/>
    public double CalculateSubtree()
    {
        ArgumentNullException.ThrowIfNull(LeftChild);
        ArgumentNullException.ThrowIfNull(RightChild);

        var leftPart = LeftChild.CalculateSubtree();
        var rightPart = RightChild.CalculateSubtree();
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
            throw new DivideByZeroException("Tree contains division by zero");
        }
    }

    /// <inheritdoc/>
    public string PrintSubtree() => $"({Operation} {LeftChild?.PrintSubtree()} {RightChild?.PrintSubtree()})";

    private static double CalclulateExpression(char operation, double leftPart, double rightPart) =>
        operation switch
        {
            '+' => leftPart + rightPart,
            '-' => leftPart - rightPart,
            '*' => leftPart * rightPart,
            '/' => (rightPart == 0) ? throw new DivideByZeroException() : leftPart / rightPart,
            _ => throw new ArgumentException("Operation is incorrect ", nameof(operation))
        };
}