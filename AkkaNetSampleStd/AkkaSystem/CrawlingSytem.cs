using System;
using Akka.Actor;
using AkkaNetSampleStd.Actors;
using AkkaNetSampleStd.Messages;
using AkkaNetSampleStd.ViewModels;

namespace AkkaNetSampleStd.AkkaSystem
{
    public static class CrawlingSystem
    {
        private static readonly ActorSystem _system;
        private static readonly IActorRef _coordinator;

        static CrawlingSystem()
        {
            //Define a name for the Akka System
            _system = ActorSystem.Create("CrawlingSystem");
            _coordinator = _system.ActorOf(Props.Create<CoordinatorActor>(), "Coordinator");
        }

        public static void StartCrawling(string url, MainViewModel viewModel)
        {
            var props = Props.Create(() => new ResultDispatcherActor(viewModel));
            var dispatcher = _system.ActorOf(props);

            //Subscribe to the Pub/Sub event to get notified when a new result is available, pass it through to the dispatcher actor
            _system.EventStream.Subscribe(dispatcher, typeof(TargetLinks));

            //Kick off the first crawl through use of Akka.Net
            _coordinator.Tell(new Target() { Url = url });
        }
    }
}