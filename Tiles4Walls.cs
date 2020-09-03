using System;
using Microsoft.Xna.Framework;

namespace urukx 
{
    public class Tiles4Walls : Tiles 
    {
        public Tiles4Walls(bool blockingMovement=true, bool blockingLineOfSight=true) : base(Color.LightGray, Color.Transparent, '#', blockingMovement, blockingLineOfSight)
        {
            Name = "Wall";
        }
    }
} 