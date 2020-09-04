using Microsoft.Xna.Framework;
using SadConsole;
using System;
using SadConsole.Controls;
using urukx.Entities;

namespace urukx
{
    class UIstuff : ContainerConsole 
    {
        public Window MapWindow;
        public ScrollingConsole MapConsole;
        public Messages MessageLog;

        public UIstuff()
        {
            IsVisible = true;
            IsFocused = true;

            Parent = SadConsole.Global.CurrentScreen;
        }

        public void CreateMeSomeConsoles()
        {
            //MapConsole = new SadConsole.ScrollingConsole(MainLoop.World.CurrentMap.Width, MainLoop.World.CurrentMap.Height, Global.FontDefault, new Rectangle(0, 0, MainLoop.width, MainLoop.height), MainLoop.World.CurrentMap.Tiles);
            MapConsole = new SadConsole.ScrollingConsole(MainLoop.width, MainLoop.height);
        }

        private void LoadMap(Map map)
        {
            MapConsole = new SadConsole.ScrollingConsole(MainLoop.World.CurrentMap.Width, MainLoop.World.CurrentMap.Height, Global.FontDefault, new Rectangle(0, 0, MainLoop.width, MainLoop.height), map.Tiles);

            SyncMapEntities(map);
        }

        public void KeepCameraOnHero(Hero hero)
        {
            MapConsole.CenterViewPortOnPoint(hero.Position);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            Check4Input();
            base.Update(timeElapsed);
        }

        private void Check4Input()
        {
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.K)))
            {
                //MainLoop.World.Player.MoveBy(new Point(0, -1));
                MainLoop.Commands.MoveBeingBy(MainLoop.World.Player, new Point(0, -1));
                KeepCameraOnHero(MainLoop.World.Player);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.J)))
            {
                MainLoop.Commands.MoveBeingBy(MainLoop.World.Player, new Point(0, 1));
                KeepCameraOnHero(MainLoop.World.Player);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.H)))
            {
                MainLoop.Commands.MoveBeingBy(MainLoop.World.Player, new Point(-1, 0));
                KeepCameraOnHero(MainLoop.World.Player);
            }

            if ((SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right)) || (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.L)))
            {
                MainLoop.Commands.MoveBeingBy(MainLoop.World.Player, new Point(1, 0));
                KeepCameraOnHero(MainLoop.World.Player);
            }

            // Undo last command: Z
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                MainLoop.Commands.UndoMoveBeingBy();
                KeepCameraOnHero(MainLoop.World.Player);
            }

            // Repeat last command: X
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.X))
            {
                MainLoop.Commands.RedoMoveBeingBy();
                KeepCameraOnHero(MainLoop.World.Player);
            }
        }

        // Creates a window that encloses a map console
        // of a specified height and width
        // and displays a centered window title
        // make sure it is added as a child of the UIManager
        // so it is updated and drawn
        public void CreateMapWindow(int width, int height, string title)
        {
            MapWindow = new Window(width, height);
            MapWindow.CanDrag = true;

            //make console short enough to show the window title
            //and borders, and position it away from borders
            int mapConsoleWidth = width - 2;
            int mapConsoleHeight = height - 2;

            // Resize the Map Console's ViewPort to fit inside of the window's borders snugly
            MapConsole.ViewPort = new Rectangle(0, 0, mapConsoleWidth, mapConsoleHeight);

            //reposition the MapConsole so it doesnt overlap with the left/top window edges
            MapConsole.Position = new Point(1, 1);

            //close window button
            Button closeButton = new Button(3, 1);
            closeButton.Position = new Point(0, 0);
            closeButton.Text = "[X]";

            //Add the close button to the Window's list of UI elements
            MapWindow.Add(closeButton);

            // Centre the title text at the top of the window
            MapWindow.Title = title.Align(HorizontalAlignment.Center, mapConsoleWidth);

            //add the map viewer to the window
            MapWindow.Children.Add(MapConsole);

            // The MapWindow becomes a child console of the UIManager
            Children.Add(MapWindow);

            // Add the player to the MapConsole's render list
            MapConsole.Children.Add(MainLoop.World.Player);

            // Without this, the window will never be visible on screen
            MapWindow.Show();
        }

        // Adds the entire list of entities found in the
        // World.CurrentMap's Entities SpatialMap to the
        // MapConsole, so they can be seen onscreen
        private void SyncMapEntities(Map map)
        {
            // remove all Entities from the console first
            MapConsole.Children.Clear();

            // Now pull all of the entities into the MapConsole in bulk
            foreach (Entity entity in map.Entities.Items)
            {
                MapConsole.Children.Add(entity);
            }

            // Subscribe to the Entities ItemAdded listener, so we can keep our MapConsole entities in sync
            map.Entities.ItemAdded += OnMapEntityAdded;

            // Subscribe to the Entities ItemRemoved listener, so we can keep our MapConsole entities in sync
            map.Entities.ItemRemoved += OnMapEntityRemoved;
        }

        // Remove an Entity from the MapConsole every time the Map's Entity collection changes
        public void OnMapEntityRemoved(object sender, GoRogue.ItemEventArgs<Entity> args)
        {
            MapConsole.Children.Remove(args.Item);
        }

        // Add an Entity to the MapConsole every time the Map's Entity collection changes
        public void OnMapEntityAdded(object sender, GoRogue.ItemEventArgs<Entity> args)
        {
            MapConsole.Children.Add(args.Item);
        }

        public void Init()
        {
            CreateMeSomeConsoles();
            MessageLog = new Messages(MainLoop.width / 2, MainLoop.height / 2, "Your progress");

            Children.Add(MessageLog);
            MessageLog.Show();
            MessageLog.Position = new Point(0, MainLoop.height / 2);

            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1224");
            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 12543");

            LoadMap(MainLoop.World.CurrentMap);

            CreateMapWindow(MainLoop.width / 2, MainLoop.height / 2, "OverWorld");
            UseMouse = true;

            KeepCameraOnHero(MainLoop.World.Player);
        }
    }
}
