using System;
using System.Collections.Generic;
using System.Linq;


// Note to self: CTRL-K-D  To Format code in Visual Studio
// Chose dictionary to hold data
// http://www.dotnetperls.com/dictionary



class Handler
{

    // Reads text file and puts every line into string array
    public string[] GetFileData(string fileToRead)
    {
        string line;
        string[] tempStringArrayBuffer = new string[1];
        string[] returnStringArray;

        // Read the file and get lines into array
        System.IO.StreamReader file =
           new System.IO.StreamReader(fileToRead);

        int linesOfFile = 0;

        while ((line = file.ReadLine()) != null)
        {
            linesOfFile++;
        }

        file.Close();

        System.IO.StreamReader fileSecondRead =
           new System.IO.StreamReader(fileToRead);

        returnStringArray = new string[linesOfFile];
        linesOfFile = 0;

        while ((line = fileSecondRead.ReadLine()) != null)
        {
            tempStringArrayBuffer[0] = line;
            returnStringArray[linesOfFile] = tempStringArrayBuffer[0];
            linesOfFile++;
        }

        file.Close();
        return returnStringArray;
    }

    // Organizes characters by their amount from most occurencies till less
    public Dictionary<char, int> GetChars(string[] ArrayToProcess)
    {

        int counter = 0;
        int toProcess = ArrayToProcess.Length;
        Dictionary<char, int> CharAmounts = new Dictionary<char, int>();

        while (counter < toProcess)
        {
            string tempString;
            tempString = ArrayToProcess[counter].ToLowerInvariant();

            foreach (char c in tempString)
            {
                if (!CharAmounts.ContainsKey(c)) { CharAmounts.Add(c, 1); }

                else
                {
                    int CurrentValue = 0;
                    if (CharAmounts.TryGetValue(c, out CurrentValue))
                    {
                        int NewValue = CurrentValue + 1;
                        CharAmounts[c] = NewValue;
                    }
                }
            }

            counter++;
        }

        return CharAmounts;
    }

    class Execute
    {

        static void Main(string[] args)
        {
            Handler ch = new Handler();

            // Get file's content into String Array
            // string[] processedStringArray = ch.GetFileData(args[0]);

            string fileName;

            if (args.Length > 0)
            {
                System.Console.WriteLine("Program has following argument: " + args[0]);
                fileName = args[0];
            }


            // default file is count.txt if file not passed by commandline
            else
            {
                fileName = "count.txt";
                System.Console.WriteLine("Program uses default filename of \"count.txt\"");
            }

            string[] processedStringArray = ch.GetFileData(fileName);


            Dictionary<char, int> resultDict = ch.GetChars(processedStringArray);

            // Sort original Dictionary by value into resultDict
            var sortedDict = from entry in resultDict orderby entry.Value descending select entry;

            int index = 0;
            int LastIndex = sortedDict.Count();
            int totalSum = 0;

            foreach (KeyValuePair<char, int> entry in sortedDict)
            {
                totalSum += entry.Value;
            }

            while (index < LastIndex)
            {
                string tempString = "Character " + sortedDict.ElementAt(index).Key.ToString() + " appears " +
                    sortedDict.ElementAt(index).Value + " times and has percentage of " +
                    (sortedDict.ElementAt(index).Value / (float)totalSum * 100).ToString() + "%";
                Console.WriteLine(tempString);
                index++;
            }

            Console.WriteLine("Totalling " + totalSum + " characters.");

            Console.WriteLine("");

            Console.WriteLine("Write something and press enter to quit");
            Console.ReadLine();

        }
    }
}
