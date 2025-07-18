using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class potion : ConsumeItem
    {
        public potion(ItemData itemData) : base(itemData)
        {

        }

        public potion()
        {

        }
        public override void useitem()
        {

            int recoverAmount = 0;

            switch (data.Name)
            {
                case "빨간 포션":
                    recoverAmount = 30;
                    break;

                case "하얀 포션":
                    recoverAmount = 70;
                    break;

                case "파워엘릭서":
                    recoverAmount = Game.playerInstance.status.MaxHP - Game.playerInstance.status.HP;
                    break;

                default:
                    Console.WriteLine("알 수 없는 포션입니다.");
                    return;
            }

            // 체력 회복
            Game.playerInstance.IncreaseHp(recoverAmount);
            Console.WriteLine($"{data.Name}을(를) 사용하여 {recoverAmount}만큼 체력을 회복했습니다!");

        }
    }
}
