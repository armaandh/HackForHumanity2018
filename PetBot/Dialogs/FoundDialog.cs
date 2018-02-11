using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace PetBot.Dialogs
{
    [Serializable]
    public class FoundDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("We will need some info on the animal you just found.");

            var foundAnimalFormDialog = FormDialog.FromForm(BuildFoundAnimalForm, FormOptions.PromptInStart);

            context.Call(foundAnimalFormDialog, ResumeAfterFoundAnimalFormDialog);
        }

        private IForm<FoundQuery> BuildFoundAnimalForm()
        {
            OnCompletionAsyncDelegate<FoundQuery> processFoundReport = async (context, state) =>
            {
                // maybe show filled info on complete
                await context.PostAsync("Report has been sent.");
            };

            return new FormBuilder<FoundQuery>()
                .AddRemainingFields()
                .OnCompletion(processFoundReport)
                .Build();

        }

        private async Task ResumeAfterFoundAnimalFormDialog(IDialogContext context, IAwaitable<FoundQuery> result)
        {
            context.Done<object>(null);
        }
    }
}