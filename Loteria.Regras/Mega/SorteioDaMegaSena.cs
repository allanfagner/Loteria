using System;
using System.Collections.Generic;

namespace Loteria.Mega
{
    public class SorteadorDaMegaSena : ISorteio<MegaSena>
    {
        public MegaSena Sortear()
        {
            //Neste caso, podemos apenas retornar um bilhete como sendo os números vencedores
            //já que o construtor padrão gera uma sequência aletória
            return new MegaSena();
        }
    }
}
