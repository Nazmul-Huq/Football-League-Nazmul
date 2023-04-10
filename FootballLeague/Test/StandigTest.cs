using FootballLeague.data;
using FootballLeague.model;
using FootballLeague.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Test
{
    public class StandigTest
    {
        /*
         * Test cases not working properly, need to work more
         */

        public static DataStorage dataStorage = new DataStorage();
        public static void TestStandingAfterDifferentRound(int round)
        {
            DataTable standings = ResultProcessor.getResult(round);
            DataTable sortedTable = standings.AsEnumerable()
         .OrderByDescending(r => r.Field<int>("PTS"))
         .ThenBy(r => r.Field<int>("GF") - r.Field<int>("GA"))
         .ThenBy(r => r.Field<int>("GF"))
         .CopyToDataTable();

            // firt print the table head
            Console.WriteLine(
                  "{0, -3} {1, -30} {2, -3} {3, -3} {4, -3} {5, -3} {6, -3} {7, -3} {8, -3} {9, -3}",
                  "", "", "MP", "W", "D", "L", "GF", "GA", "GD", "PTS"
                  );
            //print the result

            int rank = 0;
            foreach (DataRow row in sortedTable.Rows)
            {
                rank++;
                string abb = row["ID"].ToString();
                int matchPlayed = (int)row["MP"];
                int win = (int)row["W"];
                int draw = (int)row["D"];
                int loose = (int)row["L"];
                int goalFor = (int)row["GF"];
                int goalAgainst = (int)row["GA"];
                int goalDiff = goalFor - goalAgainst;
                int point = (int)row["PTS"];
                SpecialRanking specialRanking = dataStorage.teamDetail[abb].SpecialRanking;

                string teamName = dataStorage.teamDetail[abb].Name;
                string teamFullName = "(" + specialRanking + ") " + teamName + " (" + abb + ")";

                // set the console colour
                if (rank >= 11)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (rank == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (rank == 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else if (rank == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (rank == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(
                    "{0, -3} {1, -30} {2, -3} {3, -3} {4, -3} {5, -3} {6, -3} {7, -3} {8, -3} {9, -3}",
                    rank, teamFullName, matchPlayed, win, draw, loose, goalFor, goalAgainst, goalDiff, point
                    );
                Console.ResetColor();

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Green = Winner + Champions League");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Blue = Champions League");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Yellow = Europe League");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Magenda = Conference League");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Red = relegation");
            Console.ResetColor();
        }

    }
}
