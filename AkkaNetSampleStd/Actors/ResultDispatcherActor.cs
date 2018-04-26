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

        //Thanks to the fact that an Actor can only handle ONE message at a time from it's inbox, we don't have to worry about flooding the Observable Collection with concurrency problems
        public ResultDispatcherActor(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            Receive<TargetLinks>(msg => HandleResult(msg));
        }

        private void HandleResult(TargetLinks msg)
        {
            System.Diagnostics.Debug.WriteLine($"*** RESULT - {msg.Title}");
            Device.BeginInvokeOnMainThread(() => _viewModel.Results.Add(msg));
        }
    }
}