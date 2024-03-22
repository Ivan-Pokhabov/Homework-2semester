var a = new ParseTree.ParseTree();

a.BuildTree("/ (* (+ 1 1) -2,2) 5 +");

Console.WriteLine(a.CalclulateExpression());
a.Print();