using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskNode.Parsers
{
    public class AssignCharAcceptor:ICharAction
    {
        public Node CurrentNode { get; set; }
        private bool SignDetected = false;

        public bool Accept(char ch)
        {
            if (!SignDetected && !((char.IsWhiteSpace(ch) || char.IsControl(ch)) && ch != '='))
                throw new Exception($"ожидался знак '=' , а получен знак '{ch}'");

            SignDetected = SignDetected || ch == '=';

            return ch == '=' || char.IsWhiteSpace(ch) || char.IsControl(ch);



        }
    }
}
