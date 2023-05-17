using System;

namespace FinalProject
{

    public class Action
    {
        public string Name { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
