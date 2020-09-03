using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using SadConsole.Components;

namespace urukx {

    class MainLoop {
        public const int height = 25;
        public const int width = 80;

        public static UIstuff UIManager;
        public static World World;

        static void Main() {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(width, height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }
        
        private static void Update(GameTime time)
        {

        }
 
        static void Init() {
            UIManager = new UIstuff();

            World = new World();

            UIManager.Init();

        }
    }
}