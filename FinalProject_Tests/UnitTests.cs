using NUnit.Framework;
using FinalProject;
using Action = FinalProject.Action;

namespace FinalProject_Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            DailySchedule schedule = new DailySchedule
            {
                Actions = new List<Action>
                {
                    new Action { Name = "School", StartTime = TimeSpan.FromHours(10), Duration = TimeSpan.FromHours(1) },
                    new Action { Name = "Walk", StartTime = TimeSpan.FromHours(12), Duration = TimeSpan.FromMinutes(30) },
                    new Action { Name = "Walk", StartTime = TimeSpan.FromHours(14), Duration = TimeSpan.FromMinutes(45) },
                    new Action { Name = "TV", StartTime = TimeSpan.FromHours(16), Duration = TimeSpan.FromHours(2) }
                }
            };

            // Act
            TimeSpan totalWalkDuration = Program.GetTotalWalkDuration(schedule);

            // Assert
            Assert.Pass(TimeSpan.FromMinutes(75).ToString(), totalWalkDuration);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            DailySchedule schedule = new DailySchedule
            {
                Actions = new List<Action>
                {
                    new Action { Name = "School", StartTime = TimeSpan.FromHours(10), Duration = TimeSpan.FromHours(1) },
                    new Action { Name = "TV", StartTime = TimeSpan.FromHours(12), Duration = TimeSpan.FromMinutes(30) },
                    new Action { Name = "TV", StartTime = TimeSpan.FromHours(14), Duration = TimeSpan.FromHours(1) },
                    new Action { Name = "Walk", StartTime = TimeSpan.FromHours(16), Duration = TimeSpan.FromMinutes(45) }
                }
            };

            // Act
            Program.ReplaceTvWithWalks(schedule);

            // Assert
            Action walkAction = schedule.Actions.Find(a => a.Name == "Walk")!;
            Assert.IsNotNull(walkAction);
            Assert.Pass(TimeSpan.FromMinutes(75).ToString(), walkAction.Duration);
        }
    }
}
