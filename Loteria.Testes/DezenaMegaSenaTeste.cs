using System;
using Loteria.Mega;
using Xunit;

namespace Loteria.Testes
{
    public class DezenaMegaSenaTeste
    {
        [Fact]
        public void DezenaMegaSena_Compracoes() 
        {
            var d1 = new DezenaMegaSena(1);
            var d2 = new DezenaMegaSena(1);

            Assert.True(d1 ==  d2);
            Assert.False(d1 !=  d2);
            Assert.True(d1.Equals(d2));
            Assert.True(d1.Equals((object)d2));
            Assert.False(d1.Equals(null));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(12)]
        [InlineData(25)]
        [InlineData(48)]
        [InlineData(59)]
        [InlineData(60)]
        public void CriarDezenaMegaSena_ComDezenaValido_CriaTipoCorretamente(int dezena)
        {
            var dezenaMega = new DezenaMegaSena(dezena);

            Assert.Equal(dezena, (int)dezenaMega);            
        }


        [Theory]        
        [InlineData(0)]
        [InlineData(61)]        
        public void CriarDezenaMegaSena_ComDezenaInvalida_LancaExcecao(int dezena)
        {
            var ex = Assert.Throws<Exception>(() => new DezenaMegaSena(dezena));

            Assert.Equal($"A dezena da Mega Sena deve estar entre {DezenaMegaSena.MinValue} e {DezenaMegaSena.MaxValue}", ex.Message);
        }
    }
}