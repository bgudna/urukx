using System;
using SadConsole.Components;
using Microsoft.Xna.Framework;

namespace urukx
{
    public class World
    {
        private static int _mapWidth = 100;
        private static int _mapHeight = 100;
        private static int _maxRooms = 500;
        private static int _minRoomSize = 4;
        private static int _maxRoomSize = 15;
        private Tiles[] _mapTiles;

        public Map CurrentMap { get; set; }

        public Hero Player { get; set; }

        public World()
        {
            CreateMap();

            CreatePlayer();
        }

        private void CreateMap()
        {
            _mapTiles = new Tiles[_mapWidth * _mapHeight];
            CurrentMap = new Map(_mapWidth, _mapHeight);
            MapGenerator mapGen = new MapGenerator();
            CurrentMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);
        }

        private void CreatePlayer()
        {
            Player = new Hero(Color.Yellow, Color.Transparent);
            //Player.Position = new Point(5, 5);

            // Place the player on the first non-movement-blocking tile on the map
            for (int i = 0; i < CurrentMap.Tiles.Length; i++)
            {
                if (!CurrentMap.Tiles[i].IsBlockingMovement)
                {
                    // Set the player's position to the index of the current map position
                    Player.Position = SadConsole.Helpers.GetPointFromIndex(i, CurrentMap.Width);
                }
            }

            // Add the ViewPort sync Component to the player
            Player.Components.Add(new EntityViewSyncComponent());
        }
    }
}
