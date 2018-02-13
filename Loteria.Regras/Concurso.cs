using System;
using System.Collections.Generic;

namespace Loteria
{
    public class Concurso<TJogo> where TJogo : IBilhete<TJogo> {
        //Para efito de demonstração estou mantando um lista estática com os jogos dentro desta classe
        //Em produção a lista pode ser mantida em banco e os dados jogos passados via construtor ou 
        //parâetro no método sortear
        private static List<TJogo> bilhetes = new List<TJogo>();
        private readonly ISorteio<TJogo> sorteador;

        public Concurso(ISorteio<TJogo> sorteador)
        {
            this.sorteador = sorteador;            
        }

        public void Participar(TJogo bilhete) {
            bilhetes.Add(bilhete);
        }

        //Por hora o retorno em string (Sena, Quina ou Quadra) é suficiente
        //Adicionando-se novos concursos (e entendendo a real necessidade) pode-se refatorar
        public Dictionary<string, List<IBilhete<TJogo>>> Sortear() {
            string chave;
            List<IBilhete<TJogo>> bilhtesPremiados;
            Dictionary<string, List<IBilhete<TJogo>>> ganhadores = new Dictionary<string, List<IBilhete<TJogo>>>();
            var numerosSorteados = sorteador.Sortear();

            foreach (var bilhete in bilhetes)
            {
                var resultado = bilhete.Conferir(numerosSorteados);

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
