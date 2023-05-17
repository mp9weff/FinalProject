using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FinalProject
{
    public class Program
    {
        static void Main()
        {
            DailySchedule schedule = ReadScheduleFromFile("Ben's_schedule.txt");

            Console.WriteLine();

            var totalWalkDuration = GetTotalWalkDuration(schedule);
            Console.WriteLine("Total duration of the walk: " + totalWalkDuration);

            if (totalWalkDuration < TimeSpan.FromHours(2))
            {
                ReplaceTvWithWalks(schedule);
                totalWalkDuration = GetTotalWalkDuration(schedule);
                Console.WriteLine("New total duration of the walk: " + totalWalkDuration);
            }

            WriteScheduleToFile(schedule, "Ben's_updated_schedule.txt");

           

            Console.ReadKey();
        }

        public static DailySchedule ReadScheduleFromFile(string filePath)
        {
            DailySchedule schedule = new DailySchedule
            {
                Actions = new List<Action>()
            };

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                Console.WriteLine("The file is empty.");
                return schedule;
            }

            if (DateTime.TryParse(lines[0], out DateTime date))
            {
                schedule.Date = date;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] actionData = lines[i].Split(',');

                if (actionData.Length < 3)
                {
                    Console.WriteLine($"Invalid data format at line {i + 1}. Skipping...");
                    continue;
                }

                Action action = new Action
                {
                    Name = actionData[0]
                };

                if (TimeSpan.TryParseExact(actionData[1], @"hh\:mm\:ss", null, out TimeSpan startTime))
                {
                    action.StartTime = startTime;
                }

                if (TimeSpan.TryParseExact(actionData[2], @"hh\:mm\:ss", null, out TimeSpan duration))
                {
                    action.Duration = duration;
                }

                schedule.Actions.Add(action);
            }
            foreach (Action action in schedule.Actions)
            {
                Console.WriteLine($"Action: {action.Name}, Start Time: {action.StartTime}, Duration: {action.Duration}");
            }

            return schedule;
        }

        public static TimeSpan GetTotalWalkDuration(DailySchedule schedule)
        {
            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (Action action in schedule.Actions)
            {
                if (action.Name.ToLower() == "walk" && action.StartTime >= TimeSpan.FromHours(12))
                {
                    totalDuration += action.Duration;
                }
            }

            return totalDuration;
        }

        public static void ReplaceTvWithWalks(DailySchedule schedule)
        {
            foreach (Action action in schedule.Actions)
            {
                if (action.Name.ToLower() == "tv" && action.StartTime >= TimeSpan.FromHours(14))
                {
                    TimeSpan remainingDuration = TimeSpan.FromHours(2) - GetTotalWalkDuration(schedule);
                    if (remainingDuration > TimeSpan.Zero)
                    {
                        action.Name = "Walk";
                        action.Duration = remainingDuration;
                        break;
                    }
                }
            }
        }

        static void WriteScheduleToFile(DailySchedule schedule, string filePath)
        {
            List<string> lines = new List<string> { schedule.Date.ToString("dd-MM-yyyy") };

            foreach (Action action in schedule.Actions)
            {
                string line = $"{action.Name},{action.StartTime:hh\\:mm\\:ss},{action.Duration:hh\\:mm\\:ss}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }    
    }
}
