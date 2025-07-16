using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20.Scene
{
    internal class Battle
    {
        //충돌하면
        //player enemy join
        // OnBattle

        // player turn
        // player attack
        // enemy turn 
        // enemy attack

        public static bool OnBattle(Player player, Enemy enemy)
        {

            {
                //player turn
                player.Action();

                ConsoleUI.inputView.ClearBuffer();
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{player.status.Name}의 차례입니다. 행동을 선택하세요.{AnsiColor.Reset}", ref ConsoleUI.inputView);
                ConsoleUI.Instance.DrawTextInBox("1. 일반 공격", ref ConsoleUI.inputView);
                ConsoleUI.Instance.DrawTextInBox("2. 스킬 사용", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);



                player.Attack(enemy);


            }
            ;



            return false; // 둘 다 살아있으면 전투 계속
        }
    }

    
    }
