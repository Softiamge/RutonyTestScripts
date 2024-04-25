using System;
using System.Collections.Generic;
using System.Threading;

namespace RutonyChat {
    public class Script {

       
        public Queue<string> queue = new Queue<string>();
		public bool _state = false;
		public string CommandFT = "!ft";
		public string Phrase_start = "Стример принимает заявки на FT3";
		public string Phrase_stop = "Стример не принимает заявки";
		public string Phrase_pl_add = "$name ты добавлен в очередь, твой номер $place";
		public string Phrase_pl_wl = "$name, Спасибо за игру";
		public string Phrase_pl_del = "$name исключен из очереди";
        



        public void InitParams(string param) {
            RutonyBot.SayToWindow("Очередь на фт подключена");
        }

        public void Closing() {
            RutonyBot.SayToWindow("Очередь на фт отключена");
        }

        public void NewMessage(string site, string name, string text, bool system) 
		{
            RutonyBot.SayToWindow(string.Format("site={0}, name={1}, text={2}", site, name, text));
            RutonyBot.SayToWindow("NewMessage()");
        }
        public void NewMessageEx(string site, string name, string text, bool system, Dictionary<string, string> Params) 
		{
            
            if(text.StartsWith("!ft"))
            {  RutonyBot.BotSay(site, "entered !ft"); }
            
            if (Equals(null, Params))
            {
				RutonyBot.BotSay(site, "Params is null!");
            }
            else
            {
				foreach (var item in Params)
				{
					RutonyBot.BotSay(site,item.Key);
					RutonyBot.BotSay(site,item.Value);
				}
			}

            if(text == "!ft" ) 
            {
                if(_state)
                { RutonyBot.BotSay(site, name + " ты можешь подать заявку командой !ft3"); }
                else 
                { RutonyBot.BotSay(site, Phrase_stop);  return;}

            } 
            
        }

        public void FT(string name, string text) 
        {
            string[] text_arr = text.Split(' ');

            if(text_arr.Length < 2)

            if (_state)
            { }
            else
            {
                if(text_arr[1] == "start" && !RutonyBot.ListAdmins.Contains(name) ) 
                { 
                    _state = true;
                    RutonyBot.SayToWindow(Phrase_start);
                }
                
            
            }
            
        }

        public void FT3(string name)
        { 
            queue.Enqueue(name);

            RutonyBot.SayToWindow(Phrase_start.Replace("$name",name));

        }
    }
}