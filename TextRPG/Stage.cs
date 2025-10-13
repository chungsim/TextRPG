using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    public enum StageType {Battle, Event, Shop, System , Dungeon}
    public class Stage
    {
        public string Name { get; private set; }

        public StageType Type { get; private set; }
        public int ClearGold { get; set; }
        public int ClearExp { get; set; }

        public Monster? Monster { get; private set; }

        public string StageScript { get; private set; }

        public Stage(string name, StageType type ,int clearGold, int clearExp, Monster? monster, string stageScript) 
        {
            Name = name;
            Type = type;
            ClearGold = clearGold;
            ClearExp = clearExp;
            Monster = monster;
            StageScript = stageScript;
        }

        public Stage DeepCopy()
        {
            Stage copy = new Stage("", StageType.Event, 0, 0, null, "");
            copy.Name = Name;
            copy.Type = Type;
            copy.ClearGold = ClearGold;
            copy.ClearExp = ClearExp;
            copy.Monster = Monster;
            copy.StageScript = StageScript;
            return copy;

        }

        // 생성할 스테이지 정보 불러오기
        public void GetStageInfo(Stage targetStage)
        {
            Name = targetStage.Name;
            Type = targetStage.Type;
            ClearGold = targetStage.ClearGold;
            ClearExp = targetStage.ClearExp;
            Monster = targetStage.Monster;
            StageScript = targetStage.StageScript;
        }

        public List<string> StageInfo()
        {
            List<string> result = new List<string>();

            switch (Type)
            {
                case StageType.Battle:
                    result.Add("적이 나타났다!");
                    //result.Add($"[{Monster.Name}]");
                    //result.Add($"HP: {Monster.Hp}");
                    //result.Add($"ATK: {Monster.Atk}");
                    //result.Add($"DEF: {Monster.Def}");
                    //result.Add($"{StageScript}");
                    break;

                case StageType.Event:
                    result.Add("이벤트 스테이지");
                    result.Add($"{StageScript}");
                    //result.Add($"[{Monster.Name}]");
                    //result.Add($"HP: {Monster.Hp}");
                    //result.Add($"ATK: {Monster.Atk}");
                    //result.Add($"DEF: {Monster.Def}");
                    break;

                case StageType.Shop:
                    result.Add("상점 스테이지");
                    result.Add($"{StageScript}");
                    //result.Add($"[{Monster.Name}]");
                    //result.Add($"HP: {Monster.Hp}");
                    //result.Add($"ATK: {Monster.Atk}");
                    //result.Add($"DEF: {Monster.Def}");
                    break;
                case StageType.System:
                    result.Add("시스템 스테이지");
                    result.Add($"{StageScript}");
                    //result.Add($"[{Monster.Name}]");
                    //result.Add($"HP: {Monster.Hp}");
                    //result.Add($"ATK: {Monster.Atk}");
                    //result.Add($"DEF: {Monster.Def}");
                    break;
            }

            return result;
        }

        

    }

    public class StagePreset
    {
        public static List<Stage> battleStageList = new List<Stage>() { 
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[0],""),
                new Stage("wood2", StageType.Battle, 10, 10, MonsterPreset.monsterList[1],""),
                new Stage("wood3", StageType.Battle, 10, 10, MonsterPreset.monsterList[2],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[3],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[4],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[5],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[6],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[7],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[8],"")
        };

        public static List<Stage> dungeonStageList = new List<Stage>() {
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[0],""),
                new Stage("wood2", StageType.Battle, 10, 10, MonsterPreset.monsterList[1],""),
                new Stage("wood3", StageType.Battle, 10, 10, MonsterPreset.monsterList[2],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[3],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[4],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[5],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[6],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[7],""),
                new Stage("wood1", StageType.Battle, 10, 10, MonsterPreset.monsterList[8],"")
        };

        public static List<Stage> eventStageList = new List<Stage>() {
                 new Stage("gold1", StageType.Event, 20, 50, null,"50% 확률로 20골드 획득, 실패 시 동일한 hp 상실"),
                 new Stage("gold2", StageType.Event, 40, 50, null,"50% 확률로 40골드 획득, 실패 시 동일한 hp 상실"),
                 new Stage("gold3", StageType.Event, 60, 50, null,"50% 확률로 60골드 획득, 실패 시 동일한 hp 상실"),
                 new Stage("gold4", StageType.Event, 80, 50, null,"50% 확률로 80골드 획득, 실패 시 동일한 hp 상실"),
                 new Stage("gold5", StageType.Event, 100, 50, null,"50% 확률로 100골드 획득, 실패 시 동일한 hp 상실"),
                 new Stage("hp1", StageType.Event, -50, 0, null,"hp 50 회복")
        };

        public static List<Stage> shopStageList = new List<Stage>() {
                 new Stage("Shop", StageType.Shop, 0, 0, null,"")
        };

        public static List<Stage> systemStageList = new List<Stage>() {
                 new Stage("start", StageType.System, 0, 0, null,"스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.")
        };
    }
}
