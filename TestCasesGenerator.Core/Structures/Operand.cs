using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesGenerator.Core.Structures
{
    public class Operand
    {
        private Variable _variable;
        private object _value;
        private bool _isVariable;

        public bool IsVariable => this._isVariable;
        public bool IsVariableInput => this._isVariable && this._variable.IsInput;
        public bool IsConstant => !this.IsVariableInput;

        public object Value
        {
            get
            {
                if (this._isVariable)
                {
                    return this._variable;
                }
                else
                {
                    return this._value;
                }
            }
        }

        public Operand(Variable variable)
        {
            this._isVariable = true;
            this._variable = variable;
        }

        public Operand(object value)
        {
            this._isVariable = false;
            this._value = value;
        }
    }
}
