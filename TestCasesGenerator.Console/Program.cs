using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using TestCasesGenerator.Core;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                System.Console.Write("File Path: ");
                filePath = System.Console.ReadLine();
            }

            var fileReader = new FileReader();
            string[] lines = fileReader.ReadFile(filePath);

            var fileParse = new FileParser();
            var syntaxTree = fileParse.Parse(lines);

            List<object> parameters = new List<object>();
            foreach(Variable variable in syntaxTree.InputVariables)
            {
                parameters.Add(ReadVariable(variable));
            }

            var runner = new TestRunner(filePath, syntaxTree.InputVariables.ToArray());
            object returnValue = runner.Run(parameters.ToArray());

            System.Console.WriteLine($"Output: {returnValue}");

            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
        }

        private static object ReadVariable(Variable variable)
        {
            System.Console.Write($"Enter the Value for Variable {variable.Name}: ");
            string input = System.Console.ReadLine();

            if (variable.Type == "int")
            {
                return int.Parse(input);
            }
            else if (variable.Type == "float")
            {
                return float.Parse(input);
            }
            else if (variable.Type == "char")
            {
                return char.Parse(input);
            }
            else if (variable.Type == "double")
            {
                return double.Parse(input);
            }
            else
            {
                throw new Exception("Unknown Variable Type.");
            }
        }
    }
}
