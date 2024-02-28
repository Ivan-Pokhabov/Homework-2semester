using StackCalculator;

string a = "1,5 2,5 * 3,36 5,86 / *";
var stack1 = new StackArray();
var calc1 = new Calculator(stack1);
var stack2 = new StackList();
var calc2 = new Calculator(stack2);
Console.WriteLine(calc1.CalculatePostfixExpression(a));
Console.WriteLine(calc2.CalculatePostfixExpression(a));