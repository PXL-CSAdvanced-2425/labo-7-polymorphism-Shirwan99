using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    internal class General : Machine
    {
        public override int LifeSpanCostPerMinute
        {
            get
            {
                return 1;
            }
        }
        public override void Use(int numberOfMinuts)
        {
            base.LifeSpan -= (int)(numberOfMinuts * this.LifeSpanCostPerMinute);
        }
        public General(string name) : base(name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return $"GENERAL: '{base.Name}' {base.LifeSpanInfo()}";
        }
    }
}
