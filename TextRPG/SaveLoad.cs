using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SaveLoad
    {
        static string filePath = "userdata.txt";
        static string userName = "";

        public static void SaveData()
        {
            string playerData = String.Join(",", Program.gamePlayer.SavePlayerInfo());
            string invenData = Inventory.SaveInven();
            string data = ($"{playerData}/{Program.dungeonProgress}/{invenData}");
            Console.WriteLine( data );
            File.WriteAllText(filePath, data);
        }

        public static void LoadData()
        {
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                if (data.Length > 0)
                {
                    string name = data.Split("/")[0].Split(",")[0];
                    int level = int.Parse(data.Split("/")[0].Split(",")[1]);
                    int exp = int.Parse(data.Split("/")[0].Split(",")[2]);
                    int hp = int.Parse(data.Split("/")[0].Split(",")[3]);
                    int atk = int.Parse(data.Split("/")[0].Split(",")[4]);
                    int batk = int.Parse(data.Split("/")[0].Split(",")[5]);
                    int def = int.Parse(data.Split("/")[0].Split(",")[6]);
                    int bdef = int.Parse(data.Split("/")[0].Split(",")[7]);
                    int gold = int.Parse(data.Split("/")[0].Split(",")[8]);

                    Player playerLoad = new Player(name, hp, atk, def);

                    playerLoad.BonusAtk = batk;
                    playerLoad.BonusDef = bdef;
                    playerLoad.Exp = exp;
                    playerLoad.Level = level;
                    playerLoad.Gold = gold;

                    Program.gamePlayer = playerLoad;

                    Program.dungeonProgress = int.Parse(data.Split("/")[1]);

                    List<Item> invenLoad = new List<Item>();
                    string invenString = data.Split("/")[2];
                    if (invenString.Length > 0)
                    {
                        string[] invenNameList = data.Split("/")[2].Split(",");

                        if (invenNameList.Length > 0 && invenNameList != null)
                        {
                            foreach (string itemName in invenNameList)
                            {
                                foreach (Item item in ItemPreset.itemList)
                                {
                                    if (item.Name == itemName.Split("[")[0]) 
                                    {
                                        invenLoad.Add(item);
                                        invenLoad.Find(n => n == item).Amount = int.Parse(itemName.Split("[")[1]);
                                    } 
                                }
                            }
                        }
                    }           
                }
            }
            else
            {
                Console.WriteLine("새로운 게임");
                Program.gamePlayer.GetGold(100);
            }
        }
    }
}
