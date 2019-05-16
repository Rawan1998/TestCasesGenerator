using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core
{
    public class TestRunner
    {
        private Object _compiledObject;
        private Variable[] _inputVariables;

        public TestRunner(string filePath, Variable[] inputVariables)
        {
            string file = File.ReadAllText(filePath);
            string source = this.CreateSourceTemplate(file);
            this.InitCompiler(source);
            this._inputVariables = inputVariables;
        }
        
        public object[] RunAll(TestCase[] testCases)
        {
            List<object> returns = new List<object>();

            foreach(TestCase testCase in testCases)
            {
                returns.Add(this.Run(testCase));
            }

            return returns.ToArray();
        }

        public object Run(TestCase testCase)
        {
            List<Object> parameters = new List<object>();
            foreach(object value in testCase.Inputs.Values)
            {
                parameters.Add(value);
            }

            return this.Run(parameters.ToArray());
        }

        public object Run(Object[] parameters)
        {
            List<object> sanitizedParams = new List<object>();

            int i = 0;
            foreach(Variable variable in this._inputVariables)
            {
                if (variable.Type == "int")
                {
                    sanitizedParams.Add(int.Parse(Convert.ToString(parameters[i])));
                }
                else if (variable.Type == "float")
                {
                    sanitizedParams.Add(float.Parse(Convert.ToString(parameters[i])));
                }
                else if (variable.Type == "char")
                {
                    sanitizedParams.Add(char.Parse(Convert.ToString(parameters[i])));
                }
                else if (variable.Type == "double")
                {
                    sanitizedParams.Add(double.Parse(Convert.ToString(parameters[i])));
                }

                i++;
            }

            MethodInfo mi = this._compiledObject.GetType().GetMethod("TestMethod");

            var task = Task.Run(() => {
                try
                {
                    return mi.Invoke(this._compiledObject, sanitizedParams.ToArray());
                }
                catch (TargetInvocationException tie)
                {
                    return tie.InnerException.Message;
                }
            });

            if (task.Wait(TimeSpan.FromSeconds(0.1)))
                return task.Result;
            else
                return "TIMEOUT";
        }

        private void InitCompiler(string source)
        {
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v3.5"}
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };

            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);

            if (results.Errors.Count != 0)
                throw new Exception("Mission failed!");

            this._compiledObject = results.CompiledAssembly.CreateInstance("TestCasesGenerator.Test");
        }

        private string CreateSourceTemplate(string file)
        {
            string source =
@"
namespace TestCasesGenerator
{
    public class Test
    {
       " + file + @"
    }
}
            ";
            
            return source;
        }
    }
}
