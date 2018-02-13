using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loteria;
using Loteria.Mega;

namespace Loteria.Api.Controllers
{
    //Não estou utilizando injeção de dependência por simplicidade
    //Havendo a necissidade posesmo implementá-la
    [Route("api/concurso")]
    public class ConcursoController : Controller
    {
        private Concurso<MegaSena> concurso 
            = new Concurso<MegaSena>(new SorteadorDaMegaSena());

        [HttpPost]
        [Route("megasena/bilhete")]
        public IActionResult Participar([FromBody]BilheteMegaSenaVO aposta)
        {
            if (aposta == null) throw new Exception("Aposta não informada ou inválida");
            
            var bilhete = new MegaSena(aposta.Dezena1, aposta.Dezena2, aposta.Dezena3,
                aposta.Dezena4, aposta.Dezena5, aposta.Dezena6);

            concurso.Participar(bilhete);

            return Json(aposta);
        }

        [HttpPost]
        [Route("megasena/bilhete/surpresa")]
        public IActionResult Surpresinha()
        {
            BilheteMegaSenaVO aposta = new BilheteMegaSenaVO();
            MegaSena bilhete = new MegaSena();

            aposta.Dezena1 = bilhete.Dezena1;
            aposta.Dezena2 = bilhete.Dezena2;
            aposta.Dezena3 = bilhete.Dezena3;
            aposta.Dezena4 = bilhete.Dezena4;
            aposta.Dezena5 = bilhete.Dezena5;
            aposta.Dezena6 = bilhete.Dezena6;

            concurso.Participar(bilhete);

            return Json(aposta);
        }

        [HttpPost]
        [Route("megasena/sorteio")]
        public IActionResult Sortear() 
        {
            var resultado = concurso.Sortear();
            var resultadoResponse = new List<ResultadoDoSorteioVO>();
            List<BilheteMegaSenaVO> bilhetesPremiados;
            List<ResultadoDoSorteioVO> resultadoVO = new List<ResultadoDoSorteioVO>();


            foreach (var r in resultado)
            {
                bilhetesPremiados = new List<BilheteMegaSenaVO>();

                foreach (MegaSena v in r.Value)
                {
                    bilhetesPremiados.Add(new BilheteMegaSenaVO
                    {
                        Dezena1 = v.Dezena1,
                        Dezena2 = v.Dezena2,
                        Dezena3 = v.Dezena3,
                        Dezena4 = v.Dezena4,
                        Dezena5 = v.Dezena5,
                        Dezena6 = v.Dezena6
                    });
                }

                resultadoVO.Add(new ResultadoDoSorteioVO
                {
                    Premio = r.Key,
                    Bilhetes = bilhetesPremiados
                });
            }

            return Json(resultadoVO);
        }
    }
}
