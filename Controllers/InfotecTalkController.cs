using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.Extensions.Configuration;

namespace Infotec5Hub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfotecTalkController : ControllerBase
    {
        private readonly ILogger<InfotecTalkController> _logger;
        private readonly IConfiguration _configuration;

        public InfotecTalkController(ILogger<InfotecTalkController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
        }

        [HttpGet]
        public object Get()
        {
            return Ok("Integração Infotec X 5Hub");
        }
        
        [HttpGet("cliente/cpf/{id}")]
        public object GetCpf(string id)
        {
            Clientes cliente;
            using (var context = new InfotecContext(_configuration))
            {
                cliente = context.Clientes.AsNoTracking()
                      .FirstOrDefault(c =>  Equals(c.CPF_CNPJ, id));

                if (cliente != null)
                {
                    ObterOperador(context, cliente);
                    return Ok(cliente);
                }

            }

            return NotFound();
        }

        private static void ObterOperador(InfotecContext context, Clientes cliente)
        {
            var campanha = context.CampanhasClientes.AsNoTracking()
                .FirstOrDefault(c => Equals(c.CLIENTE, cliente.CODIGO) && Equals(c.CONCLUIDO, "NAO"));

            if (campanha != null)
            {
                var ope = context.Operadores.AsNoTracking()
                    .FirstOrDefault(c => Equals(c.CODIGO, campanha.OPERADOR));

                if (ope != null)
                {
                    cliente.OPERADOR_NOME = ope.NOME;
                    cliente.OPERADOR = ope.CODIGO;
                }
            }
        }

        [HttpGet("cliente/fone/{ddd}/{fone}")]
        public object GetFone(int ddd, string fone)
        {
            Clientes cliente;
            using (var context = new InfotecContext(_configuration))
            {
                cliente = context.Clientes.AsNoTracking()
                    .FirstOrDefault(c => Equals(c.FONE1, fone) && Equals(c.AREA1, ddd));

                if (cliente != null)
                {
                    ObterOperador(context, cliente);
                    return Ok(cliente);
                }

            }

            return NotFound();
        }

         [HttpGet("cliente/email/{email}")]
        public object GetFone(string email)
        {
            Clientes cliente;
            using (var context = new InfotecContext(_configuration))
            {
                cliente = context.Clientes.AsNoTracking()
                    .FirstOrDefault(c => Equals(c.EMAIL, email));

                if (cliente != null)
                {
                    ObterOperador(context, cliente);
                    return Ok(cliente);
                }

            }

            return NotFound();
        }

        private bool AddClientesOmni(InfotecContext context, InfotecTalk infotecTalk)
        {
            if (context.ClientesOmni.AsNoTracking().Any(c => Equals(c.talk_id, infotecTalk.id))) return false;

            context.ClientesOmni.Add(
                new ClientesOmni
                {
                    talk_id = infotecTalk.id,
                    customer_id = infotecTalk.customer_id,
                    channel_id = infotecTalk.channel_id,
                    finished_at = Convert.ToDateTime(infotecTalk.finished_at),
                    created_at = Convert.ToDateTime(infotecTalk.created_at),
                    json = JsonSerializer.Serialize(infotecTalk)
                }
            );
            
            return true;
        }

        private bool AddClientesOmniMsg(InfotecContext context, int talkid, TalkHistory talkHistory)
        {
            if (context.ClientesOmniMsg.AsNoTracking().Any(c => Equals(c.id, talkHistory.id))) return false;

            context.ClientesOmniMsg.Add(
                new ClientesOmniMsg
                {
                    id = talkHistory.id,
                    talk_id = talkid,
                    user_id = talkHistory.user_id,
                    channel_id = talkHistory.channel_id,
                    message = talkHistory.message,
                    type = talkHistory.type,
                    sent_at = Convert.ToDateTime(talkHistory.sent_at)
                }
            );
            
            return true;
        }

        [HttpPost]
        public ActionResult<InfotecTalk> Post(InfotecTalk infotecTalk)
        {
            try
            {
                var contador = 0;
                using (var context = new InfotecContext(_configuration))
                {
                    if (AddClientesOmni(context, infotecTalk))
                        contador++;

                    infotecTalk.talk_histories
                        .ForEach(h =>
                        {
                            if (AddClientesOmniMsg(context, infotecTalk.id, h)) contador++;
                        });

                    context.SaveChanges();
                }
                
                _logger.LogInformation($"Foram incluidas {contador} mensagens.");

                return Ok(infotecTalk);
            }
            catch (Exception error)
            {
                _logger.LogError(error.InnerException != null ? error.InnerException.Message : error.Message);
                return StatusCode(500);
            }
        }
    }
}
