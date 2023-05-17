using System;
using System.Collections.Generic;

namespace FinalProject
{

    public class DailySchedule
    {
        public DateTime Date { get; set; }
        public List<Action> Actions { get; set; } = null!;
    }
}
