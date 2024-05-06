using UnityEngine;

public static class TextFileReader
{
    public static string GetTextFromTextFile(string textFileName)
    {
        var textFile = Resources.Load<TextAsset>("Text/" + textFileName);

        return textFile.text;
    }   
}
