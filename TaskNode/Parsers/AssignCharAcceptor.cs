using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.NodesCreation;

namespace TaskNode.Parsers
{
    public class AssignCharAcceptor:ICharAction
    {
        public Node CurrentNode { get; set; }
        private INodesFactory NodesFactory;
        private bool SignDetected = false;

        public AssignCharAcceptor( Node node, INodesFactory nodesFactory )
        {
            CurrentNode = node;
            NodesFactory = nodesFactory;
        }


        public bool Accept(char ch)
        {               
            if ( !SignDetected && ( !(char.IsWhiteSpace( ch ) || char.IsControl( ch ) )&& ch != '=' ) )
                throw new Exception(String.Format("Ожидался знак '=' , а получен знак '{0}'",ch));

            if ( SignDetected && ch == '=')
                throw new Exception( "Два знака равно" );

            SignDetected = SignDetected || ch == '=' ;
            return !SignDetected ;   //Не будем подрезать за собой

            //return ch == '=' || char.IsWhiteSpace( ch ) || char.IsControl( ch );
             
        }
    }
}
