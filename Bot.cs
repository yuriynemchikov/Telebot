
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

public struct Bot
{
    static string token;
    static string baseUri;
    static HttpClient hc = new HttpClient();

    

    public static void Start()
    {
        int offset = 0;
       while (true)
       {    
            string url = $"{baseUri}getUpdates?offset={offset }";
            string json = hc.GetStringAsync(url).Result;
            System.Console.WriteLine(json);

            JsonParse.Init(json);    
            List<ModelMessage> msgs = JsonParse.Parse();

            foreach (ModelMessage msg in msgs)
            {
                System.Console.WriteLine(msg);
                Repository.Append(msg);
                string uid = msg.userId;
string msgText = String.Empty;
Random r = new Random();
                if(! Game.db.ContainsKey(uid) || msg.userMessageText == "/restart")
                {
                    int candy = r.Next(22,33);  
                    if(!Game.db.ContainsKey(uid)) Game.db.Add(uid, 0);

                    Game.db[uid] = candy;
                    msgText = $"Привет!\n";
                }
                else
                {
                    int user = 0;
                    bool flag = int.TryParse(msg.userMessageText, out user);

                    if (!flag) {msgText += $"Введи число\n";}
                    if(user>=1 && user <= 4)
                    {
                        Game.db[msg.userId] -= user;
                        var ca = Game.db[msg.userId];
                        if (ca<=0)
                        {
                            msgText += $"Ура\n";
                        }
                        else
                        {
                            var ii = r.Next(1,4);
                            Game.db[msg.userId] -= ii;
                            msgText += $"Ход бота: {ii}\n";
                            if(Game.db[msg.userId] <= 0)
                            {
                                msgText += $"Ура Бот\n"; 
                            }
                        }

                    }
                    else
                    {
                        msgText += $"Число неверное\n" ;
                    }

                }
                msgText += $"Конфет осталось: {Game.db[uid]}.\n Возьми от 1 до 4 \nПерезапуск /restart";
                SendMessage(uid, msgText, msg.messageId);
                
                offset = (Int32.Parse(msg.userUpdateId) + 1);
                Thread.Sleep(2000);


            }
        Repository.Save();    
           
        Thread.Sleep(2000);
       }
         
    }

    public static void Init(string publicToken)
    {
        token = publicToken;
        baseUri = $"https://api.telegram.org/bot{token}/";
    }

    public static void SendMessage(string id, string text, string replyToMessageId = " ")
    {
        string url = $"{baseUri}sendmessage?chat_id={id}&text={text}&reply_to_message_id={replyToMessageId}";
        var req = hc.GetStringAsync(url).Result;
    }


} 