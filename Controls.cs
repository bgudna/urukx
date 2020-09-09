using System;
using Microsoft.Xna.Framework;
using urukx.Entities;

namespace urukx {

    public class Controls {
        
        public Controls() {

        }

        public bool MoveBeingBy(Being hero, Point position) {
            return hero.MoveBy(position);
        }
        
    }
}