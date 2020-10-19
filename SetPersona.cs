using System;
using System.Collections.Generic;
using System.Text;

namespace Registro
{
   public class SetPersona
    {
        private Persona[][] _personas = new Persona[27][];
        
        public SetPersona()
        {
            for (int i = 0; i < _personas.Length; i++)
            {
                _personas[i] = new Persona[10];
            }
        }

        public bool Add(Persona persona)
        {
            if (Contains(persona))
            {
                return false;
            }
            bool filledBucket = false;
            int index = persona.GetHashCode() % _personas.Length;
            Persona[] bucket = _personas[index];
            for (int i = 0; i < bucket.Length; i++)
            {
                if(bucket[i] == null)
                {
                    bucket[i] = persona;
                    filledBucket = true;
                    break;
                }
                if (!filledBucket)
                {
                    ExtendBucket(index);
                    _personas[index][bucket.Length] = persona;
                }
            }

            return true;
        }

        private void ExtendBucket(int index)
        {
            int oldLength = _personas[index].Length;
            int newLength = (int)(oldLength *1.5);
            Persona[] newBucket = new Persona[newLength];
            for (int i = 0; i < oldLength; i++)
            {
                newBucket[i] = _personas[index][i];
            }
        }

        private bool Contains(Persona persona)
        {
            int bucketIndex = Math.Abs(persona.GetHashCode()) % _personas.Length;
            Persona[] bucket = _personas[bucketIndex];
            for (int i = 0; i < bucket.Length; i++)
            {
                if (persona.Equals(bucket[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
