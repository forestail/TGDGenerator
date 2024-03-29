﻿using System;
using System.Text.RegularExpressions;
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


            //if (Position == null) Position = "x,x,x,x,x,x";
            //if (Position == null) Position = "";
            

            //パラメータのサニタイズ
            SanitizeParameters();



            if (Position != null)
            {
                PositionArray = Position.Split(",");
            }
            Root = root;



        }

        public string GetPosition()
        {
            return Position;
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
                string[] tmpStr = str.Split("-");
                string tmpResult = string.Empty;

                for (int i = 0; i < tmpStr.Length; i++)
                {
                    if (i == 0)
                    {
                        if (tmpStr[i] != "x" && int.Parse(tmpStr[i]) == 0)
                        {
                            tmpResult = "●";
                        }
                        
                    }
                    else if (i == 1)
                    {
                        if (tmpStr[i] != "x" && int.Parse(tmpStr[i]) == 0)
                        {
                            //tmpResult = "×";
                            tmpResult = "Ｘ";
                        }
                        
                    }
                    else if (i == 2)
                    {
                        if (tmpStr[i] != "x" && int.Parse(tmpStr[i]) == 0)
                        {
                            tmpResult = "□";
                        }
                        
                    }
                }

                if (tmpResult == string.Empty)
                {
                    result += "  ";
                }
                else
                {
                    result += tmpResult;
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
                    string[] tmpStr = str.Split("-");
                    string tmpResult = string.Empty;

                    for (int j = 0; j < tmpStr.Length; j++)
                    {
                        if (j == 0)
                        {
                            if (tmpStr[j] != "x" && int.Parse(tmpStr[j]) == MinFret() + i)
                            {
                                tmpResult = "●";
                            }

                        }
                        else if (j == 1)
                        {
                            if (tmpStr[j] != "x" && int.Parse(tmpStr[j]) == MinFret() + i)
                            {
                                //tmpResult = "×";
                                tmpResult = "Ｘ";
                            }

                        }
                        else if (j == 2)
                        {
                            if (tmpStr[j] != "x" && int.Parse(tmpStr[j]) == MinFret() + i)
                            {
                                tmpResult = "□";
                            }

                        }
                    }

                    if (tmpResult == string.Empty)
                    {
                        result += "│";
                    }
                    else
                    {
                        result += tmpResult;
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
            if (Position == null || Position == "") return false;
            //if (PositionArray.Length > 7) return false;

            foreach (string p in PositionArray)
            {
                foreach (string pp in p.Split("-"))
                {
                    if (pp != "x")
                    {
                        if (int.Parse(pp) > 23) return false;
                    }
                }
            }

            return true;
        }


        private int MinFret()
        {
            int result = 9999;
            foreach (var item in PositionArray)
            {
                foreach (var subItem in item.Split("-"))
                {
                    if (subItem != "x")
                    {
                        int test = int.Parse(subItem);
                        if (test < result && test != 0)
                        {
                            result = test;
                        }
                    }
                }



            }
            if (result == 9999) result = 1;

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
                    if (PositionArray[i].Split("-")[0] != "x")
                    {

                        result += Common.Fretboard[i, int.Parse(PositionArray[i].Split("-")[0])].PadRight(2);


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
                    if (PositionArray[i].Split("-")[0] != "x")
                    {

                        testNoteIndex = Common.NoteIndex(Common.Fretboard[i, int.Parse(PositionArray[i].Split("-")[0])]);

                        if (testNoteIndex < rootNoteIndex)
                        {
                            dinterval = testNoteIndex + 12 - rootNoteIndex;
                        }
                        else
                        {
                            dinterval = testNoteIndex - rootNoteIndex;
                        }


                        switch (dinterval)
                        {
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
                    if (PositionArray[i].Split("-")[0] != "x")
                    {

                        testNoteIndex = Common.NoteIndex(Common.Fretboard[i, int.Parse(PositionArray[i].Split("-")[0])]);

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
                /*
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



                */




                Interval = Interval.Replace("/0/", "");
                //省略込み（Ｖ）
                //メジャーコード
                //if (Interval.Contains("/4/") && Interval.Contains("/7/"))   //IIIとVがある
                //if (Interval.Contains("/4/"))   //IIIがある
                if ((Interval.Contains("/4/") && Interval.Contains("/7/"))
                    || (Interval.Contains("/4/") && !Interval.Contains("/7/") && !Interval.Contains("/8/")))   //IIIとVがある　またはIIIがあって、Vも#Vもない
                {
                    Interval = Interval.Replace("/4/", "").Replace("/7/", "");

                    if (Interval == "")
                    {
                        result += "";
                    }

                    else if (Interval.Contains("/11/") && Interval.Contains("/2/") && Interval.Contains("/9/"))   //M7 9 13がある
                    {
                        Interval = Interval.Replace("/11/", "").Replace("/2/", "").Replace("/9/", "");
                        if (Interval == "") result += "Δ13";
                    }
                    else if (Interval.Contains("/11/") && Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/11/", "").Replace("/9/", "");
                        if (Interval == "") result += "Δ7/6";
                    }
                    else if (Interval.Contains("/2/") && Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/2/", "").Replace("/9/", "");
                        if (Interval == "") result += "6/9";
                    }
                    else if (Interval.Contains("/2/"))
                    {
                        Interval = Interval.Replace("/2/", "");
                        if (Interval == "") result += "add9";
                    }
                    else if (Interval.Contains("/2/") && Interval.Contains("/11/"))
                    {
                        Interval = Interval.Replace("/2/", "").Replace("/11/", "");
                        if (Interval == "") result += "Δ9";
                    }
                    else if (Interval.Contains("/11/"))
                    {
                        Interval = Interval.Replace("/11/", "");
                        if (Interval == "") result += "Δ7";
                    }
                    else if (Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/9/", "");
                        if (Interval == "") result += "6";
                    }




                    //ドミナント系
                    else if (Interval.Contains("/10/"))   //7がある
                    {
                        Interval = Interval.Replace("/10/", "");
                        if (Interval == "")
                        {
                            result += "7";
                        }

                        else if (Interval.Contains("/2/") && Interval.Contains("/5/") && Interval.Contains("/9/"))
                        {
                            Interval = Interval.Replace("/2/", "").Replace("/5/", "").Replace("/9/", "");
                            if (Interval == "") result += "11/13";
                        }
                        else if (Interval.Contains("/5/") && Interval.Contains("/9/"))
                        {
                            Interval = Interval.Replace("/5/", "").Replace("/9/", "");
                            if (Interval == "") result += "7/6/11";
                        }
                        else if (Interval.Contains("/2/") && Interval.Contains("/9/"))
                        {
                            Interval = Interval.Replace("/2/", "").Replace("/9/", "");
                            if (Interval == "") result += "13";
                        }
                        else if (Interval.Contains("/2/") && Interval.Contains("/5/"))
                        {
                            Interval = Interval.Replace("/2/", "").Replace("/5/", "");
                            if (Interval == "") result += "11";
                        }
                        else if (Interval.Contains("/2/"))
                        {
                            Interval = Interval.Replace("/2/", "");
                            if (Interval == "") result += "9";
                        }
                        else if (Interval.Contains("/5/"))
                        {
                            Interval = Interval.Replace("/5/", "");
                            if (Interval == "") result += "7/11";
                        }
                        else if (Interval.Contains("/9/"))
                        {
                            Interval = Interval.Replace("/9/", "");
                            if (Interval == "") result += "7/6";
                        }
                        else
                        {
                            result += "?";
                        }

                    }
                    else
                    {
                        result += "?";
                    }

                }
                //sus4系
                else if (Interval.Contains("/5/") && Interval.Contains("/7/"))   //bIIIとVがある
                {
                    Interval = Interval.Replace("/5/", "").Replace("/7/", "");

                    if (Interval.Contains("/10/") && Interval.Contains("/2/") && Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/10/", "").Replace("/2/", "").Replace("/9/", "");
                        if (Interval == "") result += "13 Sus";
                    }
                    else if (Interval.Contains("/9/") && Interval.Contains("/10/"))
                    {
                        Interval = Interval.Replace("/9/", "").Replace("/10/", "");
                        if (Interval == "") result += "7/6 Sus";
                    }
                    else if (Interval.Contains("/10/"))
                    {
                        Interval = Interval.Replace("/10/", "");
                        if (Interval == "") result += "7 Sus";
                    }
                    else
                    {
                        result += "?";
                    }

                }
                //マイナーコード
                //else if (Interval.Contains("/3/") && Interval.Contains("/7/"))   //bIIIとVがある
                //else if (Interval.Contains("/3/"))   //bIIIがある
                else if ((Interval.Contains("/3/") && Interval.Contains("/7/"))
                        || (Interval.Contains("/3/") && !Interval.Contains("/7/") && !Interval.Contains("/6/")))   //bIIIとVがある　またはbIIIがあって、VもbVもない
                {
                    Interval = Interval.Replace("/3/", "").Replace("/7/", "");
                    if (Interval == "")
                    {
                        result += "m";
                    }


                    else if (Interval.Contains("/11/") && Interval.Contains("/2/"))
                    {
                        Interval = Interval.Replace("/11/", "").Replace("/2/", "");
                        if (Interval == "") result += "mM7/9";
                    }
                    else if (Interval.Contains("/11/"))
                    {
                        Interval = Interval.Replace("/11/", "");
                        if (Interval == "") result += "mM7";
                    }
                    else if (Interval.Contains("/9/") && Interval.Contains("/2/"))
                    {
                        Interval = Interval.Replace("/9/", "").Replace("/2/", "");
                        if (Interval == "") result += "m 6/9";
                    }
                    else if (Interval.Contains("/2/"))
                    {
                        Interval = Interval.Replace("/2/", "");
                        if (Interval == "") result += "m add 9";
                    }
                    else if (Interval.Contains("/10/") && Interval.Contains("/5/"))
                    {
                        Interval = Interval.Replace("/10/", "").Replace("/5/", "");
                        if (Interval == "") result += "m7/11";
                    }
                    else if (Interval.Contains("/10/") && Interval.Contains("/2/") && Interval.Contains("/5/"))
                    {
                        Interval = Interval.Replace("/10/", "").Replace("/2/", "").Replace("/5/", "");
                        if (Interval == "") result += "m11";
                    }
                    else if (Interval.Contains("/10/") && Interval.Contains("/2/"))
                    {
                        Interval = Interval.Replace("/10/", "").Replace("/2/", "");
                        if (Interval == "") result += "m9";
                    }
                    else if (Interval.Contains("/10/"))
                    {
                        Interval = Interval.Replace("/10/", "");
                        if (Interval == "") result += "m7";
                    }
                    else if (Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/9/", "");
                        if (Interval == "") result += "m6";
                    }
                    else
                    {
                        result += "?";
                    }


                }
                //dimコード
                else if (Interval.Contains("/3/") && Interval.Contains("/6/"))   //bIIIとbVがある
                {
                    Interval = Interval.Replace("/3/", "").Replace("/6/", "");
                    if (Interval == "")
                    {
                        result += "dim";
                    }

                    else if (Interval.Contains("/9/"))
                    {
                        Interval = Interval.Replace("/9/", "");
                        if (Interval == "") result += "o";
                    }
                    else if (Interval.Contains("/10/"))
                    {
                        Interval = Interval.Replace("/10/", "");
                        if (Interval == "") result += "m7b5";
                    }
                    else
                    {
                        result += "?";
                    }

                }
                //augコード
                //else if (Interval.Contains("/4/") && Interval.Contains("/8/"))   //IIIと#Vがある
                else if (Interval.Contains("/8/"))   //IIIと#Vがある（IIIは省略も可）
                {
                    Interval = Interval.Replace("/4/", "").Replace("/8/", "");

                    if (Interval.Contains("/2/") && Interval.Contains("/11/"))
                    {
                        Interval = Interval.Replace("/2/", "").Replace("/11/", "");
                        if (Interval == "") result += "Δ9";
                    }
                    else if (Interval.Contains("/11/"))
                    {
                        Interval = Interval.Replace("/11/", "");
                        if (Interval == "") result += "Δ7";
                    }


                    result += "+";

                }
                else
                {
                    result += "?";
                }

            }


            return result;
        }


        public void SanitizeParameters()
        {
            //Positionパラメータのサニタイズ

            Position = Position.Trim(' ' , ',' , '　');


            if (Common.CountChar(Position, ',') == 0)
            {
                Position += ",x,x,x,x,x";
            }
            else if (Common.CountChar(Position, ',') == 1)
            {
                Position += ",x,x,x,x";
            }
            else if (Common.CountChar(Position, ',') == 2)
            {
                Position += ",x,x,x";
            }
            else if (Common.CountChar(Position, ',') == 3)
            {
                Position += ",x,x";
            }
            else if (Common.CountChar(Position, ',') == 4)
            {
                Position += ",x";
            }
            else if (Common.CountChar(Position, ',') == 5)
            {
                
            }
            else if (Common.CountChar(Position, ',') > 5)
            {
                Position = Position.Split(",")[0] + "," + Position.Split(",")[1] + "," + Position.Split(",")[2] + "," + Position.Split(",")[3] + "," + Position.Split(",")[4];
            }



            //string ptnPosition = @"^(x|[1-9][0-9]*(-[1-9][0-9]*)*),(x|[1-9][0-9]*(-[1-9][0-9]*)*),(x|[1-9][0-9]*(-[1-9][0-9]*)*),(x|[1-9][0-9]*(-[1-9][0-9]*)*),(x|[1-9][0-9]*(-[1-9][0-9]*)*),(x|[1-9][0-9]*(-[1-9][0-9]*)*)$";
            //string ptnPosition = @"^(x|[0-9]+(-[0-9]+)*),(x|[0-9]+(-[0-9]+)*),(x|[0-9]+(-[0-9]+)*),(x|[0-9]+(-[0-9]+)*),(x|[0-9]+(-[0-9]+)*),(x|[0-9]+(-[0-9]+)*)$";
            string ptnPosition = @"^(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*,(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*,(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*,(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*,(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*,(x|[0-9]{1,2})(-(x|[0-9]{1,2}))*$";
            Match mPst = Regex.Match(Position, ptnPosition);

            if (!mPst.Success)
            {
                Position = "";
            }

        }

    }
}
