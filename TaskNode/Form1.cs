using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaskNode
{
    enum eParseMode
    {
        SearchNameStart,
        SearchNameStop,
        SearchValueStart,
        SearchValueStop,
        SearchChildStop
    };

    public partial class Form1 :Form
    {
        private String fileName;
        private Node treeNode;
        //public Node treeNode;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click( object sender, EventArgs e )
        {
            treeNode = new Node();
            try
            {
                if ( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    fileName = openFileDialog1.FileName;
                    using ( System.IO.StreamReader file = new System.IO.StreamReader( fileName, Encoding.Default ) )
                    {
                        int Id = 0;
                        eParseMode ParseMode = eParseMode.SearchNameStart;
                        string currentName = String.Empty;
                        string currentValue = String.Empty;
                        Node currentNode;
                        treeNode.Id = 0;
                        currentNode = treeNode;
                        bool needCreateNode = false;
                        bool IsMustBeEqualy = false;
                        while ( !file.EndOfStream )
                        {
                            string line = file.ReadLine();
                            foreach ( char c in line )
                            {
                                if (ParseMode == eParseMode.SearchNameStart || ParseMode == eParseMode.SearchValueStart )
                                    if ( char.IsWhiteSpace( c ) || char.IsControl( c ) ) //пропустим мусор
                                        continue;
                                if (ParseMode == eParseMode.SearchNameStart)
                                {
                                    if ( c.Equals( '}' ) )
                                    {
                                        currentNode = currentNode.Parent;
                                        currentNode.IsValidate = true;
                                        needCreateNode = true;
                                        continue;
                                    }
                                    if ( char.IsNumber( c ) && ( currentName == String.Empty ) ) //строка не может начинаться с цифры
                                    {
                                        throw new ArgumentException( "Cтрока не может начинаться с цифры" );
                                    }
                                    //Это начало имени
                                    if ( needCreateNode )
                                    {
                                        currentNode.Parent.AddChild( new Node () );
                                        currentNode = currentNode.Parent.ChildList.Last();
                                        currentNode.Id = Id++;
                                        needCreateNode = false;
                                    }
                                    ParseMode = eParseMode.SearchNameStop;
                                    currentName += c;
                                    continue;
                                }
                                
                                if ( ParseMode == eParseMode.SearchNameStop  )
                                {
                                    if ( char.IsLetterOrDigit( c ) || c.Equals( '_' ) )
                                    {
                                        if ( IsMustBeEqualy )
                                            throw new ArgumentException( "Имя содержит управляющий символ или пробел" );
                                        currentName += c;
                                        continue;
                                    }
                                    if ( char.IsWhiteSpace( c ) || char.IsControl( c ) ) //пропустим мусор, активируем признак поиска равно
                                    {
                                        IsMustBeEqualy = true;
                                        continue;
                                    }
                                    if ( c.Equals( '=' ) ) //Закончили с именем, переходим к поиску значения
                                    {
                                        //if ( currentNode != null )
                                        IsMustBeEqualy = false;
                                        currentNode.Name = currentName;
                                        currentName = String.Empty;
                                        ParseMode = eParseMode.SearchValueStart;
                                        continue;
                                    }
                                }
                                if ( ParseMode == eParseMode.SearchValueStart )
                                {
                                    if ( !c.Equals( '\"' ) && !c.Equals( '{' ) ) //все управляющие символы мы уже отрезали
                                        throw new ArgumentException( "Не хватает двойных кавычек" );
                                    if ( c.Equals( '\"' ) )
                                    {
                                        ParseMode = eParseMode.SearchValueStop;
                                    }
                                    if ( c.Equals( '{' ) )
                                    {
                                        currentNode.AddChild( new Node() );
                                        currentNode = currentNode.ChildList.Last();
                                        currentNode.Id = Id++;
                                        ParseMode = eParseMode.SearchNameStart;
                                    }
                                    continue;
                                }

                                if ( ParseMode == eParseMode.SearchChildStop )
                                {
                                    ParseMode = eParseMode.SearchNameStart;
                                }

                                //Это просто значение 
                                if ( ParseMode == eParseMode.SearchValueStop )
                                {
                                    if ( c.Equals( '\"' ) )
                                    {
                                        currentNode.IsValidate = true;
                                        currentNode.Value = currentValue;
                                        currentValue = String.Empty;
                                        //currentNode.Id = Id++;
                                        ParseMode = eParseMode.SearchNameStart;
                                        needCreateNode = true;
                                        continue;
                                    }
                                    if ( c.Equals('\r') || c.Equals('\n') )
                                        throw new ArgumentException( "Значение содержит перевод каретки" );
                                    currentValue += c;
                                    continue;
                                }
                            }
                        }
                    }
                }
                /*
                if ( treeNode.IsValidate )
                    throw new ArgumentException( "Ошибка разбора" );
                 */ 
                textBox1.Text = treeNode.ToString();
            }
            catch (Exception Error )
            {
                MessageBox.Show( Error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        private void button2_Click( object sender, EventArgs e )
        {
        }

}

}
