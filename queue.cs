using System;
using System.Collections.Generic;
using System.Threading;

namespace RutonyChat {
    public class Script {

       
        public Queue<string> queue = new Queue<string>();
	public bool _state = false;
	public string Comm = "!ft";
	public string Phrase_start = "Стример принимает заявки на FT3";
	public string Phrase_stop = "Стример не принимает заявки";
        public string Phrase_mt = "В очереди никого не осталось";
        public string Phrase_cnt = "В очереди $cnt человек";
	
	public string Phrase_pl_add = "$name ты добавлен в очередь, твой номер $place";
	public string Phrase_pl_wl = "$name, Спасибо за игру";
	public string Phrase_pl_del = "$name исключен из очереди";

        public void NewAlert(string site, string typeEvent, string subplan, string name, string text, float donate, string currency, int qty)
        {

            if (typeEvent == "donate")
            {
                RutonyBot.BotSay(site, name + " поддержал на " + donate.ToString() + currency);
            }

            if (typeEvent == "TwitchPoints")
            {
                RutonyBot.BotSay(site, name + " активировал награду за " + donate.ToString());
            }

        }


        public void InitParams(string param) {
            RutonyBot.SayToWindow("Очередь на фт подключена");
        }

        public void Closing() {
            RutonyBot.SayToWindow("Очередь на фт отключена");
        }
        public void NewMessage(string site, string name, string text, bool system = false, Dictionary<string, string> Params = null)
        {
            NewMessageEx(site, name, text, system, Params);
        }

            public void NewMessageEx(string site, string name, string text, bool system, Dictionary<string, string> Params) 
		{

            if (text.StartsWith(Comm))
            { 
                var msg = text.Substring(Comm.Length-1);
                bool adm = false;

                if (ProgramSettings.twitch.Channel == name || RutonyBot.ListAdmins.Contains(name)) { adm = true; }
                RutonyBot.SayToWindow(msg);

                if (text == "!ft")
                { 
                    if (_state)
                    { 
                        RutonyBot.BotSay(site, Phrase_start);
                        RutonyBot.BotSay(site, "чтобы принять участие напиши !ft3");
                    }
                    else 
                    { RutonyBot.BotSay(site, Phrase_stop); }
                }    

                if (msg == "start")
                {
                    if (adm)
                    { _state = true; }
                    else 
                    { RutonyBot.BotSay(site, "У вас не достаточно прав"); }
                }

                if (msg == "stop")
                {
                    if (adm)
                    { _state = true; }
                    else
                    { RutonyBot.BotSay(site, "У вас не достаточно прав"); }
                }

                if (msg == "3")
                {
                    if (_state)
                    { 
                        FT3(name);
                    }
                    else
                    { RutonyBot.BotSay(site, Phrase_stop); }
                }

                if (msg == "next" && adm )
                {
                    if (queue.Count > 0)
                    {
                        RutonyBot.BotSay(site, Phrase_pl_wl.Replace("$name", queue.Peek()));
                        queue.Dequeue();
                        RutonyBot.BotSay(site, Phrase_cnt.Replace("$cnt", queue.Count.ToString()));
                    }
                    else
                    {
                        RutonyBot.BotSay(site, Phrase_mt);
                    }
                }

                if (msg == "del" && adm)
                {
                    if (queue.Count > 0)
                    {
                        RutonyBot.BotSay(site, Phrase_pl_del.Replace("$name", queue.Peek()));
                        queue.Dequeue();
                        RutonyBot.BotSay(site, Phrase_cnt.Replace("$cnt", queue.Count.ToString()));
                    }
                    else
                    {
                        RutonyBot.BotSay(site, Phrase_mt);
                    }
                }




            }
                    
            
             
            
        }

        public void NewAlert() { }


        public void FT3(string name)
        { 
            queue.Enqueue(name);

            RutonyBot.SayToWindow(Phrase_pl_add.Replace("$name",name).Replace("$place", queue.Count.ToString()));

        }
    }
}
