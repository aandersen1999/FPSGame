using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class HandleTextFile
{
    const string FILE_NAME = "Quotes.txt";
    private const string FearFILE_NAME = "fear.txt";

    public static bool GetQuotes(out List<string> output)
    {
        output = new List<string>();

        try
        {
            using(StreamReader file = new StreamReader("Resources/" + FILE_NAME))
            {
                string line;
                while((line = file.ReadLine()) != null)
                {
                    output.Add(line);
                }
            }
            return true;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.Message);
        }
        return false;
    }

    public static void WriteFearFile()
    {
       using(StreamWriter file = new StreamWriter(FearFILE_NAME, false))
       {
            string line = "everything will be okay";

            for(int i = 0; i < 536; i++)
            {
                file.WriteLine(line);
            }
       }
        
    }
}
