using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class BattleScene : Scene
    {
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();

        public BattleScene(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
        }

        public override void PrintScene()

        {
            
            // 모든 영역 초기화
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            // 메인 뷰: 전투 메뉴
            ConsoleUI.Instance.DrawTextInBox("=== Battle Scene ===", ref ConsoleUI.mainView);
            Battle.OnBattle(player);

            //  플레이어 정보
            ConsoleUI.Instance.DrawTextInBox($"[{player.status.Name}] - {player.Job}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"HP: {player.status.Hp}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"ATK: {player.status.TotalAtk}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"DEF: {player.status.TotalDef}", ref ConsoleUI.info1View);



            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        
        }

        public override bool Action(int input)
        {
            

            if (player.status.Hp <= 0)
            {
                Game.Instance.SceneChange(Game.SceneState.Result);
                return false;
            }

            // 플레이어가 행동을 선택함: input
            Enemy target = null;

            if (enemies.Count > 1)
            {
                // 적 목록 보여주기
                ConsoleUI.inputView.ClearBuffer();
               
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (!enemies[i].IsDead)
                    {
                        ConsoleUI.Instance.DrawTextInBox($"{i + 1}. {enemies[i].status.Name} (HP: {enemies[i].status.Hp})", ref ConsoleUI.inputView);
                    }
                }
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
                ConsoleUI.Instance.DrawTextInBox("공격할 적을 선택하세요:", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
                string? s = ConsoleUI.Read(ref ConsoleUI.inputView);
                if (int.TryParse(s, out int targetIndex) && targetIndex > 0 && targetIndex <= enemies.Count)
                {
                    target = enemies[targetIndex - 1];
                    if (target.IsDead)
                    {
                        ConsoleUI.Instance.DrawTextInBox("이미 쓰러진 적을 선택했습니다.", ref ConsoleUI.logView);
                        ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                        return true;
                    }
                }
                else
                {
                    ConsoleUI.Instance.DrawTextInBox("잘못된 대상입니다. 턴이 소모됩니다!", ref ConsoleUI.logView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                    Battle.EnemyTurn(enemies, player);
                    return !player.IsDead;
                }
            }
            else
            {
                // 적이 하나면 자동선택
                target = enemies.FirstOrDefault(e => !e.IsDead);
                if (target == null)
                {
                    ConsoleUI.Instance.DrawTextInBox("공격할 적이 없습니다!", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Win);
                    return false;
                }
            }

            // 행동 처리
            switch (input)
            {
                case 1:
                    {
                        Enemy enemy = Battle.SelectEnemy(Game.enemies);
                        player.Attack(target);
                        Battle.CheckEnemies(player, Game.enemies);
                        break;
                    }

                case 2:
                    {
                        Enemy enemy = Battle.SelectEnemy(Game.enemies);
                        player.UseSkill(target);
                        Battle.CheckEnemies(player, Game.enemies);
                        break;
                    }

                default:
                    Battle.Miss(player, Game.enemies);
                    ((Scene)this).InvalidInput();
                    break;
            }

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            return true;
        }


        //public override bool Action(int input)
        //{

        //    if (player.status.Hp > 0)
        //    {
        //        switch (input)
        //        {
        //            case 1: // 공격
        //                Battle.OnNormalAttack(player, enemy);
        //                break;

        //            case 2: //스킬사용
        //                Battle.OnSkillAttack(player, enemy);
        //                break;

        //            default: // 잘못입력
        //                Battle.Miss(player, enemy);
        //                ((Scene)this).InvalidInput();
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        Game.Instance.SceneChange(Game.SceneState.Result);
        //    }
        //    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
        //    return true;
        //}
    }
}
