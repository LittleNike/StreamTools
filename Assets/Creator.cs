using Newtonsoft.Json;
using System;
using System.IO;

[Serializable]
public class Creator
{
    public string tName;
    public string tPlatform;
    [JsonIgnore] public bool tToggle;

    public Creator(string name, string platform)
    {
        tName = name;
        tPlatform = platform;
    }

    public override string ToString()
    {
        return $"{tName} {tPlatform}";
    }
}

public static class CreatorUtilities
{
    public static void WriteJson(string path, Creator[] creators)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(creators, Formatting.Indented));
    }

    public static Creator[] ReadJson(string path)
    {
        string s = File.ReadAllText(path);
        if(s.Length != 0)
            return JsonConvert.DeserializeObject<Creator[]>(s);
        return new Creator[0];
    }
}
