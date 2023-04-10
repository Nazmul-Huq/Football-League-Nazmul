using FootballLeague.Util.CsvUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Util
{
    public class ResultProcessor
    {
        public static DataTable resultTable = new DataTable();

        /// <summary>
        /// Read all file if not limit provided
        /// Process the result and return the result
        /// </summary>
        /// <param name="noOfRound"></param>
        /// <returns></returns>
        public static DataTable getResult(int noOfRound = 22)
        {
            

            //DataTable resultTable= new DataTable();
            // add header to datatable
            resultTable.Columns.Add("ID");
            resultTable.Columns.Add("MP", typeof(int));
            resultTable.Columns.Add("W", typeof(int));
            resultTable.Columns.Add("D", typeof(int));
            resultTable.Columns.Add("L", typeof(int));
            resultTable.Columns.Add("GF", typeof(int));
            resultTable.Columns.Add("GA", typeof(int));
            resultTable.Columns.Add("GD", typeof(int));
            resultTable.Columns.Add("PTS", typeof(int));

            // read all round info from csv file set info to result table
            for (int i = 1; i <= noOfRound; i++)
            {
                string fullPath = "C:\\Users\\hmnaz\\source\\repos\\FootballLeague\\FootballLeague\\Files\\";
                string finalPath = fullPath + "round-"+ i + ".csv";
                var roundResult = MyCsvReader.ConvertCSVtoDataTable(finalPath);                
               

                foreach (DataRow row in roundResult.Rows)
                {
                    // get a specific game and add details to resultTable
                    string homeTeamAbbreviation = row["HomeTeam"].ToString();
                    string awayTeamAbbreviation = row["AwayTeam"].ToString();
                    int homeTeamGoal = Int32.Parse(row["GoalHomeTeam"].ToString());
                    int awayTeamGoal = Int32.Parse(row["GoalAwayTeam"].ToString());
                    int homeTeamPoint;
                    int awayTeamPoint;
                    bool isHomeTeamWinner = false;
                    bool isAwayTeamWinner = false;
                    bool isDraw = false;

                    GetWinnterAndPoints(
                        out homeTeamPoint, 
                        out awayTeamPoint,
                        out isHomeTeamWinner,
                        out isAwayTeamWinner,
                        out isDraw,
                        in homeTeamGoal, 
                        in awayTeamGoal
                        );                    

                    // select the Home Team's info if already exist
                    DataRow homeTeamRow = resultTable.AsEnumerable().SingleOrDefault(x => x.Field<string>("ID") == homeTeamAbbreviation);
                    // set home team's info based on, it is already exist or not
                    if (homeTeamRow != null)
                    {
                        setTeamsInfoIfExist(homeTeamRow, homeTeamGoal, awayTeamGoal, homeTeamPoint, isHomeTeamWinner, isDraw);
                    }
                    else
                    {
                        setTeamsInfoIfNotExist(homeTeamAbbreviation, homeTeamGoal, awayTeamGoal, homeTeamPoint, isHomeTeamWinner, isDraw);
                    }


                    // select the Away Team's info if already exist
                    DataRow awayTeamRow = resultTable.AsEnumerable().SingleOrDefault(x => x.Field<string>("ID") == awayTeamAbbreviation);
                    // set away team's info based on, it is already exist or not
                    if (homeTeamRow != null)
                    {
                        setTeamsInfoIfExist(awayTeamRow, awayTeamGoal, homeTeamGoal, awayTeamPoint, isAwayTeamWinner, isDraw);
                    }
                    else
                    {
                        setTeamsInfoIfNotExist(awayTeamAbbreviation, awayTeamGoal, homeTeamGoal, awayTeamPoint, isAwayTeamWinner, isDraw);
                    }


                }
            }

            return resultTable;
        }

        /// <summary>
        /// set teams info like goal, point etc in team already exit in datatable 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="goalFor"></param>
        /// <param name="goalAgainst"></param>
        /// <param name="point"></param>
        /// <param name="isWinner"></param>
        /// <param name="isDraw"></param>
        private static void setTeamsInfoIfExist(
            DataRow row,
            int goalFor,
            int goalAgainst,
            int point,
            bool isWinner,
            bool isDraw = false
            )
        {
            row["MP"] = Int32.Parse(row["MP"].ToString()) + 1;
            row["GF"] = Int32.Parse(row["GF"].ToString()) + goalFor;
            row["GA"] = Int32.Parse(row["GA"].ToString()) + goalAgainst;
            row["PTS"] = Int32.Parse(row["PTS"].ToString()) + point;
            if (isDraw == true)
            {
                row["W"] = Int32.Parse(row["W"].ToString()) + 0;
                row["D"] = Int32.Parse(row["D"].ToString()) + 1;
                row["L"] = Int32.Parse(row["L"].ToString()) + 0;
            }
            else if (isWinner == true)
            {
                row["W"] = Int32.Parse(row["W"].ToString()) + 1;
                row["D"] = Int32.Parse(row["D"].ToString()) + 0;
                row["L"] = Int32.Parse(row["L"].ToString()) + 0;
            }
            else
            {
                row["W"] = Int32.Parse(row["W"].ToString()) + 0;
                row["D"] = Int32.Parse(row["D"].ToString()) + 0;
                row["L"] = Int32.Parse(row["L"].ToString()) + 1;

            }
        }

        /// <summary>
        /// Set teams information like point goal etc in case it is already not in database
        /// </summary>
        /// <param name="teamAbbreviation"></param>
        /// <param name="goalFor"></param>
        /// <param name="goalAgainst"></param>
        /// <param name="point"></param>
        private static void setTeamsInfoIfNotExist(
            string teamAbbreviation,
            int goalFor,
            int goalAgainst,
            int point,
            bool isWinner,
            bool isDraw = false
            )
        {
            DataRow dataRowHomeTeam = resultTable.NewRow();
            dataRowHomeTeam["ID"] = teamAbbreviation;
            dataRowHomeTeam["MP"] = 1;
            dataRowHomeTeam["GF"] = goalFor;
            dataRowHomeTeam["GA"] = goalAgainst;
            dataRowHomeTeam["PTS"] = point;

            if(isDraw == true)
            {
                dataRowHomeTeam["W"] = 0;
                dataRowHomeTeam["D"] = 1;
                dataRowHomeTeam["L"] = 0;
            } else if (isWinner == true) 
            {
                dataRowHomeTeam["W"] = 1;
                dataRowHomeTeam["D"] = 0;
                dataRowHomeTeam["L"] = 0;
            }
            else
            {
                dataRowHomeTeam["W"] = 0;
                dataRowHomeTeam["D"] = 0;
                dataRowHomeTeam["L"] = 1;

            }
            resultTable.Rows.Add(dataRowHomeTeam);
        }

        /// <summary>
        /// Method will determint the winner team, and set respetive points
        /// </summary>
        /// <param name="homeTeamPoint"></param>
        /// <param name="awayTeamPoint"></param>
        /// <param name="homeTeamGoal"></param>
        /// <param name="awayTeamGoal"></param>
        private static void GetWinnterAndPoints(
            out int homeTeamPoint, 
            out int awayTeamPoint,
            out bool isHomeTeamWinner,
            out bool isAwayTeamWinner,
            out bool isDraw,
            in int homeTeamGoal, 
            in int awayTeamGoal
            )
        {
            if(homeTeamGoal > awayTeamGoal)
            {
                homeTeamPoint = 3;
                awayTeamPoint = 0;
                isHomeTeamWinner = true;
                isAwayTeamWinner = false;
                isDraw = false;

            }
            else if (homeTeamGoal < awayTeamGoal)
            {
                homeTeamPoint = 0;
                awayTeamPoint = 3;
                isHomeTeamWinner = false;
                isAwayTeamWinner = true;
                isDraw = false;
            } else {
                homeTeamPoint = 1;
                awayTeamPoint = 1;
                isHomeTeamWinner = false;
                isAwayTeamWinner = false;
                isDraw = true;
            }            
        }
    } //class ResultProcessor ends here
} // namespace ends here 
