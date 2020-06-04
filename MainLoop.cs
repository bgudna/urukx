using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace urukx {

    class MainLoop {
        public const int height = 25;
        public const int width = 80;
        private static Hero player;

        public static Map GameMap;
        private static int _mapWidth = 100;
        private static int _mapHeight = 100;
        private static int _maxRooms = 500;
        private static int _minRoomSize = 4;
        private static int _maxRoomSize = 15;

        static void Main() {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(width, height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void createPlayerPlz() {
            player = new Hero(Color.YellowGreen, Color.Transparent);
            player.Position = new Point(5,5);
        }

        // private static void createFloorsPlz() {
        //     for(int x = 0; x < _roomWidth; x++) {
        //         for(int y = 0; y < _roomHeight; y++) {
        //             _allTiles[y * width + x] = new Tiles4Floors();
        //         }
        //     }
        // }

        // private static void createWallsPlz() {
        //     _allTiles = new Tiles[width * height];

        //     for(int i = 0;i < _allTiles.Length; i++) {
        //         _allTiles[i] = new Tiles4Walls();
        //     }
        // }

        private static void Check4Input() {
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.K)))
            {
                player.MoveBy(new Point(0, -1));
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.J)))
            {
                player.MoveBy(new Point(0, 1));
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.H)))
            {
                player.MoveBy(new Point(-1, 0));
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.L)))
            {
                player.MoveBy(new Point(1, 0));
            }
        }

        private static void Update(GameTime time) {
            Check4Input();
        }

        static void Init() {
            // createWallsPlz();
            // createFloorsPlz();
            
            // Initialize an empty map
            GameMap = new Map(_mapWidth, _mapHeight);

            // Instantiate a new map generator and
            // populate the map with rooms and tunnels
            MapGenerator mapGen = new MapGenerator();
            GameMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);

            Console beginConsole = new ScrollingConsole(GameMap.Width, GameMap.Height, Global.FontDefault, new Rectangle(0, 0, width, height), GameMap.Tiles);


            // var console = new ScrollingConsole(width, height, Global.FontDefault, new Rectangle(0, 0, width, height), _allTiles);

            SadConsole.Global.CurrentScreen = beginConsole;

            createPlayerPlz();

            beginConsole.Children.Add(player);
        }
    }
}