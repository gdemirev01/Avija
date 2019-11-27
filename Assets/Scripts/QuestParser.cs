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

        SortedDictionary<string, string> props = new SortedDictionary<string, string>();
        props.Add("type", (string)jsonObj["QuestProps"]["type"]);
        props.Add("quantity", (string)jsonObj["QuestProps"]["quantity"]);
        props.Add("target", (string)jsonObj["QuestProps"]["target"]);

        return new Quest(
            (string)jsonObj["Name"],
            (string)jsonObj["QuestGiver"],
            (string)jsonObj["QuestText"],
            props
        );
    }
}
