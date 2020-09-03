using System;
using Microsoft.Xna.Framework;


namespace urukx 
{

    public class Map 
    {
        private static Tiles[] _allTiles; // contain all tile objects
        private int _width;
        private int _height;

        public Tiles[] Tiles { get { return _allTiles; } set { _allTiles = value; }}
        public int Width { get { return _width; } set { _width = value; }}
        public int Height { get { return _height; } set { _height = value; }}


        //Build a new map with a specified width and height
        public Map(int width, int height)
        {
            _width = width;
            _height = height;
            Tiles = new Tiles[width * height];

        }

        public bool IsTileWalkable(Point location)
        {
            // first make sure that actor isn't trying to move
            // off the limits of the map
            if (location.X < 0 || location.Y < 0 || location.X >= Width || location.Y >= Height)
                return false;
            // then return whether the tile is walkable
            return !_allTiles[location.Y * Width + location.X].IsBlockingMovement;
        }

    }
}