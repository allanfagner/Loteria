using System;
using System.Collections.Generic;
using System.Linq;

namespace Loteria.Mega
{
    public class MegaSena: IBilhete<MegaSena>
    {
        private static int idSequencial = 0;
        private DezenaMegaSena[] dezenas = new DezenaMegaSena[6];
        private readonly int id;
        private readonly DateTime data;
        public MegaSena(DezenaMegaSena dezena1, DezenaMegaSena dezena2, DezenaMegaSena dezena3, DezenaMegaSena dezena4, DezenaMegaSena dezena5, DezenaMegaSena dezena6) {
            var dezenas = new SortedSet<DezenaMegaSena>();

            dezenas.Add(dezena1);
            dezenas.Add(dezena2);
            dezenas.Add(dezena3);
            dezenas.Add(dezena4);
            dezenas.Add(dezena5);
            dezenas.Add(dezena6);

            if (dezenas.Count < 6) throw new Exception("Todas as seis dezenas devem ser diferentes");            

            this.id = MegaSena.idSequencial++;
            this.data = DateTime.Now;
            this.dezenas = dezenas.ToArray();
        }
        public MegaSena() {
            var gerador = new Random();
            var dezenas = new SortedSet<DezenaMegaSena>();

            while (dezenas.Count < 6) {
                dezenas.Add((byte)gerador.Next(1,61));
            }

            this.id = MegaSena.idSequencial++;
            this.data = DateTime.Now;
            this.dezenas = dezenas.ToArray();
        }
        public Resultado<MegaSena> Conferir(MegaSena resultado)
        {
            var premio = "Sem Premio";

            if (this.Sena(resultado)) {
                premio = "Sena";
            }
            else if (this.Quina(resultado)) {
                premio = "Quina";
            }
            else if (this.Quadra(resultado)) {
                premio = "Quadra";
            }

            return new Resultado<MegaSena>(premio != "Sem Premio", premio, this);
        }

        public DezenaMegaSena Dezena1 => this.dezenas[0];
        public DezenaMegaSena Dezena2 => this.dezenas[1];
        public DezenaMegaSena Dezena3 => this.dezenas[2];
        public DezenaMegaSena Dezena4 => this.dezenas[3];
        public DezenaMegaSena Dezena5 => this.dezenas[4];
        public DezenaMegaSena Dezena6 => this.dezenas[5];


        //Como temos apenas uma classe podemos manter estas validações dentro da mesma
        //Havendo a necissdade podemos adotar um padrão similar ao Specification para encontrar o resultado
        //Essas novas classes de validação podem ser passadas via construtor
        private bool Sena(MegaSena resultado) => 
            this.dezenas.All(d => resultado.dezenas.Contains(d));
        private bool Quina(MegaSena resultado) =>
            this.dezenas.Where(d => resultado.dezenas.Contains(d)).Count() == 5;
        private bool Quadra(MegaSena resultado) =>

            this.dezenas.Where(d => resultado.dezenas.Contains(d)).Count() == 4;
        
        public override string ToString() => string.Join(", ", this.dezenas);        
    }
}
