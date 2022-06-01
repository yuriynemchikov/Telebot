using System.Collections.Generic;
using Newtonsoft.Json.Linq;


public struct JsonParse
{
    static string json;

    public static void Init(string JsonString)
    {
        json = JsonString;
    }

    public static List<ModelMessage> Parse()
    {
        List<ModelMessage> messagers = new();
        JObject resultReq = JObject.Parse(json);
        JToken result = resultReq["result"]!;

        foreach (JToken message in result)
        {
            ModelMessage mm = new();
            mm.userFirstName = message["message"]!["from"]!["first_name"]!.ToString();
            mm.userMessageText = message["message"]!["text"]!.ToString();
            mm.userUpdateId = message["update_id"]!.ToString();
            mm.userId = message["message"]!["from"]!["id"]!.ToString();
            mm.messageId = message["message"]!["message_id"]!.ToString();
            messagers.Add(mm);
        }
        return messagers;
    }
}

