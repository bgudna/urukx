using System;
using Microsoft.Xna.Framework;
namespace urukx
{
    public class TileDoor : Tiles
    {

        public bool Locked; // Locked door = 1, Unlocked = 0
        public bool IsOpen; // Open door = 1, closed = 0

        //Default constructor
        //A TileDoor can be set locked/unlocked/open/closed using the constructor.
        public TileDoor(bool locked, bool open) : base(Color.Gray, Color.Transparent, '+')
        {
            //+ is the closed glyph
            //closed by default
            Glyph = '+';

            //Update door fields
            Locked = locked;
            IsOpen = open;

            //change the symbol to open if the door is open
            if (!Locked && IsOpen)
                Open();
            else if (Locked || !IsOpen)
                Close();
        }

        //closes a door
        public void Close()
        {
            IsOpen = false;
            Glyph = '+';
            IsBlockingLineOfSight = true;
            IsBlockingMovement = true;
        }

        //opens a door
        public void Open()
        {
            IsOpen = true;
            IsBlockingLineOfSight = false;
            IsBlockingMovement = false;
            Glyph = '-';
        }
    }
}