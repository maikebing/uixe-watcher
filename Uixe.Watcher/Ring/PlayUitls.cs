using System;
using System.Media;
using System.Runtime.InteropServices;

namespace Uixe.Watcher.Ring
{
    public class PlayUitls : IDisposable
    {
        private SoundPlayer audioFileReader;

        public PlayUitls()
        {
        }

        public PlayUitls(string filename)
        {
            try
            {
                string sss = "Ring\\" + filename + ".wav";
                audioFileReader = new SoundPlayer(sss);
            }
            catch (Exception)
            {
                try
                {
                    GC.Collect();
                }
                catch (Exception)
                {
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern int Beep(int dwFreq, int dwDuration);

        public enum Music
        {
            Do = 523,
            Re = 587,
            Mi = 659,
            Fa = 698,
            So = 784,
            La = 880,
            Ti = 988,
            Do2 = 1046
        }
    
        public static string sfile;

        public static void SetMp3File(string file)
        {
            try
            {
                utils = new PlayUitls(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"tmNetworkTest_TickAsync{ex.Message}");
            }
        }

        private static PlayUitls utils;

        public static void PlayRing()
        {
            if (utils == null)
            {
                utils = new PlayUitls();
            }
            utils.Play();
        }

        public void Play()
        {
            try
            {
                audioFileReader?.PlaySync();
            }
            catch (Exception)
            {
            }
        }
 

        public void Dispose()
        {
            ((IDisposable)audioFileReader).Dispose();
        }
    }
}