using System;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaNetSampleStd.Messages;
using AngleSharp;

namespace AkkaNetSampleStd.Actors
{
    public class CrawlActor : ReceiveActor
    {
        public CrawlActor(IActorRef parent)
        {
            Receive<Target>(msg => OnReveiveTarget(msg));
            Receive<TargetLinks>(msg => parent.Forward(msg));
        }

        private void OnReveiveTarget(Target msg)
        {
            System.Diagnostics.Debug.WriteLine($"*** CRAWLING - {Self.Path} - {msg.Url}");

            var config = Configuration.Default.WithDefaultLoader();

            BrowsingContext.New(config).OpenAsync(msg.Url).ContinueWith(request =>
            {
                var document = request.Result;
                var links = document.Links
                                    .Select(link => link.GetAttribute("href"))
                                    .ToList();

                return new TargetLinks() { Url = document.Url, Title = document.Title, LinkedUrls = links };
            }, TaskContinuationOptions.ExecuteSynchronously).PipeTo(Self);
        }
    }
}