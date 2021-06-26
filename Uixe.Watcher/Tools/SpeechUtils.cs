using System.Speech.Synthesis;

namespace Uixe.Watcher
{
    public static class SpeechUtils
    {
        private static System.Speech.Synthesis.SpeechSynthesizer Speecher_ = null;

        public static System.Speech.Synthesis.SpeechSynthesizer Speecher
        {
            get
            {
                if (Speecher_ == null)
                {
                    Speecher_ = new SpeechSynthesizer();
                }
                return Speecher_;
            }
        }
    }
}