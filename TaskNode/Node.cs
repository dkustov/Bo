using System;
//using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskNode
{
    class Node
    {
        public String Name;
        public String Value;
        public int Id;               //Нужно следить за Id
        public Node Parent;
        public bool IsValidate;
        public List<Node> ChildList;


        public void AddChild( Node _node )
        {
            if ( _node == null )
                return;
            //Без проверки на существование
            ChildList.Add( _node );
            _node.Parent = this;
        }


        public Node()
        {
            Name = "";
            Value = "";
            Id = -1;
            Parent = null;
            ChildList = new List<Node>();
            IsValidate = false;
        }
        public override string ToString()
        {
            string s;
            s = String.Format( "id={0}; ParentId={1}; Name={2}; Value={3}" + Environment.NewLine, Id, ( Parent != null ) ? Parent.Id.ToString() : String.Empty, Name, Value );
            foreach ( Node n in ChildList )
                s = s + n.ToString();
            return s;
        }
    }
}
