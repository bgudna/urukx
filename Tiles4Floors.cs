using System;
using Microsoft.Xna.Framework;

namespace urukx 
{
    public class Tiles4Floors : Tiles 
    {
        public Tiles4Floors(bool blockingMovement=false, bool blockingLineOfSight=false) : base(Color.DarkGreen, Color.Transparent, '.', blockingMovement, blockingLineOfSight)
        {
            Name = "Floor";
        }
    }
} 