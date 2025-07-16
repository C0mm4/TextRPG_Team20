using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class Enemy : Character, ICloneable
    {

        public Enemy(string name, Status status) : base(name, "Enemy", 0, status)
        {

        }

        public override void Action()
        {
            Console.WriteLine($"{status.Name}이(가) 공격을 준비합니다.");
            Attack();
        }

        public virtual void Attack()
        {

        }

        public object Clone()
        {
            var clone = Activator.CreateInstance(this.GetType()) as Enemy;
            clone.status = new Status
            (
                status.ID,
                status.Level,
                status.Hp,
                status.Atk,
                status.Def,
                status.ExtraAtk,
                status.ExtraDef
            );
            return clone;
        }
    }
}
