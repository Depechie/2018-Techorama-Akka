using System;
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

            IActorRef musicPlayer = system.ActorOf<MusicPlayerActor>("musicPlayer");

            musicPlayer.Tell(new PlaySongMessage("Never let me down again"));
            musicPlayer.Tell(new PlaySongMessage("Enjoy the silence"));
            musicPlayer.Tell(new StopPlayingMessage());
            musicPlayer.Tell(new StopPlayingMessage());
            musicPlayer.Tell(new PlaySongMessage("Enjoy the silence"));

            Console.Read();
            system.Terminate();
        }
    }
}
