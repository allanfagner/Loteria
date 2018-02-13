
using System;

namespace Loteria.Mega
{
    [Serializable]
    public struct DezenaMegaSena : IEquatable<DezenaMegaSena>, IComparable<DezenaMegaSena>
    {
        public const byte MinValue = 1;
        public const byte MaxValue = 60;
        private int dezena;
        public DezenaMegaSena(int dezena)
        {
            if (dezena < MinValue || dezena > MaxValue) throw new Exception($"A dezena da Mega Sena deve estar entre {MinValue} e {MaxValue}");

            this.dezena = dezena;
        }

        public static bool operator ==(DezenaMegaSena n1, DezenaMegaSena n2)
        {
            return n1.dezena == n2.dezena;
        }

        public static bool operator !=(DezenaMegaSena n1, DezenaMegaSena n2)
        {
            return !(n1.dezena == n2.dezena);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            
            
            return (this == (DezenaMegaSena)obj);
        }
                
        public override int GetHashCode()
        {
            return this.dezena.GetHashCode();
        }

        public bool Equals(DezenaMegaSena other)
        {
            return this.Equals((object)other);
        }

        public int CompareTo(DezenaMegaSena other)
        {
            var resultado = 0;

            if (this.dezena < other.dezena) 
            {
                resultado = -1;
            }
            else if (this.dezena > other.dezena)
            {
                    resultado = 1;
            }

            return  resultado;
        }

        public static implicit operator DezenaMegaSena(int dezena)
        {
            return new DezenaMegaSena(dezena);
        }

        public static implicit operator int(DezenaMegaSena dezena)
        {
            return dezena.dezena;
        }

        public override string ToString() 
        {
            return this.dezena.ToString();
        }        
    }
}