using System;
using Akka.Actor;
using AkkaNetConsole.Messages;

namespace AkkaNetConsole.Actors
{
    public class MusicPlayerActor : ReceiveActor
    {
        protected string CurrentSong;

        public MusicPlayerActor()
        {
            StoppedBehavior();
        }

        private void StoppedBehavior()
        {
            Receive<PlaySongMessage>(m => PlaySong(m.Song));
            Receive<StopPlayingMessage>(m => Console.WriteLine("Cannot stop, the actor is already stopped"));
        }

        private void PlayingBehavior()
        {
            Receive<PlaySongMessage>(m => Console.WriteLine($"Cannot play. Currently playing '{CurrentSong}'"));
            Receive<StopPlayingMessage>(m => StopPlaying());
        }

        private void PlaySong(string song)
        {
            CurrentSong = song;
            Console.WriteLine($"Currently playing '{CurrentSong}'");

            Become(PlayingBehavior);
        }

        private void StopPlaying()
        {
            CurrentSong = null;
            Console.WriteLine($"Player is currently stopped.");

            Become(StoppedBehavior);
        }
    }
}