﻿namespace AkkaNetConsole.Messages
{
    public class PlaySongMessage
    {
        public PlaySongMessage(string song, string user)
        {
            Song = song;
            User = user;
        }

        public string Song { get; }
        public string User { get; }
    }
}