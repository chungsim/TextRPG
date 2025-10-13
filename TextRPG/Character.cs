using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    public enum CharState {Live, Die}
    public abstract class Character
    {
        public string Name { get; private set; }
        public int Hp{ get; protected set; }
        public int Atk {  get; protected set; }
        public int Def {  get; protected set; }
        public  CharState State { get; set; }

        //캐릭터 생성자
        public Character(string name, int hp, int atk, int def)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Def = def;
            State = CharState.Live;
        }

        public Monster DeepcopyMonster()
        {
            Monster copy = new Monster("", 0, 0, 0);
            copy.Name = Name;
            copy.Hp = Hp;
            copy.Atk = Atk;
            copy.Def = Def;
            return copy;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if(Hp > 100) Hp = 100;
        }
        
        public void AtkUP(int num)
        {
            Atk += num;
        }

        public void DefUp(int num)
        {
            Def += num;
        }
    }

    class Player : Character
    {
        public int Level { get; set; }
        public int Exp { get; set; }

        public int BonusAtk;

        public int BonusDef;

        public int[] LevelUpExp = new int[] {0,20,30,40,50,60,70,80,90,100};

        public int Gold;

        // player 생성자
        public Player(string name, int hp, int atk, int def) : base(name, hp, atk, def)
        {
            Level = 1;
            Exp = 0;
            BonusAtk = 0;
            BonusDef = 0;
            Gold = 0;
        }

        public List<String> PlayerInfo()
        {
            List<String> infos = new List<String>();
            infos.Add($"Name = {Name}");
            infos.Add($"LV  = {Level}");
            infos.Add($"EXP  = {Exp}/{LevelUpExp[Level]}");
            infos.Add($"HP  = {Hp}");
            infos.Add($"Atk = {Atk}{(BonusAtk == 0 ? "" : $"(+{BonusAtk})")}");
            infos.Add($"Def = {Def}{(BonusDef == 0 ? "" : $"(+{BonusDef})")}");
            infos.Add($"Gold = {Gold}");
            return infos;
        }

        public List<String> SaveplayerInfo()
        {
            List<String> infos = new List<String>();
            infos.Add($"{Name}");
            infos.Add($"{Level}");
            infos.Add($"{Exp}");
            infos.Add($"{Hp}");
            infos.Add($"{Atk}");
            infos.Add($"{BonusAtk}");
            infos.Add($"{Def}");
            infos.Add($"{BonusDef}");
            infos.Add($"{Gold}");
            return infos;
        }

        public void BonusAtkUp(int num)
        {
            BonusAtk += num;
        }

        public void BonusDefUp(int num)
        {
            BonusDef += num;
        }

        public void GetExp(int num)
        {
            Exp += num;
            while(Level < 10 && Exp > LevelUpExp[Level])
            {
                Exp -= LevelUpExp[Level];
                Level++;
                TakeDamage(10);
                Atk += 1;
                Def += 1;
                Console.WriteLine($"레벨 업!");
            }
        }

        public void GetGold(int num)
        {
            Gold += num;
        }

        //public void DisplayInfo()
        //{
        //    Console.WriteLine($"====================");
        //    Console.WriteLine($"   Name = {Name}");
        //    Console.WriteLine($"    LV  = {Level}");
        //    Console.WriteLine($"   EXP  = {Exp}/100");
        //    Console.WriteLine($"    HP  = {Hp}");
        //    Console.WriteLine($"    Atk = {Atk}");
        //    Console.WriteLine($"    Def = {Def}");
        //    Console.WriteLine($"====================");
        //}

    }

    public class Monster : Character
    {
        public Monster(string name, int hp, int atk, int def) : base(name, hp, atk, def)
        {
            
        }
        public List<string> MonsterInfo()
        {
            List <string> infos = new List<string>();
            infos.Add($"[{Name}]");
            infos.Add($"HP: {Hp}");
            infos.Add($"ATK: {Atk}");
            infos.Add($"DEF: {Def}");
            return infos;
        }
    }

    public class MonsterPreset
    {
        public static List<Monster> monsterList = new List<Monster>()
        {
            new Monster("슬라임", 10, 5, 0),
            new Monster("고블린", 15, 6, 3),
            new Monster("오크", 30, 7, 5),
            new Monster("골렘", 20, 6, 10),
            new Monster("아울베어", 30, 10, 8),
            new Monster("구울", 15, 15, 0),
            new Monster("벰파이어", 35, 15, 10),
            new Monster("악마", 66, 16, 6),
            new Monster("드래곤", 999, 99, 99)
        };

        
    };
}
