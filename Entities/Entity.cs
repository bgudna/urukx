using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace urukx.Entities
{
    public abstract class Entity : SadConsole.Entities.Entity, GoRogue.IHasID
    {
        public uint ID { get; set; } // for keeping the entities unique id

        protected Entity(Color foreground, Color background, int glyph, int width = 1, int height = 1) : base(width, height)
        {
            Animation.CurrentFrame[0].Foreground = foreground;
            Animation.CurrentFrame[0].Background = background;
            Animation.CurrentFrame[0].Glyph = glyph;

            ID = Map.IDGenerator.UseID();
        }
    }
}
