using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Diagnostics;

namespace TGDGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        //結果のテキストボックス
        private string tGDText;
        public string TGDText
        {
            get { return tGDText; }
            set {
                tGDText = value;
                OnPropertyChanged("TGDText");
            }
        }

        //コードネーム
        private string chordName;
        public string ChordName
        {
            get {
                return chordName; 
            }
            set
            {
                chordName = value;
                OnPropertyChanged("ChordName");
            }
        }

        //ポジション
        private string position;
        public string Position
        {
            get {
                return position; 
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        //フィンガリング
        private string fingering;
        public string Fingering
        {
            get {
                return fingering; 
            }
            set
            {
                fingering = value;
                OnPropertyChanged("Fingering");
            }
        }


        //音名表示フラグ
        private bool flgLetterNotation;
        public bool FlgLetterNotation
        {
            get
            {
                return flgLetterNotation;
            }
            set
            {
                if (value != FlgLetterNotation)
                {
                    flgLetterNotation = value;
                    OnPropertyChanged("FlgLetterNotation");
                }
            }
        }


        //度数表示フラグ
        private bool flgDegree;
        public bool FlgDegree
        {
            get
            {
                return flgDegree;
            }
            set
            {
                if (value != FlgDegree)
                {
                    flgDegree = value;
                    OnPropertyChanged("FlgDegree");
                }
            }
        }



        //ルート
        private int? root;
        public int? Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
                OnPropertyChanged("Root");
            }
        }


        private string[] rootName = new string[12];
        public string[] RootName
        {
            get
            {
                return rootName;
            }
            set
            {
                rootName = value;
                OnPropertyChanged("RootName");
            }
        }



        //度数表示フラグ




        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Common.Initialize();
            Array.Copy(Common.RootNameBase, RootName, Common.RootNameBase.Length);

        }

        private void button_generate_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine()
            //if (Position != null || Position == "") return;

            //TGDiagram tgd = new TGDiagram("test", "8,x,9,9,x,x", "");
            TGDiagram tgd = new TGDiagram(ChordName, Position, Fingering, Root);

            //Console.WriteLine(tgd.IsValid());



            if (tgd.IsValid())
            {
                if(Root == null)
                {
                    
                    for (int i=0; i< 12; i++)
                    {
                        if (i == Common.RootIndex(Position))
                        {
                            RootName[i] = Common.RootNameBase[i] + " ●";
                        }
                        else if (tgd.LetterNotations().Contains(Common.RootNameBase[i]))
                        {
                            RootName[i] = Common.RootNameBase[i] + " ○";
                        }
                        else
                        {
                            RootName[i] = Common.RootNameBase[i];
                        }
                    }
                    
                    Root = Common.RootIndex(Position);
                }

                TGDText = tgd.Stringize();
                if (FlgLetterNotation)
                {
                    TGDText += tgd.LetterNotations();
                }


                if (FlgDegree)
                {
                    TGDText += tgd.Degrees();
                }

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        private void textBox_position_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(Position != null && textBox_position.SelectionStart == Position.Length)
            {
                if (e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Delete || e.Key == Key.Back)
                {
                    e.Handled = false;
                }
                else if (e.Key == Key.Space)
                {
                    if (Position != null && Common.CountChar(Position, ',') < 5 && !Position.EndsWith(","))
                    {
                        Position += ",";
                        textBox_position.Select(textBox_position.Text.Length, 0);
                        e.Handled = true;
                    }else if(Position != null && Common.CountChar(Position, ',') == 5 && !Position.EndsWith(","))
                    {
                        e.Handled = true;
                    }

                }
                else if (((e.Key >= Key.D0) && (e.Key <= Key.D9)) || ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9)))
                {
                    if (Common.CountChar(Position, ',') == 5 && Position.EndsWith("x"))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }

                }
                else
                {
                    if (Common.CountChar(Position, ',') < 5)
                    {
                        if (Position.EndsWith(","))
                        {
                            Position += "x,";
                        }
                        else
                        {
                            Position += ",x,";
                        }
                    }
                    else if (Common.CountChar(Position, ',') == 5)
                    {
                        if (Position.EndsWith(","))
                        {
                            Position += "x";
                        }
                        else
                        {
                            //Position += ",x";
                        }

                    }
                    //else if (Position.EndsWith(","))
                    //{
                    //    Position += "x";
                    //}
                    textBox_position.Select(textBox_position.Text.Length, 0);
                    e.Handled = true;
                }
            }


        }

        private void textBox_position_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Position != null)
            //{
            //    Root = Common.RootIndex(Position);
            //}
            //Root = null;


            for (int i = 0; i < 12; i++)
            {
                RootName[i] = Common.RootNameBase[i];
                //RootName[i] = "hoge";
            }
            

            Root = null;
            
            //Array.Copy(Common.RootNameBase, RootName, Common.RootNameBase.Length);
            
        }
    }
}
