using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    public abstract class Machine
    {
        //this en base gebruiken wij zodat wij weten van welke class dat de prop komen
        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _lifeSpan;

        public int LifeSpan
        {
            get { return _lifeSpan; }
            set { _lifeSpan = value; }
        }
        private float _price;

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public bool OutOfUse
        {
            get 
            { 
                return this.LifeSpan <= 0;
            }
        }
        #endregion

        public abstract int LifeSpanCostPerMinute { get;}

        protected Machine(string name)
        {
            this.Name = name;
        }
        #region Methodes & Functions

        public abstract void Use(int numberOfMinuts);

        public string LifeSpanInfo()
        {
            if (this.OutOfUse)
            {
                return "OUT OF USE";
            }
            else
            {
                return $"<LifeSpan> {this.LifeSpan} h>";
            }
        }
        public override string ToString()
        {
            return this.LifeSpanInfo();
        }
        #endregion

    }
}
