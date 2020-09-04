using Microsoft.Xna.Framework;

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

        //private int _health;
        //private int _maxHealth;

        //public int Health {
        //    get { return _health; }
        //    set { _health = value; }
        //}

        //public int MaxHealth {
        //    get { return _maxHealth; }
        //    set { _maxHealth = value; }
        //}        

        protected Being(Color foreground, Color background, int glyph, int width = 1, int height = 1) : base(foreground, background, glyph, width, height) 
        {
            Animation.CurrentFrame[0].Foreground = foreground;
            Animation.CurrentFrame[0].Background = background;
            Animation.CurrentFrame[0].Glyph = glyph;
        }

        public bool MoveBy(Point positionChange)
        {
            if(MainLoop.World.CurrentMap.IsTileWalkable(Position + positionChange)) {
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