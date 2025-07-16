using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20;

namespace TextRPG_Team20
{
    internal class Character
    {
        public string Job { get; set; }
        public int Gold { get; private set; }
        public Status status { get; set; }
        public Inventory Inventory { get; private set; }
        

        public virtual void Action()
        {
            Console.WriteLine();
        }


        public Character(string? name, string job, int gold, Status status)
        {
            if(name == null)
            {
                status.Name = "";
            }
            else
            {
                status.Name = name;
            }
            Job = job;
            Gold = gold;
            this.status = status;

            Inventory = new Inventory();
        }

        public void Attack(Character target)
        {
            int damage = status.TotalAtk;
            Console.WriteLine($"{status.Name}이(가) {target.status.Name}을(를) 공격했습니다! ({damage} 데미지)");
            target.DecreaseHp(damage);
        }

        public void IncreaseHp(int amount)
        {
            status.Hp += amount;
            Console.WriteLine($"{status.Name}의 체력이 {amount}만큼 회복되어 {status.Hp}가 되었습니다.");
        }

        public void DecreaseHp(int amount)
        {
            status.Hp -= amount;
            if (status.Hp < 0) status.Hp = 0;

            Console.WriteLine($"{status.Name}이(가) {amount}의 피해를 입었습니다. 현재 체력: {status.Hp}");
        }

        public virtual void CharacterInfo()
        {
            Console.WriteLine($"Lv. {status.Level:D2}");
            Console.WriteLine($"{status.Name} {{ {Job} }}");
            Console.WriteLine($"공격력 : {status.TotalAtk}");
            Console.WriteLine($"방어력 : {status.TotalDef}");
            Console.WriteLine($"체력 : {status.Hp}");
            Console.WriteLine($"Gold : {Gold} G");
        }
    }
}