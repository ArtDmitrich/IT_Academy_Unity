using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFileReader : MonoBehaviour
{
    public static TextFileReader instance = null;

    public static string GetTextFromTextFile(string textFileName)
    {
        var textFile = Resources.Load<TextAsset>("Text/" + textFileName);

        return textFile.text;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }    
    }    
}
