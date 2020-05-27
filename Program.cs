using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace urukx
{

    class Program
    {
        public const int height = 25;
        public const int width = 80;
        private static SadConsole.Entities.Entity player;

        static void Main()
        {
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

        private static void Update(GameTime time)
        {
            
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
        }

        static void Init()
        {
            var console = new Console(width, height);
            //console.FillWithRandomGarbage();
            console.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, 0);
            console.Print(4, 4, "haro haro");

            SadConsole.Global.CurrentScreen = console;

            createPlayerPlz();

            console.Children.Add(player);
        }
    }
}