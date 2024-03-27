// Copyright (c) Ivan-Pokhabov. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace ParseTree;

/// <summary>
/// Interface of node in Parse tree.
/// </summary>
public interface IParseTreeNode
{
    /// <summary>
    /// Calculate expression in subtree.
    /// </summary>
    /// <returns>Double result.</returns>
    double CalculateSubtree();

    /// <summary>
    /// Print subtree into console.
    /// </summary>
    /// <returns>String representation of subtree.</returns>
    string PrintSubtree();
}