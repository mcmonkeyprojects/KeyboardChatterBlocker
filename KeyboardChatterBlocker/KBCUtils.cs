using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Media;

namespace KeyboardChatterBlocker
{
    public static class KBCUtils
    {
        public static Action GetSoundPlayer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return () => { };
            }
            SoundPlayer player = new SoundPlayer
            {
                SoundLocation = filePath
            };
            return () => player.Play();
        }
    }
}
