using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGDGenerator
{
    internal class Common
    {

        public static string[,] Fretboard = { {"E","F","F#","G","G#","A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#" },
                                        {"A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#","A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#" },
                                        {"D","D#","E", "F", "F#", "G", "G#","A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#","A","A#","B","C","C#" },
                                        {"G", "G#","A","A#","B","C","C#","D","D#","E", "F", "F#","G", "G#","A","A#","B","C","C#","D","D#","E", "F", "F#" },
                                        {"B","C","C#","D","D#","E", "F", "F#", "G", "G#","A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#","A","A#" },
                                        {"E","F","F#","G","G#","A","A#","B","C","C#","D","D#","E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#"  } };

        public static Dictionary<string, int> interval = new Dictionary<string, int>();
        
        public static void Initialize()
        {
            interval.Add("C", 0);
            interval.Add("C#", 1);
            interval.Add("D", 2);
            interval.Add("D#", 3);
            interval.Add("E", 4);
            interval.Add("F", 5);
            interval.Add("F#", 6);
            interval.Add("G", 7);
            interval.Add("G#", 8);
            interval.Add("A", 9);
            interval.Add("A#", 10);
            interval.Add("B", 11);
        }


        // 文字の出現回数をカウント
        public static int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        // 与えた文字列（全角・半角混じり）と、全体の半角分スペース数から、センタリングした文字列を返す
        public static string Centering(string s, int len)
        {


            
            int tmpGraphicalLength = 0;



            tmpGraphicalLength = GraphicalLength(s);

            
            int preSpace = (len - tmpGraphicalLength) / 2;
            int postSpace = len - (preSpace + tmpGraphicalLength);

            if (preSpace < 0) preSpace = 0;
            if (postSpace < 0) postSpace = 0;
           
            return (new string(' ', preSpace)) + s + (new string(' ', postSpace));
        }

        //与えた文字列（全角・半角混じり）の半角分スペース数を返す
        public static int GraphicalLength(string s)
        {
            //任意の文字列の、(バイト数-文字数)/2　が日本語の文字数と一致する。
            //これにより、与えられた文字列が英数字X文字、日本語Y文字と分かるので、
            //X+2Y個の半角幅が必要と分かる。

            Encoding utf8Enc = Encoding.GetEncoding("UTF-8");

            int tmpByteCount = 0;
            int tmpCharCount = 0;
            int tmpEngCount = 0;
            int tmpJpnCount = 0;

            tmpByteCount = utf8Enc.GetByteCount(s);
            tmpCharCount = s.Length;
            tmpJpnCount = (tmpByteCount - tmpCharCount) / 2;
            tmpEngCount = tmpCharCount - tmpJpnCount;
            return tmpEngCount + (tmpJpnCount * 2);
        }


        public static int RootIndex(string p)
        {
            if (p == "" || p == null) return 0;

            string? tmpBassNote = BassNote(p);
            if (tmpBassNote == null || tmpBassNote == "") return 0;

            return interval[tmpBassNote];
        }

        public static int NoteIndex(string note)
        {
            return interval[note];
        }


        public static string BassNote(string p)
        {
            string result = string.Empty;
            string[] parr = p.TrimEnd(new Char[] { ' ', ','}).Replace(" ","").Split(',');

            if (parr != null)
            {
                for (int i = 0; i < parr.Length; i++)
                {
                    if (parr[i] != "x" && int.Parse(parr[i]) <= 24)
                    {
                        result = Common.Fretboard[i, int.Parse(parr[i])];
                        break;
                    }

                }
            }
            return result;

        }


    }
}
