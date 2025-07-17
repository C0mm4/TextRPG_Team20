using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20;
using TextRPG_Team20.Dungeon;
using TextRPG_Team20.Item;

namespace TextRPG_Team20
{
    internal abstract class Character : IComponent
    {
        public JobType Job { get; protected set; }
        public Status status { get; set; }
        public Inventory Inventory { get; private set; }

        

        public int x, y;

        protected int currentPosData;

        public virtual void AddGold(int gold)
        {
            status.Gold += gold;
        }
		public void DecreaseGold(int gold)
		{
			Gold -= gold;
            if (Gold <0)
            {
                Gold = 0;   // 혹시 모를 음수 방지
            }
		}

		public virtual void Action()
        {
            Console.WriteLine();
        }

        public Character()
        {
            Job = JobType.None;
        }
        


        public Character(string? name, JobType jobType, int gold, Status status)
        {
            if (name == null)
            {
                status.Name = "";
            }
            else
            {
                status.Name = name;
            }
            Job = jobType;
            status.Gold = gold;
            this.status = status;

            Inventory = new Inventory(this);
        }

        public void SetPos(int x, int y)
        {
            currentPosData = DungeonManager.Instance.currentField[x, y];

            this.x = x; this.y = y;

            DungeonManager.Instance.currentField[x, y] = -1;

        }

        public virtual void Attack(Character target)
        {
            int damage = status.TotalAtk;
            //Console.WriteLine($"{status.Name}이(가) {target.status.Name}을(를) 공격했습니다! ({damage} 데미지)");
            target.DecreaseHp(damage);
        }

        public void IncreaseHp(int amount)
        {
            status.HP += amount;
            //Console.WriteLine($"{status.Name}의 체력이 {amount}만큼 회복되어 {status.Hp}가 되었습니다.");
        }

        public void DecreaseHp(int amount)
        {
            status.HP -= amount;
            if (status.HP < 0) status.HP = 0;

            //Console.WriteLine($"{status.Name}이(가) {amount}의 피해를 입었습니다. 현재 체력: {status.Hp}");
        }

        public virtual void CharacterInfo()
        {
            Console.WriteLine($"Lv. {status.Level:D2}");
            Console.WriteLine($"{status.Name} {{ {Job} }}");
            Console.WriteLine($"공격력 : {status.TotalAtk}");
            Console.WriteLine($"방어력 : {status.TotalDef}");
            Console.WriteLine($"체력 : {status.HP}");
            Console.WriteLine($"Gold : {status.Gold} G");
        }
  


        public abstract void PrintData();

        public abstract void PrintInField();
    }
}