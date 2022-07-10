using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Forms;

namespace TGDGenerator
{
    internal class ChordSound
    {
        private string[,] FretboardSound = { {"E4","F4","F#4","G4","G#4","A4","A#4","B4","C5","C#5","D5","D#5","E5", "F5", "F#5", "G5", "G#5", "A5", "A#5", "B5", "C6", "C#6", "D6", "D#6" },
                                        {"A4","A#4","B4","C5","C#5","D5","D#5","E5", "F5", "F#5", "G5", "G#5","A5","A#5","B5","C6","C#6","D6","D#6","E6", "F6", "F#6", "G6", "G#6" },
                                        {"D5","D#5","E5", "F5", "F#5", "G5", "G#5","A5","A#5","B5","C6","C#6","D6","D#6","E6", "F6", "F#6", "G6", "G#6","A6","A#6","B6","C7","C#7" },
                                        {"G5", "G#5","A5","A#5","B5","C6","C#6","D6","D#6","E6", "F6", "F#6","G6", "G#6","A6","A#6","B6","C7","C#7","D7","D#7","E7", "F7", "F#7" },
                                        {"B5","C6","C#6","D6","D#6","E6", "F6", "F#6", "G6", "G#6","A6","A#6","B6","C7","C#7","D7","D#7","E7", "F7", "F#7", "G7", "G#7","","" },
                                        {"E6","F6","F#6","G6","G#6","A6","A#6","B6","C7","C#7","D7","D#7","E7", "F7", "F#7", "G7", "G#7", "", "", "", "", "", "", ""  } };

        private string Position { get; set; }
        private string[]? PositionArray { get; set; }

        private int Capo { get; set; }

        private static WaveOutEvent outputDevice0 = new WaveOutEvent();
        private static WaveOutEvent outputDevice1 = new WaveOutEvent();
        private static WaveOutEvent outputDevice2 = new WaveOutEvent();
        private static WaveOutEvent outputDevice3 = new WaveOutEvent();
        private static WaveOutEvent outputDevice4 = new WaveOutEvent();
        private static WaveOutEvent outputDevice5 = new WaveOutEvent();


        public ChordSound(string position, int capo)
        {
            Position = position;
            Capo = capo;

            if (Position != null)
            {
                PositionArray = Position.Split(",");
            }

            //WaveOutEvent outputDevice = new WaveOutEvent();
            //AudioFileReader afr = new AudioFileReader(@"E:\temp\guitar\C5.mp3");
            //outputDevice.Init(afr);
            //outputDevice.Play();

            //WaveOutEvent outputDevice2 = new WaveOutEvent();
            //AudioFileReader afr2 = new AudioFileReader(@"E:\temp\guitar\B5.mp3");
            //outputDevice2.Init(afr2);
            //outputDevice2.Play();

            //WaveOutEvent outputDevice3 = new WaveOutEvent();
            //AudioFileReader afr3 = new AudioFileReader(@"E:\temp\guitar\E6.mp3");
            //outputDevice3.Init(afr3);
            //outputDevice3.Play();

        }

        public async void PlaySound()
        {
            
            if (PositionArray != null)
            {
                string path = Application.ExecutablePath;
                if (path.EndsWith('\\'))
                {
                    path += "..\\Sound\\";
                }
                else
                {
                    path += "\\..\\Sound\\";
                }


                //１音目------------------------------------------------------------

                try
                {
                    //6弦
                    if (PositionArray[0].Split("-")[0] != "x")
                    {
                        outputDevice0.Dispose();
                        //WaveOutEvent outputDevice0 = new WaveOutEvent();
                        if (FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr0 = new AudioFileReader(path + FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[0])] + ".mp3");
                            outputDevice0.Init(afr0);
                            outputDevice0.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //5弦
                    if (PositionArray[1].Split("-")[0] != "x")
                    {
                        outputDevice1.Dispose();
                        //WaveOutEvent outputDevice1 = new WaveOutEvent();
                        if (FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr1 = new AudioFileReader(path + FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[0])] + ".mp3");
                            outputDevice1.Init(afr1);
                            outputDevice1.Play();
                        }
                    }
                }
                catch
                {

                }


                try
                {
                    //4弦
                    if (PositionArray[2].Split("-")[0] != "x")
                    {
                        outputDevice2.Dispose();
                        //WaveOutEvent outputDevice2 = new WaveOutEvent();
                        if (FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr2 = new AudioFileReader(path + FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[0])] + ".mp3");
                            outputDevice2.Init(afr2);
                            outputDevice2.Play();
                        }
                    }
                }
                catch
                {

                }


                try
                {
                    //3弦
                    if (PositionArray[3].Split("-")[0] != "x")
                    {
                        outputDevice3.Dispose();
                        //WaveOutEvent outputDevice3 = new WaveOutEvent();
                        if (FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr3 = new AudioFileReader(path + FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[0])] + ".mp3");
                            outputDevice3.Init(afr3);
                            outputDevice3.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //2弦
                    if (PositionArray[4].Split("-")[0] != "x")
                    {
                        outputDevice4.Dispose();
                        //WaveOutEvent outputDevice4 = new WaveOutEvent();
                        if (FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr4 = new AudioFileReader(path + FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[0])] + ".mp3");
                            outputDevice4.Init(afr4);
                            outputDevice4.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //1弦
                    if (PositionArray[5].Split("-")[0] != "x")
                    {
                        outputDevice5.Dispose();
                        //WaveOutEvent outputDevice5 = new WaveOutEvent();
                        if (FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[0])] != String.Empty)
                        {
                            AudioFileReader afr5 = new AudioFileReader(path + FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[0])] + ".mp3");
                            outputDevice5.Init(afr5);
                            outputDevice5.Play();
                        }
                    }
                }
                catch
                {

                }

                //------------------------------------------------------------------------


                //２音目------------------------------------------------------------

                await Task.Delay(300);

                try
                {
                    //6弦
                    if (PositionArray[0].Split("-")[1] != null)
                    {
                        outputDevice0.Dispose();
                        //WaveOutEvent outputDevice0 = new WaveOutEvent();
                        if (FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr0 = new AudioFileReader(path + FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[1])] + ".mp3");
                            outputDevice0.Init(afr0);
                            outputDevice0.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //5弦
                    if (PositionArray[1].Split("-")[1] != null)
                    {
                        outputDevice1.Dispose();
                        //WaveOutEvent outputDevice1 = new WaveOutEvent();
                        if (FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr1 = new AudioFileReader(path + FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[1])] + ".mp3");
                            outputDevice1.Init(afr1);
                            outputDevice1.Play();
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    //4弦
                    if (PositionArray[2].Split("-")[1] != null)
                    {
                        outputDevice2.Dispose();
                        //WaveOutEvent outputDevice2 = new WaveOutEvent();
                        if (FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr2 = new AudioFileReader(path + FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[1])] + ".mp3");
                            outputDevice2.Init(afr2);
                            outputDevice2.Play();
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    //3弦
                    if (PositionArray[3].Split("-")[1] != null)
                    {
                        outputDevice3.Dispose();
                        //WaveOutEvent outputDevice3 = new WaveOutEvent();
                        if (FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr3 = new AudioFileReader(path + FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[1])] + ".mp3");
                            outputDevice3.Init(afr3);
                            outputDevice3.Play();
                        }
                    }

                }
                catch
                {

                }
                try
                {
                    //2弦
                    if (PositionArray[4].Split("-")[1] != null)
                    {
                        outputDevice4.Dispose();
                        //WaveOutEvent outputDevice4 = new WaveOutEvent();
                        if (FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr4 = new AudioFileReader(path + FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[1])] + ".mp3");
                            outputDevice4.Init(afr4);
                            outputDevice4.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //1弦
                    if (PositionArray[5].Split("-")[1] != null)
                    {
                        outputDevice5.Dispose();
                        //WaveOutEvent outputDevice5 = new WaveOutEvent();
                        if (FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[1])] != String.Empty)
                        {
                            AudioFileReader afr5 = new AudioFileReader(path + FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[1])] + ".mp3");
                            outputDevice5.Init(afr5);
                            outputDevice5.Play();
                        }
                    }

                }
                catch
                {

                }


                //------------------------------------------------------------------------



                //３音目------------------------------------------------------------

                await Task.Delay(300);

                try
                {
                    //6弦
                    if (PositionArray[0].Split("-")[2] != null)
                    {
                        outputDevice0.Dispose();
                        //WaveOutEvent outputDevice0 = new WaveOutEvent();
                        if (FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr0 = new AudioFileReader(path + FretboardSound[0, Capo + int.Parse(PositionArray[0].Split("-")[2])] + ".mp3");
                            outputDevice0.Init(afr0);
                            outputDevice0.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //5弦
                    if (PositionArray[1].Split("-")[2] != null)
                    {
                        outputDevice1.Dispose();
                        //WaveOutEvent outputDevice1 = new WaveOutEvent();
                        if (FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr1 = new AudioFileReader(path + FretboardSound[1, Capo + int.Parse(PositionArray[1].Split("-")[2])] + ".mp3");
                            outputDevice1.Init(afr1);
                            outputDevice1.Play();
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    //4弦
                    if (PositionArray[2].Split("-")[2] != null)
                    {
                        outputDevice2.Dispose();
                        //WaveOutEvent outputDevice2 = new WaveOutEvent();
                        if (FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr2 = new AudioFileReader(path + FretboardSound[2, Capo + int.Parse(PositionArray[2].Split("-")[2])] + ".mp3");
                            outputDevice2.Init(afr2);
                            outputDevice2.Play();
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    //3弦
                    if (PositionArray[3].Split("-")[2] != null)
                    {
                        outputDevice3.Dispose();
                        //WaveOutEvent outputDevice3 = new WaveOutEvent();
                        if (FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr3 = new AudioFileReader(path + FretboardSound[3, Capo + int.Parse(PositionArray[3].Split("-")[2])] + ".mp3");
                            outputDevice3.Init(afr3);
                            outputDevice3.Play();
                        }
                    }

                }
                catch
                {

                }
                try
                {
                    //2弦
                    if (PositionArray[4].Split("-")[2] != null)
                    {
                        outputDevice4.Dispose();
                        //WaveOutEvent outputDevice4 = new WaveOutEvent();
                        if (FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr4 = new AudioFileReader(path + FretboardSound[4, Capo + int.Parse(PositionArray[4].Split("-")[2])] + ".mp3");
                            outputDevice4.Init(afr4);
                            outputDevice4.Play();
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    //1弦
                    if (PositionArray[5].Split("-")[2] != null)
                    {
                        outputDevice5.Dispose();
                        //WaveOutEvent outputDevice5 = new WaveOutEvent();
                        if (FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[2])] != String.Empty)
                        {
                            AudioFileReader afr5 = new AudioFileReader(path + FretboardSound[5, Capo + int.Parse(PositionArray[5].Split("-")[2])] + ".mp3");
                            outputDevice5.Init(afr5);
                            outputDevice5.Play();
                        }
                    }

                }
                catch
                {

                }


                //------------------------------------------------------------------------

            }


        }


    }
}
