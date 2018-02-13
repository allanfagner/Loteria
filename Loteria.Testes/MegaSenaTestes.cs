using System;
using Xunit;
using Loteria;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Loteria.Mega;

namespace Loteria.Testes
{
    public class MegaSenaTestes
    {
        [Fact]
        public void CriarBilheteDaMegaSena_SemNumero_UmBilheteComNumeracaoAletoriaEGerado() {
            var megaSena = new MegaSena();            
            
            Assert.Matches("^(((0[1-9]|[1-9])|[1-5][0-9]|60), ){5}([1-9]|[1-5][0-9]|60)$", megaSena.ToString());
                     
        }

        [Theory]
        [InlineData(55,56,57,58,59,60,"55, 56, 57, 58, 59, 60")]
        [InlineData(52,34,27,59,12,7,"7, 12, 27, 34, 52, 59")]
        public void CriarBilheteDaMegaSena_ComNumerosValidos_CriaOBilheteComOsNumerosEmOrder (
            int d1, int d2, int d3, int d4, int d5, int d6, string esperado) {
            var megaSena = new MegaSena(d1, d2, d3, d4, d5, d6);

            Assert.Equal(esperado, megaSena.ToString());
        }

        [Theory]
        [InlineData(1,2,3,4,5,5)]
        [InlineData(1,2,3,4,4,5)]
        [InlineData(1,2,3,3,4,5)]
        [InlineData(1,2,2,3,4,5)]
        [InlineData(1,1,3,3,4,5)]
        [InlineData(1,2,3,4,5,1)]
        public void CriarBilheteDaMegaSena_ComNumerosRepetidos_LancaExcecao(int d1, int d2, int d3, int d4, int d5, int d6)
        {
            var ex = Assert.Throws<Exception>(() => new MegaSena(d1, d2, d3, d4, d5, d6));

            Assert.Equal("Todas as seis dezenas devem ser diferentes", ex.Message);
        }

        [Fact]
        public void Conferir_Sena_PremiadoComSena()
        {
            var bilhete = new Mega.MegaSena(10,20,30,40,50,60);
            var resultado = new Mega.MegaSena(10,20,30,40,50,60);

            var conferido = bilhete.Conferir(resultado);

            Assert.True(conferido.Premiado);
            Assert.Equal("Sena", conferido.Premio);
            Assert.True(bilhete == conferido.Bilhete);
        }

        [Theory]
        [InlineData(5,10,15,20,30,49)]
        [InlineData(5,10,15,20,37,40)]
        [InlineData(5,10,15,24,30,40)]
        [InlineData(5,10,19,20,30,40)]
        [InlineData(5,9,15,20,30,40)]
        [InlineData(1,10,15,20,30,40)]
        public void Conferir_Quina_PremiadoComQuina(int d1, int d2, int d3, int d4, int d5, int d6)
        {
            var bilhete = new Mega.MegaSena(d1,d2,d3,d4,d5,d6);
            var resultado = new Mega.MegaSena(5,10,15,20,30,40);

            var conferido = bilhete.Conferir(resultado);

            Assert.True(conferido.Premiado);
            Assert.Equal("Quina", conferido.Premio);
            Assert.True(bilhete == conferido.Bilhete);
        }

        [Theory]
        [InlineData(5,10,15,20,37,58)]
        [InlineData(5,10,15,19,32,40)]
        [InlineData(5,10,17,24,30,40)]
        [InlineData(5,11,12,20,30,40)]
        [InlineData(3,7,15,20,30,40)]
        [InlineData(1,10,15,20,30,60)]
        public void Conferir_Quina_PremiadoComQuadra(int d1, int d2, int d3, int d4, int d5, int d6)
        {
            var bilhete = new Mega.MegaSena(d1,d2,d3,d4,d5,d6);
            var resultado = new Mega.MegaSena(5,10,15,20,30,40);

            var conferido = bilhete.Conferir(resultado);

            Assert.True(conferido.Premiado);
            Assert.Equal("Quadra", conferido.Premio);
            Assert.True(bilhete == conferido.Bilhete);
        }
    }
}
