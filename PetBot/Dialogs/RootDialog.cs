using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace PetBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string LostOption = "Lost a Pet";

        private const string FoundOption = "Found a Pet";

        public Task StartAsync(IDialogContext context)
        {
            
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            ShowOptions(context);
            return Task.CompletedTask; // might be problematic
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { LostOption, FoundOption }, "Are you looking for a lost pet or reporting one you found?", "Not a valid option", 3);
        }


        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case LostOption:
                        context.Call(new LostDialog(), ResumeAfterOptionDialog);
                        break;

                    case FoundOption:
                        context.Call(new FoundDialog(), ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Too many attempts. Please try again");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}