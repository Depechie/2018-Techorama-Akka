using System;
using Akka.Actor;
using Akka.Event;

namespace AkkaNetSampleStd.Actors
{
    public class DeadLetterMonitor : ReceiveActor
    {
        public DeadLetterMonitor()
        {
            Receive<DeadLetter>(arg => HandleDeadLetter(arg));
        }

        private void HandleDeadLetter(DeadLetter arg)
        {
            System.Diagnostics.Debug.WriteLine($"Msg {arg.Message} \n Sender {arg.Sender} \n Recipient {arg.Recipient}");
        }
    }
}
