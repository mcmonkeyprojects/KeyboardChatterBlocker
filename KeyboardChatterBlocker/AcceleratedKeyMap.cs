using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardChatterBlocker
{
    /// <summary>
    /// Represents essentially a Dictionary[Key, T] with a special case for low range keys.
    /// </summary>
    /// <typeparam name="T">The type of value in the Dictionary.</typeparam>
    public class AcceleratedKeyMap<T>
    {
        /// <summary>
        /// Linear array used to accelerate lookups.
        /// </summary>
        public T[] AccelerationArray = new T[1024];

        /// <summary>
        /// Full dictionary mapping.
        /// </summary>
        public Dictionary<Keys, T> MainDictionary = new Dictionary<Keys, T>(2048);

        /// <summary>
        /// Get or set a key-to-T mapping.
        /// </summary>
        /// <param name="key">The key index.</param>
        public T this[Keys key]
        {
            get
            {
                if ((int)key < 1024)
                {
                    return AccelerationArray[(int)key];
                }
                return MainDictionary.TryGetValue(key, out T result) ? result : default;
            }
            set
            {
                if ((int)key < 1024)
                {
                    AccelerationArray[(int)key] = value;
                }
                MainDictionary[key] = value;
            }
        }
    }
}
