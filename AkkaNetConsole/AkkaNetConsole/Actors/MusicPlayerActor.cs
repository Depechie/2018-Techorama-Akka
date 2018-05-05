using System;
using Akka.Actor;
using AkkaNetConsole.Messages;

namespace AkkaNetConsole.Actors
{
    public class MusicPlayerActor : ReceiveActor
    {
        protected PlaySongMessage CurrentSong;

        public MusicPlayerActor()
        {
            StoppedBehavior();
        }

        private void StoppedBehavior()
        {
            Receive<PlaySongMessage>(m => PlaySong(m));
            Receive<StopPlayingMessage>(m => Console.WriteLine($"{m.User}'s player: Cannot stop, the actor is already stopped"));
        }

        private void PlayingBehavior()
        {
            Receive<PlaySongMessage>(m => Console.WriteLine($"{CurrentSong.User}'s player: Cannot play. Currently playing '{CurrentSong.Song}'"));
            Receive<StopPlayingMessage>(m => StopPlaying());
        }

        private void PlaySong(PlaySongMessage song)
        {
            CurrentSong = song;
            Console.WriteLine($"{CurrentSong.User} is currently listening to '{CurrentSong.Song}'");
            DisplayInformation();

            Become(PlayingBehavior);
        }

        private void StopPlaying()
        {
            Console.WriteLine($"{CurrentSong.User}'s player is currently stopped.");
            CurrentSong = null;

            Become(StoppedBehavior);
        }

        private void DisplayInformation()
        {
            Console.WriteLine("Actor's information:");
            Console.WriteLine($"Typed Actor named: {Self.Path.Name}");
            Console.WriteLine($"Actor's path: {Self.Path}");
            Console.WriteLine($"Actor is part of the ActorSystem: {Context.System.Name}");
            Console.WriteLine($"Actor's parent: {Context.Self.Path.Parent.Name}");
        }
    }
}