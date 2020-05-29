using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace urukx {

    class MainLoop {
        public const int height = 25;
        public const int width = 80;
        private static SadConsole.Entities.Entity player;

        private static Tiles[] _allTiles;
        private const int _roomWidth = 10;
        private const int _roomHeight = 20;

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
            player = new SadConsole.Entities.Entity(1,1);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.BlueViolet;
            player.Position = new Point(20,10);
        }

        private static void createFloorsPlz() {
            for(int x = 0; x < _roomWidth; x++) {
                for(int y = 0; y < _roomHeight; y++) {
                    _allTiles[y * width + x] = new Tiles4Floors();
                }
            }
        }

        private static void createWallsPlz() {
            _allTiles = new Tiles[width * height];

            for(int i = 0;i < _allTiles.Length; i++) {
                _allTiles[i] = new Tiles4Walls();
            }
        }

        private static void Update(GameTime time) {
            
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.K)))
            {
                player.Position += new Point(0, -1);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.J)))
            {
                player.Position += new Point(0, 1);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.H)))
            {
                player.Position += new Point(-1, 0);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.L)))
            {
                player.Position += new Point(1, 0);
            }
        }

        static void Init() {
            createWallsPlz();
            createFloorsPlz();
            var console = new ScrollingConsole(width, height, Global.FontDefault, new Rectangle(0, 0, width, height), _allTiles);
            //console.FillWithRandomGarbage();
            // console.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, 0);
            // console.Print(4, 4, "haro haro");

            SadConsole.Global.CurrentScreen = console;

            createPlayerPlz();

            console.Children.Add(player);
        }
    }
}