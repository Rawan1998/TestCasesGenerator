using System;
using System.Collections.Generic;
using System.Linq;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core
{
    public class FileParser
    {
        private Stack<Scope> scope;
        private List<Variable> inputVariables;

        public SyntaxTree Parse(String[] file)
        {
            this.scope = new Stack<Scope>();
            this.inputVariables = new List<Variable>();
            
            scope.Push(this.ParseFunction(file[0]));

            List<String> lineList = file.ToList<String>();
            lineList.RemoveAt(0);

            foreach (String line in file)
            {
                String[] parsedLine = this.ParseLine(line);
                Scope scopedKeyword = this.ParseScope(parsedLine);
                if (scopedKeyword != null)
                {
                    this.scope.Peek().Children.Add(scopedKeyword);
                    scopedKeyword.Variables.AddRange((this.scope.Peek() as Scope).Variables);
                    this.scope.Push(scopedKeyword);
                    continue;
                }

                Variable variableDeclaration = this.ParseVariableDeclaration(parsedLine);
                if (variableDeclaration != null)
                {
                    (this.scope.Peek() as Scope).Variables.Add(variableDeclaration);
                }

                Operator[] op = this.ParseOperators(parsedLine);
                this.scope.Peek().Children.AddRange(op);

                if (this.scope.Count > 1 && parsedLine[0] == "}")
                {
                    this.scope.Pop();
                }
            }

            return new SyntaxTree(this.scope.Peek() as Function, this.inputVariables);
        }

        private Function ParseFunction(String line)
        {
            Function function = new Function();
            String[] parsedLine = this.ParseLine(line);

            int curIndex = 3;
            while(parsedLine.Length > curIndex)
            {
                Variable variable = new Variable(parsedLine[curIndex], parsedLine[curIndex + 1], true);
                this.inputVariables.Add(variable);
                function.Parameters.Add(variable);
                function.Variables.Add(variable);
                curIndex += 2;
            }

            return function;
        }

        private String[] ParseLine(String line)
        {
            String[] parsed = line.Replace(';', ' ').Replace('(', ' ').Replace(')', ' ').Replace(',', ' ').Split(' ');
            return parsed.Where(item => item != "").ToArray<String>();
        }

        private Scope ParseScope(String[] line)
        {
            String keyword = line[0];
            if (line.Length > 1 && line[1] == "if")
            {
                keyword = $"{line[0]} {line[1]}";
            }

            String[] conditionals = { "else if", "if", "while" };

            if (conditionals.Contains<String>(keyword))
            {
                return this.ParseConditionalScope(keyword, line);
            }
            else if (keyword == "for")
            {
                return this.ParseForScope(line);
            }
            else if (keyword == "else")
            {
                return new Scope("else");
            }

            return null;
        }

        private ConditionalScope ParseConditionalScope(String keyword, String[] line)
        {
            ConditionalScope scope = new ConditionalScope(keyword);

            Operator op;
            if (keyword == "else if")
                op = new Operator(line[3]);
            else
                op = new Operator(line[2]);

            Scope currentScope = this.scope.Peek() as Scope;
            op.LeftOperand = this.ParseOperand(line[1]);
            op.RightOperand = this.ParseOperand(line[3]);

            scope.Operator = op;
            return scope;
        }

        private ConditionalScope ParseForScope(String[] line)
        {
            ConditionalScope scope = new ConditionalScope("for");
            Variable var = new Variable("int", line[2], line[4], false);
            scope.Variables.Add(var);

            Operator op = new Operator(line[6]);
            op.LeftOperand = new Operand(var);
            op.RightOperand = new Operand(line[7]);
            scope.Operator = op;

            return scope;
        }

        private Operator[] ParseOperators(String[] line)
        {
            String types = "> >= < <= == * + / - =";
            List<Operator> operators = new List<Operator>();

            for (int i = 0; i < line.Length; ++i)
            {
                if (types.Contains(line[i]))
                {
                    Operator op = new Operator(line[i]);
                    op.LeftOperand = this.ParseOperand(line[i - 1]);
                    op.RightOperand = this.ParseOperand(line[i + 1]);
                    operators.Add(op);
                }
            }

            return operators.ToArray();
        }

        private Operand ParseOperand(String value)
        {
            Scope currentScope = this.scope.Peek() as Scope;
            Variable var = currentScope.FindVariable(value);
            if (var != null)
            {
                return new Operand(var);
            }
            else
            {
                return new Operand(value);
            }
        }

        private Variable ParseVariableDeclaration(String[] line)
        {
            String[] types = { "int", "float", "double", "char" };
            String keyword = line[0];
            
            if (types.Contains<String>(keyword))
            {
                if (line.Contains<String>("/*INPUT*/"))
                {
                    Variable var = new Variable(keyword, line[1], true);
                    this.inputVariables.Add(var);
                    return var;
                }
                else if (line.Length > 3)
                {
                    return new Variable(keyword, line[1], line[3], false);
                }
                else
                {
                    return new Variable(keyword, line[1], false);
                }
            }

            return null;
        }
    }
}
