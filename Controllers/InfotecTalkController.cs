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
