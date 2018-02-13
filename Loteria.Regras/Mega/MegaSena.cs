using System;
using System.Collections.Generic;
using System.Linq;

namespace Loteria.Mega
{
    public class MegaSena: IBilhete<MegaSena>
    {
        private static int idSequencial = 0;
        private readonly SortedSet<DezenaMegaSena> dezenas = new SortedSet<DezenaMegaSena>();
        private readonly int id;
        private readonly DateTime data;
        public MegaSena(DezenaMegaSena dezena1, DezenaMegaSena dezena2, DezenaMegaSena dezena3, DezenaMegaSena dezena4, DezenaMegaSena dezena5, DezenaMegaSena dezena6) {
            this.dezenas.Add(dezena1);
            this.dezenas.Add(dezena2);
            this.dezenas.Add(dezena3);
            this.dezenas.Add(dezena4);
            this.dezenas.Add(dezena5);
            this.dezenas.Add(dezena6);

            if (this.dezenas.Count < 6) throw new Exception("Todas as seis dezenas devem ser diferentes");            

            this.id = MegaSena.idSequencial++;
            this.data = DateTime.Now;
        }
        public MegaSena() {
            var gerador = new Random();

            while (this.dezenas.Count < 6) {
                this.dezenas.Add((byte)gerador.Next(1,61));
            }

            this.id = MegaSena.idSequencial++;
            this.data = DateTime.Now;
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
        private bool Sena(MegaSena resultado) => 
            this.dezenas.All(d => resultado.dezenas.Contains(d));
        private bool Quina(MegaSena resultado) =>
            this.dezenas.Where(d => resultado.dezenas.Contains(d)).Count() == 5;
        private bool Quadra(MegaSena resultado) =>

            this.dezenas.Where(d => resultado.dezenas.Contains(d)).Count() == 4;
        
        public override string ToString() => string.Join(", ", this.dezenas);        
    }
}
