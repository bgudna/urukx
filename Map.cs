using System;
using System.Linq;
using Microsoft.Xna.Framework;
using urukx.Entities;


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

        public GoRogue.MultiSpatialMap<Entity> Entities; // Keeps track of all the Entities on the map
        public static GoRogue.IDGenerator IDGenerator = new GoRogue.IDGenerator(); // A static IDGenerator that all Entities can access

        // Add FOV system
        public GoRogue.FOV PlayerFOV;
        private bool[] _exploredTiles; // Track which tiles have been seen
        public int FOVRadius = 10; // How far the player can see

        //Build a new map with a specified width and height
        public Map(int width, int height)
        {
            _width = width;
            _height = height;
            Tiles = new Tiles[width * height];
            Entities = new GoRogue.MultiSpatialMap<Entity>();
            
            // Initialize explored tiles array
            _exploredTiles = new bool[width * height];
        }

        // Initialize FOV after tiles are populated
        public void InitializeFOV()
        {
            PlayerFOV = new GoRogue.FOV(new GoRogue.MapViews.LambdaTranslationMap<Tiles, bool>(
                new GoRogue.MapViews.ArrayMap<Tiles>(Tiles, _width),
                tile => tile.IsBlockingLineOfSight));
            
            // Hide all tiles initially (they haven't been explored yet)
            HideAllTiles();
        }

        // Hide all tiles at startup
        private void HideAllTiles()
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i].UpdateVisibility(false, false);
            }
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

        // Checking whether a certain type of
        // entity is at a specified location the manager's list of entities
        // and if it exists, return that Entity
        public T GetEntityAt<T>(Point location) where T : Entity
        {
            return Entities.GetItems(location).OfType<T>().FirstOrDefault();
        }

        // Removes an Entity from the MultiSpatialMap
        public void Remove(Entity entity)
        {
            // remove from SpatialMap
            Entities.Remove(entity);

            // Link up the entity's Moved event to a new handler
            entity.Moved -= OnEntityMoved;
        }

        // Adds an Entity to the MultiSpatialMap
        public void Add(Entity entity)
        {
            // add entity to the SpatialMap
            Entities.Add(entity, entity.Position);

            // Link up the entity's Moved event to a new handler
            entity.Moved += OnEntityMoved;
        }

        // When the Entity's .Moved value changes, it triggers this event handler
        // which updates the Entity's current position in the SpatialMap
        private void OnEntityMoved(object sender, Entity.EntityMovedEventArgs args)
        {
            Entities.Move(args.Entity as Entity, args.Entity.Position);
        }

        public bool IsExplored(int index)
        {
            return _exploredTiles[index];
        }

        public void SetExplored(int index)
        {
            _exploredTiles[index] = true;
        }

        public bool IsInFOV(Point position)
        {
            return PlayerFOV.BooleanFOV[position.X, position.Y];
        }
    }
}