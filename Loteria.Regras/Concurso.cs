using System;
using System.Collections.Generic;

namespace Loteria
{
    public class Concurso<TJogo> where TJogo : IBilhete<TJogo> {
        private static List<TJogo> bilhetes = new List<TJogo>();
        private readonly TJogo numeroSorteados;

        public Concurso(ISorteio<TJogo> sorteador)
        {
            this.numeroSorteados = sorteador.Sortear();            
        }

        public void Participar(TJogo bilhete) {
            bilhetes.Add(bilhete);
        }

        public Dictionary<string, List<IBilhete<TJogo>>> Sortear() {
            string chave;
            List<IBilhete<TJogo>> bilhtesPremiados;
            Dictionary<string, List<IBilhete<TJogo>>> ganhadores = new Dictionary<string, List<IBilhete<TJogo>>>();

            foreach (var bilhete in bilhetes)
            {
                var resultado = bilhete.Conferir(numeroSorteados);

                if (resultado.Premiado) {
                    chave = resultado.Premio; 
                    if (!ganhadores.ContainsKey(chave)) {
                        bilhtesPremiados = new List<IBilhete<TJogo>>();
                        ganhadores.Add(chave, bilhtesPremiados);
                    }
                    ganhadores[chave].Add(resultado.Bilhete);
                }
            }

            //Como não estamos utilizando banco de dados
            //esta linha garante que o concurso não seja sorteado duas vezes
            //Utilizando-se banco uma estratégia mais elaborada pode ser implementada
            bilhetes.Clear();
            return ganhadores;
        }
    }
}
