During the startup:
Program read team.csv and setup.csv file and store informatin to DataStorage.cs
Then, the solution automatically generate 22 rounds based on informatin stored at DataStorage.cs
Then dump random goal to each game
Finally save those to the csv file as round-n.csv, n represent number of round
In case rounds are already exist, it will print that "file already exist"

When done above mentions steps it calculate the result
Finallly print Standings in the console, with various color marking to represent different status


Test Data and its implementation is not working perfectly
Need to work more
However from Program.ch file we can select different round
Which presented at lines 78-82
Must uncommeent one line at a time and restart the program