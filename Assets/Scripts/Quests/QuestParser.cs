using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

public class QuestParser
{
    public static Quest Deserialize(string path)
    {
        StreamReader reader = new StreamReader(path);
        var jsonObj = JObject.Parse(reader.ReadToEnd());
        reader.Close();

        var parts = PartsParser(jsonObj);        

        return new Quest(
            (string)jsonObj["name"],
            (string)jsonObj["giver"],
            (string)jsonObj["text"],
            (int)jsonObj["partsNumber"],
            (float)jsonObj["exp"],
            (int)jsonObj["coins"],
            parts
        );
    }

    private static List<SortedDictionary<string, string>> PartsParser(JObject jsonObj)
    {
        int partsNumber = (int)jsonObj["partsNumber"];
        List<SortedDictionary<string, string>> parts = new List<SortedDictionary<string, string>>();
        for (int i = 1; i <= partsNumber; i++)
        {
            SortedDictionary<string, string> part = new SortedDictionary<string, string>();

            part.Add("type", (string)jsonObj["part" + i]["type"]);
            part.Add("target", (string)jsonObj["part" + i]["target"]);
            part.Add("quantity", (string)jsonObj["part" + i]["quantity"]);
            part.Add("progress", "0");
            part.Add("status", "open");
            parts.Add(part);
        }

        return parts;
    }
}
