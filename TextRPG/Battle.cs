using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //전트의 모든 선택지 구현
    public class Battle
    {
        public static void PlayerAttack(Character target)
        {
            Player player = Program.gamePlayer;
            int damage = player.Atk + player.BonusAtk - target.Def; // 방어력이 더 높으면 1 데미지
            damage = damage > 0 ? damage : 1;

            target.TakeDamage(damage);
        }

        public static void MonsterAttack(Monster monster)
        {
            Player player = Program.gamePlayer;
            int damage = monster.Atk - (player.Def + player.BonusDef);
            damage = damage > 0 ? damage : 1;

            player.TakeDamage(damage);
        }
    }
}
