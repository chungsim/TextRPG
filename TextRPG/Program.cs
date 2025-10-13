namespace TextRPG
{
    internal class Program
    {
        public static Stage currentStage = new Stage("none", StageType.Event,0, 0,null, "");
        public static Player gamePlayer = new Player("testname", 100, 10, 10);
        public static Monster gameMonster = new Monster("testmonster", 100, 10, 10);
        public static int dungeonProgress = 0;
        public static StageType nextStageType = StageType.System;

        static void Main(string[] args)
        {
            // 저장 불러오기
            SaveLoad.LoadData();

            //게임 도입부
            LoadStartStage();
            IOManager.PrintStage(currentStage.StageInfo());
            
            IOManager.UserInput();

            //메인 흐름
            while (true)
            {
                Random random = new Random();
                int idx = 0;

                // 다음 스테이지 종류에 따라 구별
                switch (nextStageType)
                {
                    case StageType.System:
                        currentStage = StagePreset.systemStageList[0].DeepCopy(); // 시작 메뉴
                        dungeonProgress = 0; // 던전 진행도 초기화
                        break;
                    case StageType.Shop:
                        currentStage = StagePreset.shopStageList[gamePlayer.Level - 1].DeepCopy(); // 현재 레벨에 맞는 상점 출현
                        break;
                    case StageType.Event:
                        idx = random.Next(0,StagePreset.eventStageList.Count);
                        currentStage = StagePreset.eventStageList[idx].DeepCopy();  // 랜덤 이벤트 발생
                        break;
                    case StageType.Battle:
                        idx = random.Next(0, StagePreset.battleStageList.Count);
                        currentStage = StagePreset.battleStageList[idx].DeepCopy(); // 랜덤 전투 발생_보류
                        gameMonster = currentStage.Monster.DeepcopyMonster();
                        break;
                    case StageType.Dungeon:
                        currentStage = StagePreset.dungeonStageList[dungeonProgress].DeepCopy(); // 최종 던전 진도부터 진행
                        gameMonster = currentStage.Monster.DeepcopyMonster();
                        break;
                }
                //LoadRandomStage();
                //IOManager.PrintStage(currentStage.StageInfo());
                IOManager.PrintStage(currentStage.StageInfo());
                IOManager.UserInput();
            }
            
        }

        //랜덤 스테이지 호출
        static void LoadRandomStage()
        {
            Random rand = new Random();
            int ranStageType = rand.Next(0,2);
            int idx;
            switch (ranStageType)
            {
                // 전투
                case 0: 
                    idx = rand.Next(0,StagePreset.battleStageList.Count);
                    //currentStage.GetStageInfo(StagePreset.battleStageList[idx]);
                    currentStage = StagePreset.battleStageList[idx].DeepCopy();
                    gameMonster = currentStage.Monster.DeepcopyMonster();
                    break;

                //이벤트
                case 1:
                    idx = rand.Next(0, StagePreset.eventStageList.Count);
                    //currentStage.GetStageInfo(StagePreset.eventStageList[idx]);
                    currentStage = StagePreset.eventStageList[idx].DeepCopy();
                    break;

                //상점_보류
                case 2:
                    idx = rand.Next(0, StagePreset.shopStageList.Count);
                    //currentStage.GetStageInfo(StagePreset.shopStageList[idx]);
                    currentStage = StagePreset.shopStageList[idx].DeepCopy();
                    break;
            }
        }

        static void LoadDungeonStage(int dp)
        {
            currentStage = StagePreset.battleStageList[dp].DeepCopy();
        }

        //시작 스테이지
        static void LoadStartStage()
        {
            currentStage = StagePreset.systemStageList[0].DeepCopy();
            if(Inventory.Inven.Count == 0)
            {
                Inventory.Inven.Add(ItemPreset.itemList[0]);
                Inventory.Inven.Add(ItemPreset.itemList[4]);
                Inventory.Inven.Add(ItemPreset.itemList[5]);
                Inventory.Inven.Add(ItemPreset.itemList[8]);
                
            }           
        }
    }
}
