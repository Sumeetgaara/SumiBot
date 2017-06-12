using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using SumiBot.Controllers;
using Microsoft.Bot.Builder.Dialogs;

namespace SumiBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public String task = string.Empty;
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Sumi());
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}
