using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Event
    {
        
        public static void eventRoll()
        {
            if(Program.currentStage.Type == StageType.Event)
            {
                Random random = new Random();
                int ranInt = random.Next(0, 100);
                if(ranInt < Program.currentStage.ClearExp)
                {
                    Program.gamePlayer.GetGold(Program.currentStage.ClearGold);
                }
                else
                {
                    Program.gamePlayer.TakeDamage(Program.currentStage.ClearGold);
                }
            }
        }
    }
}
