using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using PetBot.Services;

namespace PetBot.Dialogs
{
    [Serializable]
    public class LostDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("We will need some info on the animal you're looking for.");
            var lostFormDialog = FormDialog.FromForm(this.BuildLostForm, FormOptions.PromptInStart);

            context.Call(lostFormDialog, this.ResumeAfterLostFormDialog);
        }

        private IForm<LostQuery> BuildLostForm()
        {
            OnCompletionAsyncDelegate<LostQuery> processLostSearch = async (context, state) =>
            {
                await context.PostAsync($"Ok. Searching for Dogs within the last {state.Days} days.");
            };

            return new FormBuilder<LostQuery>()
                .Field(nameof(LostQuery.Days))
                .Message("Looking for Dogs within the last {Days} days...")
                .AddRemainingFields()
                .OnCompletion(processLostSearch)
                .Build();
        }

        private async Task ResumeAfterLostFormDialog(IDialogContext context, IAwaitable<LostQuery> result)
        {
            try
            {
                var searchQuery = await result;

                var dogs = await GetDogsAsync(searchQuery);

                await context.PostAsync($"I found in total {dogs.Count()} dogs for you:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var dog in dogs)
                {
                    ThumbnailCard dogCard = new ThumbnailCard()
                    {
                        Title = $"Animal:  {dog.AnimalType}",
                        Subtitle = $"Date Found - {String.Format("{0:g}", dog.DateFound)} {Environment.NewLine} Color - {dog.Color} {Environment.NewLine} Gender - {dog.Sex}",
                        Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "https://i.ebayimg.com/thumbs/images/g/ResAAOSwyjBW5pty/s-l200.jpg" }
                        },
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Claim Dog",
                                Type = ActionTypes.OpenUrl,
                                Value = $"http://petapihackforhumanity2018.azurewebsites.net/"
                            },
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = $"http://petapihackforhumanity2018.azurewebsites.net/"
                            }
                        }
                    };

                    resultMessage.Attachments.Add(dogCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task<IEnumerable<Dog>> GetDogsAsync(LostQuery searchQuery)
        {
            var dg = await PetService.GetFoundDogs();
            var dogs = new List<Dog>();

            for (var i = 0; i < dg.Count; i++)
            {
                Dog dog = new Dog()
                {
                        AnimalType= dg[i].AnimalType,
	                    DateFound = dg[i].DateFound,
	                    Size= dg[i].Size,
	                    Color = dg[i].Color,
	                    Sex= dg[i].Sex,
                };

                dogs.Add(dog);
            }

            return dogs;
        }
    }
}