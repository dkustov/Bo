using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using TaskNode.Parsers;
using TaskNode.NodesCreation;

namespace TaskNode.Parsers
{
    public class NameCharAcceptor: ICharAction
    {
        private const string AllowedFirstCharacters = "_abcdefghijklmnopqrstuvwxyzABCDEFGGIJKLMNOPQRSTUVWXYZ";
        private const string AllowedNextCharacters = "_1234567890abcdefghijklmnopqrstuvwxyzABCDEFGGIJKLMNOPQRSTUVWXYZ";
        private INodesFactory nodesFactory;


        public Node currentNode { get; set; }

        public NameCharAcceptor( Node n, INodesFactory nf )
        {
            currentNode = n;
            nodesFactory = nf;
        }




        private string NodeName
        {
            get { return currentNode.Name; }
            set
            {
                if(currentNode == null)
                    throw new Exception("Ошибка алгоритма - нет текущего узла");
                currentNode.Name = value;
            }
        }

        public bool Accept(char ch)
        {
            if (CanAccept(ch))
            {
                NodeName = NodeName + ch;
                return true;
            }
            return CanSkip(ch);
        }

        private bool CanSkip(char ch)
        {
            return !NodeName.Any() && (char.IsWhiteSpace(ch) || char.IsControl(ch));
        }

        private bool CanAccept(char ch)
        {
            return (NodeName.Any() ? AllowedNextCharacters : AllowedFirstCharacters).Contains(ch);
        }
    }
}
