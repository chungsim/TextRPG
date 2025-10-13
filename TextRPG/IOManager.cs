using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class IOManager
    {
        public static void UserInput()
        {
            StageType curruntStageType = Program.currentStage.Type;
            bool flag = true;
            Console.Clear();
            //IOManager.PrintStage(Program.currentStage.StageInfo());
            while (flag)
            {
                IOManager.PrintStage(Program.currentStage.StageInfo());
                
                switch (curruntStageType)
                {
                    case StageType.Battle:
                        PrintMonsterInfo(Program.gameMonster);
                        Console.WriteLine("1. 상태보기");
                        Console.WriteLine("2. 인벤토리");
                        Console.WriteLine("3. 공격");
                        Console.WriteLine("4. 도주");
                        break;

                    case StageType.Event:
                        Console.WriteLine("1. 상태보기");
                        Console.WriteLine("2. 인벤토리");
                        Console.WriteLine("3. 수락");
                        Console.WriteLine("4. 거절");
                        break;

                    case StageType.Shop:
                        Console.WriteLine("1. 상태보기");
                        Console.WriteLine("2. 인벤토리");
                        Console.WriteLine("3. 구매");
                        Console.WriteLine("4. 판매");
                        Console.WriteLine("5. 떠나기");
                        break;

                    case StageType.System:
                        Console.WriteLine("1. 상태보기");
                        Console.WriteLine("2. 인벤토리");
                        Console.WriteLine("3. 던전");
                        Console.WriteLine("4. 상점");
                        Console.WriteLine("5. 모험");
                        Console.WriteLine("9. 저장");
                        break;

                }

                Console.WriteLine("원하시는 행동을 입력해주세요");
                string input = Console.ReadLine();

                PrintLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        //IOManager.PrintStage(Program.currentStage.StageInfo());
                        PrintLine();
                        PrintPlayrInfo(Program.gamePlayer);
                        break;

                    case "2":
                        Console.Clear();
                        IOManager.PrintStage(Program.currentStage.StageInfo());
                        PrintInventory();
                        Console.WriteLine("사용할 아이템의 번호를 입력하세요.");
                        flag = !ItemUseInput();
                        PrintLine();
                        break;

                    case "3":
                        Console.Clear();
                        switch (curruntStageType)
                        {
                            case StageType.Battle:
                                Console.WriteLine($"{Program.gamePlayer.Name}의 공격!");
                                Battle.PlayerAttack(Program.gameMonster);

                                if(Program.gameMonster.Hp > 0)
                                {
                                    Console.WriteLine($"{Program.gameMonster.Name}의 공격!");
                                    Battle.MonsterAttack(Program.gameMonster);
                                }                            
                                break;
                            case StageType.Event:
                                Console.WriteLine("수락!");
                                Event.eventRoll();
                                flag = false;
                                BackToStart();
                                break;
                            case StageType.Shop:
                                Console.Clear();
                                IOManager.PrintStage(Program.currentStage.StageInfo());
                                PrintShop(Program.gamePlayer.Level);
                                Console.WriteLine("구매할 아이템의 번호를 입력하세요.");
                                flag = !ItemBuyInput(ShopPreset.shopItemList[Program.gamePlayer.Level]);
                                PrintLine();
                                Thread.Sleep(1000);
                                break;
                            case StageType.System:
                                Console.WriteLine("system_던전연결");
                                Program.nextStageType = StageType.Dungeon;
                                flag = false;
                                break;

                        }                      
                        PrintLine();
                        //flag = false;
                        break;

                    case "4":
                        switch (curruntStageType)
                        {
                            case StageType.Battle:
                                Console.WriteLine("도주!");
                                Program.nextStageType = StageType.System;
                                break;
                            case StageType.Event:
                                Console.WriteLine("거절!");
                                break;
                            case StageType.Shop:
                                Console.Clear();
                                IOManager.PrintStage(Program.currentStage.StageInfo());
                                PrintInventory();
                                Console.WriteLine("판매할 아이템의 번호를 입력하세요.");
                                flag = !ItemSellInput();
                                PrintLine();
                                Thread.Sleep(1000);
                                break;
                            case StageType.System:
                                Console.WriteLine("system_상점연결");
                                flag = false;
                                Program.nextStageType = StageType.Shop;
                                break;
                        }
                        Console.Clear();
                        PrintLine();
                        flag = false;
                        break;

                    case "5":
                        Console.Clear();
                        switch (curruntStageType)
                        {
                            case StageType.Battle:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                            case StageType.Event:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                            case StageType.Shop:
                                Console.WriteLine("상점 떠나기");
                                flag = false;
                                BackToStart();
                                break;
                            case StageType.System:
                                Console.Clear();
                                Console.WriteLine("system_랜덤이벤트 연결(넥스트 스테이지 타입만 변경)");
                                Program.nextStageType = StageType.Event;
                                PrintLine();
                                flag = false;
                                break;
                        }
                        break;

                    case "9":
                        if(curruntStageType == StageType.System)
                        {
                            SaveLoad.SaveData();
                            Console.WriteLine($"현재 경로: {Directory.GetCurrentDirectory()}");
                            Thread.Sleep(10000);
                        }
                        flag = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }

                BattleEndCheck(flag);
                if(Program.gameMonster.Hp <= 0)
                {
                    flag = false;
                }
            }
        }

        public static void PrintStage(List<string> lines)
        {
            Console.WriteLine("============================================================");
            for (int i = 0; i < 10 - lines.Count; i++)
            {
                //Console.WriteLine("");
            }
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("============================================================");

        }
        public static void PrintStart()
        {
            PrintLine();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        }

        public static void PrintLine()
        {
            Console.WriteLine("============================================================");
        }

        static void PrintPlayrInfo(Player player)
        {
            List<string> lines = player.PlayerInfo();
            string outString = "";
            foreach (string line in lines)
            {
                outString += "[" + line + "]";
            }
            Console.WriteLine(outString);
        }

        static void PrintMonsterInfo(Monster monster)
        {
            List<string> lines = monster.MonsterInfo();
            string outString = "";
            foreach (string line in lines)
            {
                outString += "[" + line + "]";
            }
            Console.WriteLine(outString);
        }

        static void PrintInventory()
        {
            //장비 출력
            Console.WriteLine("================= 장비 ================");
            foreach (KeyValuePair<ItemType, Item> item in Inventory.equipments)
            {
                Console.WriteLine("[{0}: {1}]", item.Key.ToString(), (item.Value == null ? "빈칸" : item.Value.Name));
            }
            
            PrintLine();
            //인벤 출력
            Console.WriteLine("================= 인벤토리 ================");           

            for (int i = 0;i < Inventory.Inven.Count;i++)
            {
                Item itemInven = Inventory.Inven[i];
                Console.WriteLine($"{i +1}.[{itemInven.Name}]: {itemInven.Amount}{(itemInven.IsUsing == true ? "[E]" : " ")}");
            }

        }

        static void PrintShop(int level)
        {
            List<Item> shopItemList = ShopPreset.shopItemList[level];
            //상점 출력
            Console.WriteLine("================= 상점 ================");

            for (int i = 0; i < shopItemList.Count; i++)
            {
                Item itemShop = shopItemList[i];
                Console.WriteLine($"{i + 1}.[{itemShop.Name}] : [{itemShop.Price}]G");
            }
        }

        static bool ItemUseInput()
        {
            bool flag = true;
            bool result = false;
            Console.WriteLine("0번은 돌아가기입니다.");
            while (flag)
            {
                int index;
                int.TryParse(Console.ReadLine(), out index);

                if(index >= 0)
                {
                    if(index == 0)
                    {
                        flag = false;
                        Console.Clear();
                    }
                    else
                    {
                        Inventory.Inven[index - 1].UseItem();
                        flag = false;
                        result = true;
                    }                    
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                
            }

            return result;
        }

        static bool ItemBuyInput(List<Item> shopItemList)
        {
            bool flag = true;
            bool result = false;

            Console.WriteLine("0번은 돌아가기입니다.");
            while (flag)
            {
                int index;
                int.TryParse(Console.ReadLine(), out index);

                if (index >= 0)
                {
                    if (index == 0)
                    {
                        flag = false;
                        Console.Clear();
                    }
                    else
                    {
                        shopItemList[index - 1].BuyItem();
                        flag = false;
                        result = true;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }

            return result;
        }

        static bool ItemSellInput()
        {
            bool flag = true;
            bool result = false;

            Console.WriteLine("0번은 돌아가기입니다.");
            while (flag)
            {
                int index;
                int.TryParse(Console.ReadLine(), out index);

                if (index >= 0)
                {
                    if (index == 0)
                    {
                        flag = false;
                        Console.Clear();
                    }
                    else
                    {
                        if(!Inventory.Inven[index - 1].IsUsing)
                        {
                            Inventory.Inven[index - 1].SellItem();
                        }
                        else
                        {
                            Console.WriteLine("장착 중인 아이템은 판매 할 수 없습니다.");
                        }                       
                        flag = false;
                        result = true;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }

            return result;
        }



        public static void BattleEndCheck(bool flag)
        {
            if (Program.gameMonster != null) // 몬스터 정보가 존재
            {
                if (Program.gameMonster.Hp <= 0) // 몬스터 사망 확인
                {
                    if (Program.nextStageType == StageType.Dungeon) // 던전일 경우
                    {
                        Program.dungeonProgress++;  // 던전 진행도 증가
                        Program.nextStageType = StageType.Dungeon;
                        //Program.currentStage = StagePreset.dungeonStageList[Program.dungeonProgress].DeepCopy();    // 다음 스테이지 로드
                        flag = false;
                        Console.WriteLine("던전 몬스터 사망 확인");
                        Program.gamePlayer.GetExp((Program.dungeonProgress + 1)* 10);
                        Program.gamePlayer.GetGold((Program.dungeonProgress + 1) * 5);
                    }
                    else // 그냥 전투일 경우
                    {
                        Program.nextStageType = StageType.System;
                        //Program.currentStage = StagePreset.systemStageList[0].DeepCopy(); // 마을로 복귀
                        flag = false;
                    }
                }
            }
        }

        static void BackToStart()
        {
            Program.nextStageType = StageType.System;
            //Program.currentStage = StagePreset.systemStageList[0].DeepCopy() ;
        }
    }
}
