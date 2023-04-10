using FootballLeague.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Util.CsvUtil
{
    public abstract class MyCsvWriter
    {
        /// <summary>
        /// Write game data to a specific round.csv file
        /// This method randomly generate goal and assing to each team
        /// which can be ommitted and create another method that write goal 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static void writeGameToCsvFile(List<Game> data, string filename)
        {
            if (File.Exists(filename))
            {
                Console.WriteLine("the file already exist");
            } else
            {
                using (TextWriter tw = new StreamWriter(filename))
                {
                    //write the headline
                    tw.WriteLine(string.Format("{0},{1},{2},{3}", "HomeTeam", "AwayTeam", "GoalHomeTeam", "GoalAwayTeam"));              


                    // write rest of the data
                    foreach (var item in data)
                    {
                        // next 3 line is just to make some random goal
                        Random rnd = new Random();
                        int goalHomeTeam = rnd.Next(0, 4);
                        int goalAwayTeam = rnd.Next(0, 4);

                        //tw.WriteLine(string.Format("Item: {0} - Cost: {1}", item.HomeTeam, item.Cost.ToString()));
                        tw.WriteLine(string.Format(                                
                            "{0},{1},{2},{3}", 
                            item.HomeTeam, item.AwayTeam, goalHomeTeam.ToString(), goalAwayTeam.ToString()
                            ));
                    }
                }
            }
        }

        public static void writeResultToCsvFile(List<Game> data, string filename = null)
        {
            // if no file path is provided then write to the standing.csv fiel
            if (filename == null)
            {
                filename = "C:\\Users\\hmnaz\\source\\repos\\FootballLeague\\FootballLeague\\Files\\standings.csv";
                using (TextWriter tw = new StreamWriter(filename))
                {
                    //write the headline
                    tw.WriteLine(string.Format(
                        "{0},{1},{2},{3},{4},{5},{6},{7}",
                        "MP", "W", "D", "L", "GF", "GA", "GD", "PTS"
                        )); ;
                }
            }
        }
    }
} // namespace ends here
