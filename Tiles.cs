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
        
        // Store original colors for when tile goes out of view
        private Color _originalForeground;
        private Color _originalBackground;
        private int _originalGlyph;

        public Tiles(Color foreground, Color background, int glyph, bool blockingMovement=false, bool blockingLineOfSight=false, String name="") : base(foreground, background, glyph)
        {
            IsBlockingMovement = blockingMovement;
            IsBlockingLineOfSight = blockingLineOfSight;
            Name = name;
            _originalForeground = foreground;
            _originalBackground = background;
            _originalGlyph = glyph;
        }

        public void UpdateVisibility(bool isVisible, bool isExplored)
        {
            if (isVisible)
            {
                // Tile is currently visible - show at full brightness
                Foreground = _originalForeground;
                Background = _originalBackground;
                Glyph = _originalGlyph;
            }
            else if (isExplored)
            {
                // Tile has been seen before but not currently visible - dim it
                Foreground = Color.Lerp(_originalForeground, Color.Black, 0.6f);
                Background = Color.Lerp(_originalBackground, Color.Black, 0.6f);
                Glyph = _originalGlyph;
            }
            else
            {
                // Tile has never been seen - hide it completely
                Foreground = Color.Black;
                Background = Color.Black;
                Glyph = 0; // Use empty glyph for unexplored tiles
            }
        }
    }
}