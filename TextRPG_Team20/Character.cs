using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class Character
    {
        public int Level { get; }
        public string Name { get; }
        public string Job { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }

        public int ExtraAtk { get; private set; }
        public int ExtraDef { get; private set; }

        public virtual void Action()
        {
            Console.WriteLine();
        }





        public Character(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        public void Attack(Character target)
        {
            int damage = Atk + ExtraAtk;
            Console.WriteLine($"{Name}이(가) {target.Name}을(를) 공격했습니다! ({damage} 데미지)");
            target.DecreaseHp(damage);
        }

        public void IncreaseHp(int amount)
        {
            Hp += amount;
            Console.WriteLine($"{Name}의 체력이 {amount}만큼 회복되어 {Hp}가 되었습니다.");
        }

        public void DecreaseHp(int amount)
        {
            Hp -= amount;
            if (Hp < 0)
                Hp = 0;

            Console.WriteLine($"{Name}이(가) {amount}의 피해를 입었습니다. 현재 체력: {Hp}");
        }

        public void CharacterInfo()
        {
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} {{ {Job} }}");
            Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
            Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
        }
    }
}
