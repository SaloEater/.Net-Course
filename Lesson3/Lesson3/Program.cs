using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Lesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = "bc614e";
            var second = "343efcea";

            //var result = Sum(first, second);

            var result = SumExpression();
            var a = result(first, second);
            Console.WriteLine(a);
        }e

        public static Func<string, string, string> SumExpression()
        {
            ParameterExpression map = Expression.Parameter(typeof(char[]), "map");
            ParameterExpression par1 = Expression.Parameter(typeof(string), "par1");
            ParameterExpression chA1 = Expression.Parameter(typeof(char[]), "chA1");
            ParameterExpression par2 = Expression.Parameter(typeof(string), "par2");
            ParameterExpression chA2 = Expression.Parameter(typeof(char[]), "chA2");
            ParameterExpression maxLength = Expression.Parameter(typeof(int), "maxLength");
            ParameterExpression sum = Expression.Parameter(typeof(char[]), "sum");
            ParameterExpression wholePart = Expression.Parameter(typeof(int), "wholePart");
            ParameterExpression i = Expression.Parameter(typeof(int), "i");
            ParameterExpression decimal1 = Expression.Parameter(typeof(int), "decimal1");
            ParameterExpression decimal2 = Expression.Parameter(typeof(int), "decimal2");
            ParameterExpression sumPosition = Expression.Parameter(typeof(int), "sumPosition");
            ParameterExpression remainder = Expression.Parameter(typeof(int), "remainder");
            ParameterExpression result = Expression.Parameter(typeof(string), "result");
            LabelTarget breakLabel = Expression.Label("loop");

            Func<char[], char, int> dIndexOf = (c, v) => Array.IndexOf(c, v);
            Func<char[], string> dJoin = (c) => new string(c);
            Func<string, char[]> dToCharArray = (s) => s.ToCharArray();

            var loop = Expression.Loop(
                Expression.IfThenElse(
                    Expression.LessThan(i, maxLength),
                    Expression.Block(
                        new[] { decimal1, decimal2, sumPosition, remainder },

                        Expression.IfThenElse(
                            Expression.GreaterThan(i, Expression.Subtract(Expression.ArrayLength(chA1), Expression.Constant(1))),
                            Expression.Assign(
                                decimal1,
                                Expression.Constant(0)
                            ),
                            Expression.Assign(
                                decimal1,
                                Expression.Invoke(Expression.Constant(dIndexOf), map, Expression.ArrayIndex(chA1, i))
                            )
                        ),

                        Expression.IfThenElse(
                            Expression.GreaterThan(i, Expression.Subtract(Expression.ArrayLength(chA2), Expression.Constant(1))),
                            Expression.Assign(
                                decimal2,
                                Expression.Constant(0)
                            ),
                            Expression.Assign(
                                decimal2,
                                Expression.Invoke(Expression.Constant(dIndexOf), map, Expression.ArrayIndex(chA2, i))
                            )
                        ),

                        Expression.Assign(sumPosition, Expression.Add(decimal1, Expression.Add(decimal2, wholePart))),
                        Expression.Assign(remainder, Expression.Modulo(sumPosition, Expression.Constant(16))),
                        Expression.Assign(wholePart, Expression.Divide(sumPosition, Expression.Constant(16))),
                        Expression.Assign(
                            Expression.ArrayAccess(sum, i),
                            Expression.ArrayIndex(map, remainder)
                        ),
                        Expression.Assign(i, Expression.Add(i, Expression.Constant(1)))
                    ),
                    Expression.Break(breakLabel)
                ),
                breakLabel
            );
            BlockExpression block = Expression.Block(
                new[] { chA1, chA2, map, result },
                Expression.Assign(map, Expression.Constant(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' })),
                Expression.Assign(chA1, Expression.Invoke(Expression.Constant(dToCharArray), par1)),
                Expression.Call(typeof(Array).GetMethod(nameof(Array.Reverse), new[] { typeof(char[]) }), chA1),
                Expression.Assign(chA2, Expression.Invoke(Expression.Constant(dToCharArray), par2)),
                Expression.Call(typeof(Array).GetMethod(nameof(Array.Reverse), new[] { typeof(char[]) }), chA2),
                Expression.Block(
                    new[] { maxLength, sum, wholePart },
                    Expression.Assign(
                        maxLength,
                        Expression.Call(
                            typeof(Math).GetMethod(nameof(Math.Max), new[] { typeof(int), typeof(int) }),
                            Expression.ArrayLength(chA1),
                            Expression.ArrayLength(chA2)
                        )
                    ),
                    Expression.Assign(
                        sum,
                        Expression.NewArrayBounds(
                            typeof(char),
                            Expression.Add(
                                maxLength,
                                Expression.Constant(1)
                            )
                        )
                    ),
                    Expression.Assign(wholePart, Expression.Constant(0)),
                    Expression.Block(
                        new[] { i },
                        Expression.Assign(i, Expression.Constant(0)),
                        loop
                    ),
                    Expression.IfThenElse(
                        Expression.GreaterThan(wholePart, Expression.Constant(0)),
                        Expression.Assign(
                            Expression.ArrayAccess(sum, Expression.Subtract(Expression.ArrayLength(sum), Expression.Constant(1))),
                            Expression.ArrayIndex(map, wholePart)
                        ),
                        Expression.Assign(
                            Expression.ArrayAccess(sum, Expression.Subtract(Expression.ArrayLength(sum), Expression.Constant(1))),
                            Expression.Constant('0')
                        )
                    ),
                    Expression.Call(typeof(Array).GetMethod(nameof(Array.Reverse), new[] { typeof(char[]) }), sum),
                    Expression.Assign(result, Expression.Invoke(Expression.Constant(dJoin), sum)),
                    result
                ),
                result
            );
            var expressionTree = Expression.Lambda<Func<string, string, string>>(
                block,
                new[] { par1, par2 }
            );
            return expressionTree.Compile();
        }
    }
}
