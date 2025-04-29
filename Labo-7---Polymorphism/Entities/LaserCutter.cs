using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    internal class LaserCutter : Router
    {
        public double Accuracy { get; set; }

        public override int LifeSpanCostPerMinute
        {
            get
            {
                return 1500;
            }
        }
        public LaserCutter(string name, double width, double length, double costPerMinute, double accuracy) : base(name, width, length, costPerMinute)
        {
            this.Accuracy = accuracy;
            base.LifeSpan = 5000;
        }

        public override void Use(int numberOfMinuts)
        {
            base.LifeSpan -= (numberOfMinuts * this.LifeSpanCostPerMinute) + 100;
        }
        public override string ToString()
        {
            return $"LASER: '{base.Name} ({this.WorkSpaceLength}x{this.WorkSpaceWidth})" +
                   $" [accuracy:{this.Accuracy}] <lifspan:{base.LifeSpanInfo()} h> '";
        }
    }
}
