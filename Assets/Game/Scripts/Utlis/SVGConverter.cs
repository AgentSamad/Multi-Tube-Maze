using System;
using System.IO;
using System.Text.RegularExpressions;
using Unity.VectorGraphics;
using UnityEngine;

class SVGConverter
{
    const string folderPath = "Levels/";

    public static string GetSvgContent(string vectorName)
    {
        //  var fullPath = Path.Combine(Application.dataPath, folderPath + vectorName + ".svg");
        // return File.ReadAllText(fullPath.name);
        //  var fullPath = Resources.Load("Levels/" + vectorName); //as svg;


        var svg = Resources.Load<TextAsset>(folderPath +
                                            vectorName); //Loading SVG file from */Assets/Resources/{folderPath}/
        return svg.text;
    }
}