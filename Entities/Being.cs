using Microsoft.Xna.Framework;
using System.Collections.Generic;

// old Actor class

namespace urukx.Entities {

    public abstract class Being : Entity {

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Gold { get; set; }
        public List<Item> Inventory = new List<Item>();

        protected Being(Color foreground, Color background, int glyph, int width = 1, int height = 1) : base(foreground, background, glyph) 
        {
            Animation.CurrentFrame[0].Foreground = foreground;
            Animation.CurrentFrame[0].Background = background;
            Animation.CurrentFrame[0].Glyph = glyph;
        }

        public virtual bool MoveBy(Point positionChange)
        {
            if(MainLoop.World.CurrentMap.IsTileWalkable(Position + positionChange)) {

                NonHero monster = MainLoop.World.CurrentMap.GetEntityAt<NonHero>(Position + positionChange);
                
                Item item = MainLoop.World.CurrentMap.GetEntityAt<Item>(Position + positionChange);

                if (monster != null)
                {
                    MainLoop.Commands.Attack(this, monster);
                    return true;
                } else if (item != null)
                {
                    MainLoop.Commands.Pickup(this, item);
                    return true;
                }

                Position += positionChange;
                return true;
            } else {
                return false;
            }
            
        }

        // Moves the Actor TO newPosition location
        // returns true if actor was able to move, false if failed to move
        public bool MoveTo(Point newPosition)
        {
            Position = newPosition;
            return true;
        }

    }
}