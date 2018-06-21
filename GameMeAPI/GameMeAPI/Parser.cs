using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GameMeAPI
{
    class Parser
    {
        private ArrayList players = new ArrayList();
        private ArrayList steamidArrayList = new ArrayList();
        public Parser()
        {
            Console.WriteLine("Fetching user data");
            getSteamid64ID();
            foreach (String id in steamidArrayList)
            {
                GenerateValues(id);
            }
            Console.WriteLine("Done!");
            Application.Run(new MainFrame(players));
        }

        public void getSteamid64ID()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("http://steamcommunity.com/groups/NexusNation-Staff/memberslistxml/?xml=1");
            XmlNode node = doc.DocumentElement.SelectSingleNode("//memberList/members");
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                steamidArrayList.Add(GetSteamId(Convert.ToInt64(node.ChildNodes.Item(i).InnerText.Trim()))); 
            }


        }

        public string GetSteamId(long steamId64)
        {
            var authserver = (steamId64 - 76561197960265728) & 1;
            var authid = (steamId64 - 76561197960265728 - authserver) / 2;
            return $"STEAM_0:{authserver}:{authid}";
        }

        public void GenerateValues(String steamID)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("https://stats.thenexusnation.com/api/playerinfo/csgo/" + steamID);
            XmlNode testNode = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo");
            if(testNode != null) {
                XmlNode nodeId = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo/player/id");
                XmlNode nodeTime = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo/player/time");
                XmlNode nodeName = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo/player/name");
                XmlNode nodeCountry = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo/player/cn");
                XmlNode nodeActivity = doc.DocumentElement.SelectSingleNode("/gameME/playerinfo/player/activity");
                User user = new User(nodeName.InnerText, double.Parse(nodeActivity.InnerText),
                    int.Parse(nodeTime.InnerText), nodeCountry.InnerText);
                players.Add(user);
            }
        }
    }
}