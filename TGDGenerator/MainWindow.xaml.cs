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


        private bool flgAutoNaming;
        public bool FlgAutoNaming
        {
            get
            {
                return flgAutoNaming;
            }
            set
            {
                if (value != FlgAutoNaming)
                {
                    flgAutoNaming = value;
                    OnPropertyChanged("FlgAutoNaming");
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



        //Capo
        private int capo;
        public int Capo
        {
            get
            {
                return capo;
            }
            set
            {
                capo = value;
                OnPropertyChanged("Capo");
            }
        }




        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Common.Initialize();
            Array.Copy(Common.RootNameBase, RootName, Common.RootNameBase.Length);
            Capo = 0;

        }

        private void button_generate_Click(object sender, RoutedEventArgs e)
        {
            int? bufRoot = Root;


            if (bufRoot == null)
            {
                bufRoot = Common.RootIndex(Position);
            }


            TGDiagram tgd = new TGDiagram(ChordName, Position, Fingering, bufRoot);

            if (tgd.IsValid())
            {

                for (int i = 0; i < 12; i++)
                {
                    if (i == Common.RootIndex(Position))
                    {
                        RootName[i] = Common.RootNameBase[i].PadRight(2) + "※";
                    }
                    else if (tgd.LetterNotations().Contains(Common.RootNameBase[i]))
                    {
                        RootName[i] = Common.RootNameBase[i].PadRight(2) + "○";
                    }
                    else
                    {
                        RootName[i] = Common.RootNameBase[i];
                    }
                }

                //comboBox_root.ItemsSource = new List<string>();
                //comboBox_root.ItemsSource = RootName;

                Position = tgd.GetPosition();

                comboBox_root.Items.Refresh();
                comboBox_root.SelectedIndex = (int)bufRoot;
            




                TGDText = String.Empty;

                if (FlgAutoNaming)
                {
                    //string tmpChordName = tgd.GetAutoName();
                    //TGDText += tmpChordName;
                    //ChordName = tmpChordName;
                    ChordName = tgd.GetAutoName();
                }
                
                //コードネーム
                if (ChordName != null && ChordName.Trim().Length > 0)
                {
                    if (Common.GraphicalLength(ChordName) <= 12)
                    {
                        TGDText += "  " + Common.Centering(ChordName, 12);
                    }
                    else if (Common.GraphicalLength(ChordName) == 13)
                    {
                        TGDText += " " + Common.Centering(ChordName, 12);
                    }
                    else
                    {
                        TGDText += Common.Centering(ChordName, 12);
                    }
                }
                TGDText += "\r\n";

                TGDText += tgd.Stringize();

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

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.A:
                        e.Handled = false;
                        break;
                    case Key.X:
                        e.Handled = false;
                        break;
                    case Key.C:
                        e.Handled = false;
                        break;
                    case Key.V:
                        e.Handled = false;
                        break;
                }
            }

            else if (e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else if (e.Key == Key.OemPeriod || e.Key == Key.OemComma || e.Key == Key.Decimal)
            {
                if (Position != null && Common.CountChar(Position, ',') < 5 && !Position.EndsWith(",") && Position != String.Empty)
                {
                    Position += ",";
                    textBox_position.Select(textBox_position.Text.Length, 0);
                    e.Handled = true;
                }
                else if (Position != null && Common.CountChar(Position, ',') == 5 && !Position.EndsWith(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.OemPlus || e.Key == Key.Space || e.Key == Key.Add)
            {
                if (Position == null)
                {
                    Position += "x,";
                    textBox_position.Select(textBox_position.Text.Length, 0);
                    e.Handled = true;
                }
                else if (Position != null && Common.CountChar(Position, ',') <= 3 && !Position.EndsWith(","))
                {
                    if (Position == String.Empty)
                    {
                        Position += "x,";
                    }
                    else
                    {
                        Position += ",x,";
                    }
                    textBox_position.Select(textBox_position.Text.Length, 0);
                    e.Handled = true;
                }
                else if (Position != null && Common.CountChar(Position, ',') <= 4 && Position.EndsWith(","))
                {
                    Position += "x,";
                    textBox_position.Select(textBox_position.Text.Length, 0);
                    e.Handled = true;
                }
                else if (Position != null && Common.CountChar(Position, ',') == 5 && Position.EndsWith(","))
                {
                    Position += "x";
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0) && (e.Key <= Key.D9)) || ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9)) || e.Key == Key.X)
            {
                e.Handled = false;
            }
            else if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void textBox_position_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Position != null)
            //{
            //    Root = Common.RootIndex(Position);
            //}
            //Root = null;


            //for (int i = 0; i < 12; i++)
            //{
            //    RootName[i] = Common.RootNameBase[i];
            //    //RootName[i] = "hoge";
            //}
            

            Root = null;
            
            //Array.Copy(Common.RootNameBase, RootName, Common.RootNameBase.Length);
            
        }

        private void button_copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, TGDText);
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            Position = string.Empty;
            TGDText = string.Empty;
        }

        private void button_play_Click(object sender, RoutedEventArgs e)
        {
            if (TGDText != null && TGDText != String.Empty)
            {
                ChordSound cs = new ChordSound(Position, Capo);
                cs.PlaySound();
            }
        }

        private void button_up_Click(object sender, RoutedEventArgs e)
        {
            Position = Common.SlideupPosition(Position);
            button_generate_Click(sender, e);
        }

        private void button_down_Click(object sender, RoutedEventArgs e)
        {
            Position = Common.SlidedownPosition(Position);
            button_generate_Click(sender, e);
        }
    }
}
