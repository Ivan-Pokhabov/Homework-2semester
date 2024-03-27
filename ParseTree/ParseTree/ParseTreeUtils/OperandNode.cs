// Copyright (c) Ivan-Pokhabov. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace ParseTree;

/// <summary>
/// Class of opearand node.
/// </summary>
/// <param name="number">Node value.</param>
public class OperandNode(double number) : IParseTreeNode
{
    /// <summary>
    /// Gets or sets value of operand.
    /// </summary>
    public double Number { get; set; } = number;

    /// <inheritdoc/>
    public double CalculateSubtree() => Number;

    /// <inheritdoc/>
    public string PrintSubtree() => $"{Number}";
}