using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

public struct Repository
{
    static Dictionary<string, List<ModelMessage>> db = new();

    public static void Append(ModelMessage model)
    {
        var id = model.userId;
        if(db.ContainsKey(id))
        {
            db[id].Add(model);
        }
        else
        {
            db.Add(id, new List<ModelMessage>(new ModelMessage[] { model })); 
        }

    }

    public static Dictionary<string, List<ModelMessage>> Read()
    {
        return db;

    }

    public static string GetString()
    {
        string data = String.Empty;
        foreach (var item in db)
        {
    
            data += $"{item.Key} [{String.Join(' ', item.Value)}]\n";
        
        }
        return $"{data}\n\n"; 
    }

    public static void Save()
    {
        File.WriteAllText("data.json", Newtonsoft.Json.JsonConvert.SerializeObject(db));
    }

    public static void Load()
    {
        db = JsonConvert.DeserializeObject<Dictionary<string, List<ModelMessage>>>(File.ReadAllText("data.json"))!;
    }
}