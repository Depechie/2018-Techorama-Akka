using System;
using System.Collections.Generic;

namespace AkkaNetSampleStd.Messages
{
    public class TargetLinks
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public IList<string> LinkedUrls { get; set; }
    }
}