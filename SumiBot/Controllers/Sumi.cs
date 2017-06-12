using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using DataAcess;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;


namespace SumiBot.Controllers
{
    [Serializable]
    public class Sumi:IDialog
    {
       
        public string task { get; set; }
        public string pwd = "chelseaisourfavclub";
        public string name = String.Empty;
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedStartConversation);
        }
        public async Task MessageReceivedStartConversation(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("Hie!I am SumiBot!what is your name?");
            context.Wait(getName); 
        }

        public async Task getName(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            name = (await argument).Text;
            if(name == "Sumit More")
            {
                await context.PostAsync("Bro Code?");
                context.Wait(sumiSide);
            }
            else
            {
                Models.Database1Entities2 DB = new Models.Database1Entities2();
                var message = (from Sumi in DB.Sumis
                               where Sumi.Processed == 1 
                                select Sumi.MessagefromSumi);

                await context.PostAsync($"he told me ,{message.Single()}.So he is not able to pick up fone or msg you.");
                context.Wait(MessageReceivedStartConversation);
            }
        }

        public async Task  sumiSide(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            
            pwd = (await argument).Text;
            if (pwd=="chelseaisourfavclub")
            {
                await context.PostAsync("what should I remember and tell your friends?");
                context.Wait(processSumi);
            }


        }
        public async Task processSumi(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {  
                Models.Database1Entities2 DB = new Models.Database1Entities2();
                Models.Sumi sumi = new Models.Sumi();
                sumi.MessagefromSumi = Convert.ToString((await argument).Text);
                sumi.Processed = 1;
                DB.Sumis.Add(sumi);
                DB.SaveChanges();
                context.Wait(MessageReceivedStartConversation);

        }
    }
}