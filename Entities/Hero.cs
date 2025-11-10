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

        // Override MoveBy to recalculate FOV after moving
        public override bool MoveBy(Point positionChange)
        {
            bool moveSuccessful = base.MoveBy(positionChange);
            if (moveSuccessful)
            {
                UpdateFOV();
            }
            return moveSuccessful;
        }

        public void UpdateFOV()
        {
            if (MainLoop.World?.CurrentMap?.PlayerFOV == null)
            {
                return; // FOV not initialized yet
            }
            
            MainLoop.World.CurrentMap.PlayerFOV.Calculate(Position, MainLoop.World.CurrentMap.FOVRadius);
            UpdateTileVisibility();
        }

        private void UpdateTileVisibility()
        {
            Map currentMap = MainLoop.World.CurrentMap;
            
            for (int i = 0; i < currentMap.Tiles.Length; i++)
            {
                Point tilePos = new Point(i % currentMap.Width, i / currentMap.Width);
                bool inFOV = currentMap.IsInFOV(tilePos);
                
                if (inFOV)
                    currentMap.SetExplored(i);
                
                currentMap.Tiles[i].UpdateVisibility(inFOV, currentMap.IsExplored(i));
            }
        }
    }
}
