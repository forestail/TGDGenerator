using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using Microsoft.VisualBasic;

namespace TGDGenerator
{
    internal class TGDiagram
    {
        private string? ChordName { get; set; }
        private string Position { get; set; }
        private string? Fingering { get; set; }

        private string[]? PositionArray { get; set; }

        private int? Root { get; set; }
       
        private string? Interval { get; set; }


        public TGDiagram(string? chordname, string position, string? fingering, int? root)
        {
            ChordName = chordname;
            //Position = Strings.StrConv(position, VbStrConv.Narrow, 0);
            //Fingering = Strings.StrConv(fingering, VbStrConv.Narrow, 0);
            //Position = "8,x,9,9,x,x";
            //Fingering = "1,x,3,2,x,x";

            Position = position;
            Fingering = fingering;
            
            if (Position != null)
            {
                PositionArray = Position.Split(",");
            }
            Root = root;

            

        }



        public string Stringize()
        {
            if (PositionArray == null) return "";

            string result = string.Empty;


            ////コードネーム
            //if (ChordName != null && ChordName.Trim().Length > 0)
            //{
            //    if (Common.GraphicalLength(ChordName) <= 12)
            //    {
            //        result += "  " + Common.Centering(ChordName, 12);
            //    }
            //    else if (Common.GraphicalLength(ChordName) == 13)
            //    {
            //        result += " " + Common.Centering(ChordName, 12);
            //    }
            //    else
            //    {
            //        result += Common.Centering(ChordName, 12);
            //    }
            //}


                        
            //result += "\r\n";



            //ダイアグラム生成

            //開放弦
            result += "  ";
            foreach (string str in PositionArray)
            {
                if (str != "x" && int.Parse(str) == 0)
                {
                    result += "●";
                }
                else
                {
                    result += "  ";
                }
            }

            result += "\r\n";
            result += "  ┌┬┬┬┬┐\r\n";


            for (int i = 0; i < 5; i++)
            {

                //最初のフレットにはフレット番号表示
                if (i == 0)
                {
                    if (MinFret().ToString().Length == 1)
                    {
                        result += " ";
                        result += MinFret();
                    }
                    else if (MinFret().ToString().Length == 2)
                    {
                        result += MinFret();
                    }
                    else
                    {
                        result += "  ";
                    }
                }
                else if (MinFret() + i == 3 || MinFret() + i == 5 || MinFret() + i == 9 || MinFret() + i == 15 || MinFret() + i == 17)
                {
                    result += " *";
                }
                else if (MinFret() + i == 7 || MinFret() + i == 12)
                {
                    result += "**";
                }
                else
                {
                    result += "  ";
                }


                foreach (string str in PositionArray)
                {
                    Console.WriteLine(i);
                    if (str != "x" && int.Parse(str) == MinFret() + i)
                    {
                        result += "●";
                    }
                    else
                    {
                        result += "│";
                    }
                }
                result += "\r\n  ├┼┼┼┼┤\r\n";

            }




            // フィンガリング
            if (Fingering != null && Regex.Replace(Fingering, @"[^0-9]", "") != "")
            {
                string tmpFingering = Regex.Replace(Fingering, @"[^0-9]", "");
                string fingerNum;
                result += "  ";
                foreach (string str in PositionArray)
                {
                    if (str != "x")
                    {
                        fingerNum = tmpFingering.Substring(0, 1);


                        result += fingerNum + " ";

                        tmpFingering = tmpFingering.Remove(0, 1);
                    }
                    else
                    {
                        result += "  ";
                    }
                }
                result += "\r\n";
            }

            //result += LetterNotations();



            return result;
        }

        public Boolean IsValid()
        {
            if (Position == null) return false;
            //if (PositionArray.Length > 7) return false;


            return true;
        }


        private int MinFret()
        {
            int result = 99;
            foreach (var item in PositionArray)
            {
                if (item != "x")
                {
                    int test = int.Parse(item);
                    if (test < result && test != 0)
                    {
                        result = test;
                    }
                }

                
            }
            if (result == 99) result = 0;

            return result;
        }


        private int MaxFret()
        {
            int result = 0;
            foreach (var item in PositionArray)
            {
                if (item != "x")
                {
                    int test = int.Parse(item);
                    if (test > result)
                    {
                        result = test;
                    }
                }
                

            }
            return result;
        }


        public string LetterNotations()
        {
            string result = string.Empty;
            if (PositionArray != null)
            {
                result += "  ";

                for (int i = 0; i < 6; i++)
                {
                    if (PositionArray[i] != "x")
                    {

                        result += Common.Fretboard[i, int.Parse(PositionArray[i])].PadRight(2);


                    }
                    else
                    {
                        result += "  ";
                    }
                }

                result += "\r\n";
            }

            

            return result;
        }



        public string Degrees()
        {
            string result = string.Empty;
            if (PositionArray != null)
            {
                result += "  ";

                int testNoteIndex;
                int rootNoteIndex;
                int dinterval;

                if (Root != null)
                {
                    rootNoteIndex = (int)Root;
                }
                else
                {
                    rootNoteIndex = Common.RootIndex(Position);
                }

                for (int i = 0; i < 6; i++)
                {
                    if (PositionArray[i] != "x")
                    {

                        testNoteIndex = Common.NoteIndex(Common.Fretboard[i, int.Parse(PositionArray[i])]);

                        if (testNoteIndex < rootNoteIndex)
                        {
                            dinterval = testNoteIndex + 12 - rootNoteIndex;
                        }
                        else
                        {
                            dinterval = testNoteIndex - rootNoteIndex;
                        }


                        switch (dinterval){
                            case 0:
                                result += "R ";
                                break;
                            case 1:
                                result += "b9";
                                break;
                            case 2:
                                result += "9 ";
                                break;
                            case 3:
                                result += "b3";
                                break;
                            case 4:
                                result += "3 ";
                                break;
                            case 5:
                                result += "4 ";
                                break;
                            case 6:
                                result += "-5";
                                break;
                            case 7:
                                result += "5 ";
                                break;
                            case 8:
                                result += "+5";
                                break;
                            case 9:
                                result += "6 ";
                                break;
                            case 10:
                                result += "7 ";
                                break;
                            case 11:
                                result += "M7";
                                break;
                        }

                    }
                    else
                    {
                        result += "  ";
                    }
                }

                result += "\r\n";
            }



            return result;



        }

        public void GetInterval()
        {
            string result = string.Empty;
            if (PositionArray != null)
            {

                int testNoteIndex;
                int rootNoteIndex;
                int dinterval;

                if (Root != null)
                {
                    rootNoteIndex = (int)Root;
                }
                else
                {
                    rootNoteIndex = Common.RootIndex(Position);
                }

                for (int i = 0; i < 6; i++)
                {
                    if (PositionArray[i] != "x")
                    {

                        testNoteIndex = Common.NoteIndex(Common.Fretboard[i, int.Parse(PositionArray[i])]);

                        if (testNoteIndex < rootNoteIndex)
                        {
                            dinterval = testNoteIndex + 12 - rootNoteIndex;
                        }
                        else
                        {
                            dinterval = testNoteIndex - rootNoteIndex;
                        }


                        result = result + "/" + dinterval + "/";


                    }

                }

            }

            Interval = result;

        }



        public string GetAutoName()
        {

            int rootNoteIndex;
            

            if (Root != null)
            {
                rootNoteIndex = (int)Root;
            }
            else
            {
                rootNoteIndex = Common.RootIndex(Position);
            }

            string result = Common.RootNameBase[rootNoteIndex];

            GetInterval();

            if (Interval != null)
            {
                if (Interval.Contains("/4/"))   //III
                {
                    if (Interval.Contains("/8/"))   //#V
                    {
                        result += "aug";
                        Interval = Interval.Replace("/4/", "");
                        Interval = Interval.Replace("/8/", "");
                    }
                    else
                    {
                        if (Interval.Contains("/7/"))   //V
                        {
                            //result += "maj";
                        }
                        else
                        {
                            //result += "maj";
                        }
                    }

                    if (Interval.Contains("/5/"))   //IV
                    {
                        result += "11";
                    }

                }
                else
                {
                    if (Interval.Contains("/3/"))   //bIII
                    {
                        if (Interval.Contains("/6/"))   //bV
                        {
                            result += "dim";
                            Interval = Interval.Replace("/3/", "");
                            Interval = Interval.Replace("/6/", "");

                            if (Interval.Contains("/9/"))   //VI
                            {
                                result += "7";
                                Interval = Interval.Replace("/9/","");
                            }
                        }
                        else
                        {
                            if (Interval.Contains("/7/"))   //V
                            {
                                result += "m";

                            }
                            else
                            {
                                //Vの省略
                                result += "m";
                            }
                        }

                        if (Interval.Contains("/5/"))   //IV
                        {
                            result += "11";
                            Interval = Interval.Replace("/5/", "");
                        }

                    }
                    else
                    {
                        if (Interval.Contains("/5/"))   //IV
                        {
                            result += "sus4";
                            Interval = Interval.Replace("/5/", "");
                        }
                    }
                }


                if (Interval.Contains("/10/"))  //7
                {
                    result += "7";
                    if (Interval.Contains("/8/"))   //#V
                    {
                        result += "b13";
                    }
                    if (Interval.Contains("/9/"))   //VI
                    {
                        result += "13";
                    }

                }
                else
                {
                    if (Interval.Contains("/9/"))   //VI
                    {
                        result += "6";
                    }
                }

                if (Interval.Contains("/11/"))  //M7
                {
                    result += "M7";
                }

                if (Interval.Contains("/1/"))   //b9
                {
                    result += "b9";
                }

                if (Interval.Contains("/2/"))   //9
                {
                    result += "9";
                }

                //bV判定
                if (Interval.Contains("/6/") && !Interval.Contains("/7/"))   //bVがあってVがない
                {
                    result += "b5";
                    Interval = Interval.Replace("/6/", "");
                }

                if (Interval.Contains("/6/"))   //bV
                {
                    result += "#11";
                }


            }

            

            return result;
        }

    }
}
