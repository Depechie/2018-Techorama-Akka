using System;
using System.Threading;
using Akka.Actor;
using AkkaNetConsole.Actors;
using AkkaNetConsole.Messages;

namespace AkkaNetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("IntoActorSystem");

            //IActorRef musicPlayer = system.ActorOf<MusicPlayerActor>("musicPlayer");
            IActorRef dispatcher = system.ActorOf<MusicPlayerCoordinatorActor>("playercoordinator");

            dispatcher.Tell(new PlaySongMessage("Never let me down again", "Bart"));
            // uncomment to show 2 blocks of actor information
            //Thread.Sleep(200);
            //Console.WriteLine();
            dispatcher.Tell(new PlaySongMessage("Enjoy the silence", "Glenn"));
            dispatcher.Tell(new StopPlayingMessage("Bart"));
            dispatcher.Tell(new StopPlayingMessage("Glenn"));

            Console.Read();
            system.Terminate();
        }
    }
}
