using System;
using Microsoft.Xna.Framework;

namespace urukx.Entities {
    public class Hero : Being {
        public Hero(Color foreground, Color background) : base(foreground, background, '@') {

            Attack = 10;
            AttackChance = 40;
            Defense = 5;
            DefenseChance = 20;
            Name = "John";
            
        }
    }
}
