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
            Receive<PlaySongMessage>(m => PlaySong(m.Song));
        }

        private void PlaySong(string song)
        {
            CurrentSong = song;
            Console.WriteLine($"Currently playing '{CurrentSong}'");
        }
    }
}