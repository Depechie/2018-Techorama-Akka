using System;
using Akka.Actor;
using Akka.Routing;
using AkkaNetSampleStd.Messages;

namespace AkkaNetSampleStd.Actors
{
    public class CoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _crawlers;

        public CoordinatorActor()
        {
            //Enable auto spawning of 5 Crawling actors
            //New messages are routed to the actor with the current smallest mailbox ( amount of queued messages in it's mailbox )
            _crawlers = Context.ActorOf(Props.Create(() => new CrawlActor(Self)).WithRouter(new SmallestMailboxPool(5)));

            Receive<Target>(msg => _crawlers.Tell(msg));
            Receive<TargetLinks>(msg => OnReceiveTargetLinks(msg));
        }

        private void OnReceiveTargetLinks(TargetLinks result)
        {
            //Publish the actual result onto the build in Pub/Sub channel
            if (!string.IsNullOrWhiteSpace(result.Title))
                Context.System.EventStream.Publish(result);
            
            //When a crawl result comes in, pass new links to the actor pool to trigger new crawls
            foreach (var url in result.LinkedUrls)
                _crawlers.Tell(new Target() { Url = url });
        }
    }
}