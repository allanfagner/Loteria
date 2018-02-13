using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loteria;
using Loteria.Mega;

namespace Loteria.Api.Controllers
{
    [Route("api/concurso")]
    public class ConcursoController : Controller
    {
        private Concurso<MegaSena> concurso 
            = new Concurso<MegaSena>(new SorteadorDaMegaSena());

        // GET api/values/5
        [HttpGet]
        [Route("megasena")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("megasena/bilhete")]
        public IActionResult Participar(BilheteMegaSenaRequest bilhete) 
        {
            concurso.Participar(new MegaSena(bilhete.Dezena1, bilhete.Dezena2, bilhete.Dezena3,
                bilhete.Dezena4, bilhete.Dezena5, bilhete.Dezena6));

            return new ObjectResult("Bilhete recebido");
        }

        [HttpPost]
        [Route("megasena/sorteio")]
        public IActionResult Sortear() 
        {
            var resultado = concurso.Sortear();

            return new ObjectResult(resultado);
        }

        [HttpPost]
        [Route("test")]
        public IActionResult Test(string test) 
        {
            return new ObjectResult($"test - {test}");
            //return new ObjectResult(bilhete);
        }
    }
}
