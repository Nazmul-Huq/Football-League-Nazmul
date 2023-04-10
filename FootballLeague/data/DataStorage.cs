using FootballLeague.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.data
{
    public class DataStorage
    {
        //public static DataTable setupInfo = new DataTable();
        //public static DataTable teamsInfo = new DataTable();
        public DataTable? setupInfo { get; set; }
        public DataTable? teamsInfo { get; set; }
        public DataTable? roundInfo { get; set; }
        public DataTable? result { get; set; }
        public DataTable? standings { get; set; }

        public Dictionary<string, Team> teamDetail = new Dictionary<string, Team>();

        /// <summary>
        /// Get all data from a single column and return it
        /// </summary>
        /// <param name="dataToRead"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<String> getSingleColumn(DataTable dataToRead, string columnName)
        {
            List<String> teamsAbbreviation = new List<String>();

            for (int index = 0; index < dataToRead.Rows.Count; index++)
            {
                string teamAbbreviation = dataToRead.Rows[index][columnName].ToString();
                teamsAbbreviation.Add(teamAbbreviation);
            }
            return teamsAbbreviation;
        }
    }
}
