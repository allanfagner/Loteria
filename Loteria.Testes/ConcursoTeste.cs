using System;
using Xunit;
using Loteria;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Loteria.Mega;

namespace Loteria.Testes
{
    public class ConcursoTeste
    {
        [Fact]
        public void Sortear_UmUnicoBilheteAcertadorDaSena_OBilheteRetonraVencedor()
        {
            var numerosPremiados = new MegaSena(1,2,3,4,5,6);
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            
            sorteito.Participar(new MegaSena(1,2,3,4,5,6));

            var acertadores = sorteito.Sortear();
            var bilhete = acertadores.First().Value.First();
                        
            Assert.Single(acertadores);
            Assert.Equal("Sena", acertadores.Keys.First());
            Assert.Single(acertadores.Values.First());
            Assert.Equal("1, 2, 3, 4, 5, 6", bilhete.ToString());
        }

        [Fact]
        public void Sortear_UmBilheteAcertadorDaSenaComOutrosSemPremio_OBilheteRetonraVencedor()
        {            
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(10,20,3,40,5,6));
            sorteito.Participar(new MegaSena(1,2,3,4,5,6));
            sorteito.Participar(new MegaSena(1,20,30,4,5,60));
            sorteito.Participar(new MegaSena(1,2,3,40,50,60));
            
            var acertadores = sorteito.Sortear();
            var bilhete = acertadores.First().Value.First();
                        
            Assert.Single(acertadores);
            Assert.Equal("Sena", acertadores.Keys.First());
            Assert.Single(acertadores.Values.First());
            Assert.Equal("1, 2, 3, 4, 5, 6", bilhete.ToString());
        }

        [Fact]
         public void Sortear_DoisBilhetesAcertadoresDaSenaComOutrosSemPremio_OBilheteRetonraVencedor()
        {
            var numerosPremiados = new MegaSena(1,2,3,4,5,6);
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(1,2,3,4,5,6));
            sorteito.Participar(new MegaSena(1,2,3,4,5,7));
            sorteito.Participar(new MegaSena(10,20,30,40,50,7));
            sorteito.Participar(new MegaSena(1,2,3,4,5,6));
            
            var resultado = sorteito.Sortear();
            
            Assert.Equal(2, resultado.Count);
            Assert.Equal("Sena", resultado.Keys.First());
            Assert.Equal(2, resultado.Values.First().Count);
        }

        [Fact]
        public void Sortear_UmBilheteAcertadorDaQuinaComOutrosSemPremio_OBilheteRetonraVencedor()
        {
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(1,2,3,4,5,7));
            sorteito.Participar(new MegaSena(1,2,3,7,8,9));
            sorteito.Participar(new MegaSena(1,2,3,9,10,11));
            
            var resultado = sorteito.Sortear();
            
            Assert.Single(resultado);
            Assert.Equal("Quina", resultado.Keys.First());
            Assert.Single(resultado.Values.First());
        }

        [Fact]
        public void Sortear_DoisBilhetesAcertadoresDaQuinaComOutrosSemPremio_OBilheteRetonraVencedor()
        {
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(1,2,3,4,5,7)); //vencedor
            sorteito.Participar(new MegaSena(1,20,3,4,50,60));
            sorteito.Participar(new MegaSena(1,25,32,4,5,60));
            sorteito.Participar(new MegaSena(10,2,3,4,5,6)); //vencedor
            sorteito.Participar(new MegaSena(7,2,3,14,57,6));

            var resultado = sorteito.Sortear();
            
            Assert.Equal(1, resultado.Count);
            Assert.Equal("Quina", resultado.Keys.First());
            Assert.Equal(2, resultado.Values.First().Count);
        }

        [Fact]
        public void Sortear_DoisBilhetesAcertadoresDaQuadraComOutrosSemPremio_OBilheteRetonraVencedor()
        {
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(1,2,3,4,10,9)); //vencedor
            sorteito.Participar(new MegaSena(1,20,3,4,50,60));
            sorteito.Participar(new MegaSena(1,25,32,4,5,60));
            sorteito.Participar(new MegaSena(10,2,30,4,5,6)); //vencedor
            sorteito.Participar(new MegaSena(7,2,3,14,57,6));
            
            var resultado = sorteito.Sortear();
            
            Assert.Equal(1, resultado.Count);
            Assert.Equal("Quadra", resultado.Keys.First());
            Assert.Equal(2, resultado.Values.First().Count);
        }

        [Fact]
        public void Sortear_SenaEQuina_RetornaOsDoisBilhetesPremiados()
        {
            var sorteito = new Concurso<MegaSena>(new SorteioDaMegaSenaDouble());
            sorteito.Participar(new MegaSena(1,2,3,4,5,6)); //vencedor
            sorteito.Participar(new MegaSena(1,20,3,4,50,60));
            sorteito.Participar(new MegaSena(1,25,32,4,5,60));
            sorteito.Participar(new MegaSena(10,2,30,4,5,6)); //vencedor
            sorteito.Participar(new MegaSena(7,2,3,14,57,6));

            var resultado = sorteito.Sortear();
            
            Assert.Equal(2, resultado.Count);
            Assert.Equal("Quadra,Sena", string.Join(",", resultado.Keys.OrderBy(k => k)));
        }
        
        public class SorteioDaMegaSenaDouble : ISorteio<MegaSena>
        {
            public MegaSena Sortear()
            {
                return new MegaSena(1,2,3,4,5,6);
            }
        }
        
    }
}
