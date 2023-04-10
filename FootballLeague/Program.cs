// See https://aka.ms/new-console-template for more information
using FootballLeague.data;
using FootballLeague.model;
using FootballLeague.Test;
using FootballLeague.Util;
using FootballLeague.Util.CsvUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

DataStorage dataStorage = new DataStorage();

DataTable data = new DataTable();
String filePath;

// read setup.csv file and set info to DataStorage
dataStorage.setupInfo = MyCsvReader.ConvertCSVtoDataTable("C:\\Users\\hmnaz\\source\\repos\\FootballLeague\\FootballLeague\\Files\\setup.csv");
// read teams.csv file and set info to DataStorage
dataStorage.teamsInfo = MyCsvReader.ConvertCSVtoDataTable("C:\\Users\\hmnaz\\source\\repos\\FootballLeague\\FootballLeague\\Files\\teams.csv");

foreach (DataRow row in dataStorage.teamsInfo.Rows)
{
    string abb = row["abbreviation"].ToString();
    string name = row["club_name"].ToString();
    string specialRankingSymbol = row["special_ranking"].ToString();
    SpecialRanking specialRanking = GetSpecialRanking(specialRankingSymbol);
    Team team= new Team(abb, name, specialRanking);
    dataStorage.teamDetail.Add(abb, team);      
}




    // get a list of all teams abbreviation
    List<String> teamAbbreviationList = dataStorage.getSingleColumn(dataStorage.teamsInfo, "abbreviation");

int numberOfRound = 22;


/*
 * BELOW COMMENTED LINES ARE NOT REQUIREMENT FOR ASSIGNMENT
 * BUT I WROTE IT AS IN THE BEGINNING I THOUGHT I WILL ALSO NEED TO MAKE GAME SCHEDULE
 * 
 * all commented lines are responsible to make schedule from setup file
 * add random goal to matches
 * save them in the csv file
 * should run only once during startup
 */


//for (int i = 1; i <= numberOfRound; i++)
//{
//    // get fixture for a specific round
//    //var fixture = FixtureGenerator.createRound(teamAbbreviationList, i);
//    List<Game> fixture = FixtureGenerator.createRound(teamAbbreviationList, i);

//    // create the file path
//    createFilePath(out filePath, i);

//    //send information to the MyCsvWriter to write to a a csv file
//    MyCsvWriter.writeGameToCsvFile(fixture, filePath);

//}

//void createFilePath(out string filePath, int fileNumber)
//{
//    string fullPath = "C:\\Users\\hmnaz\\source\\repos\\FootballLeague\\FootballLeague\\Files\\";
//    string finalPath = fullPath + "round-" + fileNumber + ".csv";
//    filePath = finalPath;
//}


/*
 * show the result (print in console and save to a csv file)
 * Can uncommented one roud at a time 
 */
//DataTable standings = ResultProcessor.getResult(2); // show result after round 2
// DataTable standings = ResultProcessor.getResult(6); // show result after round 6
// DataTable standings = ResultProcessor.getResult(12); // show result after round 12
// DataTable standings = ResultProcessor.getResult(15); // show result after round 15
DataTable standings = ResultProcessor.getResult(); // show result after finishing all round

DataTable sortedTable = standings.AsEnumerable()
         .OrderByDescending(r => r.Field<int>("PTS"))
         .ThenBy(r => r.Field<int>("GF") - r.Field<int>("GA") )
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
    int goalFor = (int) row["GF"];
    int goalAgainst = (int) row["GA"];
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


/*
 * Based on given string return SpecialRanking enum
 */
SpecialRanking GetSpecialRanking(string specialRankingSymbol)
{
    switch (specialRankingSymbol)
    {

        case "W":
            return SpecialRanking.W;
        case "C":
            return SpecialRanking.C;
        case "P":
            return SpecialRanking.P;
        case "R":
            return SpecialRanking.R;
        default:
            return SpecialRanking.N;
    }
}


