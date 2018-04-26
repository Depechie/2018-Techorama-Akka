using System;
using Akka.Actor;
using AkkaNetSampleStd.Messages;
using AkkaNetSampleStd.ViewModels;
using Xamarin.Forms;

namespace AkkaNetSampleStd.Actors
{
    public class ResultDispatcherActor : ReceiveActor
    {
        private MainViewModel _viewModel;
        private int _messageCount = 0;

        //Thanks to the fact that an Actor can only handle ONE message at a time from it's inbox, we don't have to worry about flooding the Observable Collection with concurrency problems
        public ResultDispatcherActor(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            Receive<TargetLinks>(msg => HandleResult(msg));
        }

        private void HandleResult(TargetLinks msg)
        {
            ++_messageCount;

            //When we received 5 results, trigger a stop in the message stream by sending a Poison Pill
            if (_messageCount > 5)
                Context.ActorSelection(Self.Path.Root).Tell(PoisonPill.Instance);
            else
            {
                System.Diagnostics.Debug.WriteLine($"*** RESULT - {_messageCount} - {msg.Title}");
                Device.BeginInvokeOnMainThread(() => _viewModel.Results.Add(msg));
            }
        }
    }
}