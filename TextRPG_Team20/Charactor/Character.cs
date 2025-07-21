using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20;
using TextRPG_Team20.Dungeon;

namespace TextRPG_Team20
{
    internal abstract class Character
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
			status.Gold -= gold;
			if (status.Gold < 0)
			{
				status.Gold = 0;   // 혹시 모를 음수 방지
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

        public Character(JobType jobType, Status status)
        {
            Job = jobType;
            this.status = status;

            Inventory = new Inventory(this);
        }

        /// <summary>
        /// target 캐릭터 공격
        /// </summary>
        /// <param name="target">공격 대상</param>
        public virtual void Attack(Character target)
        {
            int damage = status.TotalAtk;
            target.DecreaseHp(damage);
        }

        public void IncreaseHp(int amount)
        {
            status.HP += amount;
            status.HP = Math.Min(status.MaxHP, status.HP);
        }

        public void DecreaseHp(int amount)
        {
            status.HP -= amount;
            if (status.HP < 0) status.HP = 0;

        }

        public virtual void CharacterInfo()
        {
        }
  
    }
}