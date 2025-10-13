using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemType { Head, Body, Weapon, ExtraWeapon, potion, Etc}
    public class Item
    {
        public string Name { get; private set; }
        public int AtkBonus { get; private set; }
        public int DefBonus { get; private set; }
        public int Effect { get; private set; }
        public int Amount { get; set; }
        public ItemType Type { get; private set; }

        public int Price {  get; private set; }
        public bool IsUsing { get; set; }

        public string ItemScript { get; private set; }

        public Item (string name, int atkBonus, int defBonus,int effect, int amount, ItemType type, int price ,string itemScript)
        {
            Name = name;
            AtkBonus = atkBonus;
            DefBonus = defBonus;
            Effect = effect;
            Amount = amount;
            Type = type;
            Price = price;
            IsUsing = false;
            ItemScript = itemScript;
        }

        public void UseItem()
        {
            Item? temp = null;
            switch (Type)
            {
                case ItemType.Head:
                    if (Inventory.equipments[ItemType.Head] == null)
                    {
                        Inventory.equipments[ItemType.Head] = this;
                    }
                    else
                    {
                        temp = Inventory.equipments[ItemType.Head];
                    }
                    break;

                case ItemType.Body:
                    if (Inventory.equipments[ItemType.Body] == null)
                    {
                        Inventory.equipments[ItemType.Body] = this;
                    }
                    else
                    {
                        temp = Inventory.equipments[ItemType.Body];
                    }
                    break;

                case ItemType.Weapon:
                    if (Inventory.equipments[ItemType.Weapon] == null)
                    {
                        Inventory.equipments[ItemType.Weapon] = this;
                    }
                    else
                    {
                        temp = Inventory.equipments[ItemType.Weapon];
                    }
                    break;

                case ItemType.ExtraWeapon:
                    if (Inventory.equipments[ItemType.ExtraWeapon] == null)
                    {
                        Inventory.equipments[ItemType.ExtraWeapon] = this;
                    }
                    else
                    {
                        temp = Inventory.equipments[ItemType.ExtraWeapon];
                    }
                    break;

                case ItemType.potion:
                    
                    break;
            }


            //아이템 수치 적용
            Program.gamePlayer.TakeDamage(-Effect);
            Program.gamePlayer.BonusAtkUp(AtkBonus);
            Program.gamePlayer.BonusDefUp(DefBonus);
            IsUsing = true;

            //이전 장비 수치 제거
            if (temp != null)
            {
                Program.gamePlayer.BonusAtkUp(-temp.AtkBonus);
                Program.gamePlayer.BonusDefUp(-temp.DefBonus);
                Console.WriteLine($"{Name} 장비, {temp.Name} 장비 해제");
                Inventory.Inven.Find(it => it == temp).IsUsing = false;
            }

            if(Type == ItemType.potion)
            {
                Inventory.Inven.Find(it => it == this).Amount--;
                if(Amount <= 0)
                {
                    Inventory.Inven.Remove(this);
                }
            }
        }

        public void BuyItem()
        {
            if (Price <= Program.gamePlayer.Gold)
            {
                if (Inventory.Inven.Contains(this)) 
                {
                    int idx = Inventory.Inven.FindIndex(n => n == this);
                    Inventory.Inven[idx].Amount++;
                }
                else
                {
                    Inventory.Inven.Add(this);
                }
                Program.gamePlayer.Gold -= Price;
                Console.WriteLine($"{this.Name} 구매 , 남은 골드 {Program.gamePlayer.Gold}G");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }

        public void SellItem()
        {
            if (Inventory.Inven.Contains(this))
            {
                int idx = Inventory.Inven.FindIndex(n => n == this);
                if(Inventory.Inven[idx].Amount > 1)
                {
                    Inventory.Inven[idx].Amount--;
                }
                else
                {
                    Inventory.Inven.Remove(Inventory.Inven[idx]);
                }
                Program.gamePlayer.Gold += (int)(Price * 0.8f);
                Console.WriteLine($"{this.Name} 판매 , 보유 골드 {Program.gamePlayer.Gold}G");
            }
            else
            {
                Console.WriteLine("오류 : 보유하고 있지 않은 아이템입니다.");
            }
            
        }

    }
    public static class Inventory
    {
        public static Dictionary<ItemType, Item> equipments = new Dictionary<ItemType, Item>() {
            { ItemType.Head, null },
            { ItemType.Body, null },
            { ItemType.Weapon, null },
            { ItemType.ExtraWeapon, null },           
        };

        public static List<Item> Inven = new List<Item>() { };

        public static void GetItem(Item item)
        {
            if (Inventory.Inven.Contains(item))
            {
                Inventory.Inven.Find(targetItem => targetItem == item ).Amount++;
            }
            else
            {
                Inventory.Inven.Add(item);
            }
            Inventory.Inven.Add(item);
        }
        
        public static string SaveInven()
        {
            string output = "";

            if( Inventory.Inven.Count > 0)
            {
                foreach (Item item in Inventory.Inven)
                {
                    output += ($"{item.Name}[{item.Amount},");
                }
            }
            return output;
        }
    }

    public class ItemPreset
    {
        public static List<Item> itemList = new List<Item>() {
            new Item("철검", 10, 0, 0, 1, ItemType.Weapon, 10, "평범한 철제 검입니다."),
            new Item("철방패", 0, 10, 0, 1, ItemType.ExtraWeapon,10, "평범한 철제 방패입니다."),
            new Item("강철검", 20, 0, 0, 1, ItemType.Weapon,10, "강철제 검입니다."),
            new Item("강철방패", 0, 20, 0, 1, ItemType.ExtraWeapon,10, "강철제 방패입니다."),
            new Item("가죽투구", 0, 5, 0, 1, ItemType.Head,10, "오크의 가죽으로 만들어진 투구입니다."),
            new Item("가죽갑옷", 0, 10, 0, 1, ItemType.Body,10, "오크의 가죽으로 만들어진 갑옷입니다."),
            new Item("철투구", 0, 10, 0, 1, ItemType.Head, 10, "단단한 철제 투구입니다."),
            new Item("철갑옷", 0, 20, 0, 1, ItemType.Body,10, "단단한 철제 갑옷입니다."),
            new Item("하급포션", 0, 0, 20, 1, ItemType.potion,10,"쓴맛이 나는 물약입니다."),
            new Item("중급포션", 0, 0, 40, 1, ItemType.potion,10,"떫은 맛이 나는 물약입니다."),
            new Item("상급포션", 0, 0, 60, 1, ItemType.potion,10,"과일 맛이 나는 물약입니다."),
            new Item("용살자", 100, 100, 0, 1, ItemType.Weapon,10,"용을 죽여라."),
            new Item("용비늘갑옷", 100, 100, 0, 1, ItemType.Body,10,"용을 죽여라."),
            new Item("용비늘방패", 100, 100, 0, 1, ItemType.ExtraWeapon,10,"용을 죽여라"),
        };
    }

}
