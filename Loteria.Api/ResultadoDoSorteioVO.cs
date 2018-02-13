using System.Collections.Generic;

namespace Loteria.Api
{
    public class ResultadoDoSorteioVO
    {
        public string Premio { get; set; }
        public IEnumerable<BilheteMegaSenaVO> Bilhetes { get; set; }
    } 
}