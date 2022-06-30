using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class HandleTextFile
{
    const string FILE_NAME = "Quotes.txt";

    public static bool GetQuotes(out List<string> output)
    {
        output = new List<string>();

        try
        {
            using(StreamReader file = new StreamReader("Assets/Resources/" + FILE_NAME))
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
}
