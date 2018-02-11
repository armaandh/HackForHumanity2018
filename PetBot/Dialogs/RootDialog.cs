using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace PetBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            // --------------#2----------------
            // We say that every message will get sent to the MessageReceivedAsync method
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // --------------#3----------------
            // var activity = await result as Activity;

            PromptDialog.Text(context, NameEntered, @"Hi! What's your name");
            return Task.CompletedTask;
            // context contains the dialog stack
            // NameEntered is the callback we are sending it to ONCE THE USER REPLIES
            // 3rd param is the prompt
            
        }

        private async Task NameEntered(IDialogContext context, IAwaitable<string> result)
        {
            // result is the users response (as a string). We wrap it in IAwaitable so we gotta use await on the following line
            await context.PostAsync($@"Hi {await result}!"); // we use await at the beginning because PostAsync is asynchronous
            // PostAsync method used to send messages back to user

            context.Wait(MessageReceivedAsync); // once we're done posting the msg back out, we say we'll wait for the next input to come back in to the MessageReceivedAsync method. That method will send us back here, creating a loop.
            // context.Wait creates a 'loop' in this method saying the next message the user sends is gonna come back into this method (MessageReceivedAsync).
            // So no matter what we send to the bot, he's going to keep replying back with You sent xxx which was xxx characters
        }
    }
}

/*Receiving information from the user
 * The initial message of the user is in the Activity.Text property(IMessageActivity.Text)
*/

/*Asking the user for information
* Done through prompts. Ex: Text, Number, Choice: 
*/
