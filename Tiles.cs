using System;
using Microsoft.Xna.Framework;
using SadConsole;
namespace urukx
{
    public abstract class Tiles : Cell
    {
        public bool IsBlockingMovement;
        public bool IsBlockingLineOfSight;
        protected string Name;

        public Tiles(Color foreground, Color background, int glyph, bool blockingMovement=false, bool blockingLineOfSight=false, String name="") : base(foreground, background, glyph)
        {
            IsBlockingMovement = blockingMovement;
            IsBlockingLineOfSight = blockingLineOfSight;
            Name = name;
        }
    }
}