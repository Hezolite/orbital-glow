using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OrbitalGlow
{
    public class HexMap
    {
        private readonly Point MAP_SIZE = new(24, 24);
        private readonly Point TILE_SIZE; // comes from texture size
        private readonly HexTile[,] _tiles;
        private readonly Vector2 MAP_OFFSET = new(0f, 0f);
        private Point _keyboardSelected = new(0, 0);
        private HexTile _lastMouseSelectedTile;

        public HexMap()
        {
            _tiles = new HexTile[MAP_SIZE.X, MAP_SIZE.Y];

            Texture2D[] textures =
            {
                Globals.Content.Load<Texture2D>("Images/Polygon 1vertical"),
                Globals.Content.Load<Texture2D>("Images/Polygon 2 vertical")
            };

            TILE_SIZE.X = textures[0].Width;
            TILE_SIZE.Y = textures[0].Height;

            Random random = new();

            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    int r = random.Next(0, textures.Length);
                    _tiles[x, y] = new(textures[r], MapToScreen(x, y));
                }
            }

            _tiles[_keyboardSelected.X, _keyboardSelected.Y].KeyboardSelect();
        }

        private Vector2 MapToScreen(int mapX, int mapY)
        {
            var shouldOffset = (mapY % 2) == 1;
            float width = TILE_SIZE.X;
            var height = width;

            var horizontalDistance = width;
            var verticalDistance = height * (3f / 4f);

            var offset = (shouldOffset) ? width/2 : 0;

            var screenX =  (mapX * horizontalDistance) + offset + MAP_OFFSET.X * TILE_SIZE.X;
            var screenY = (mapY * verticalDistance) + MAP_OFFSET.Y * TILE_SIZE.Y;

            return new Vector2(screenX, screenY);
        }

        private Point ScreenToMap(Point mousePos)
        {
            Point cursor = new(mousePos.X - (int)(MAP_OFFSET.X * TILE_SIZE.X), mousePos.Y - (int)(MAP_OFFSET.Y * TILE_SIZE.Y));
            var mapX = (cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2)) / TILE_SIZE.X;
            var mapY = (-cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2)) / TILE_SIZE.X;
            return new(mapX, mapY);
        }

        public void Update()
        {
            _lastMouseSelectedTile?.MouseDeselect();

            var map = ScreenToMap(ControlHandler.MousePosition);

            if(map.X >= 0 && map.Y >= 0 && map.X < MAP_SIZE.X && map.Y < MAP_SIZE.Y)
            {
                _lastMouseSelectedTile = _tiles[map.X, map.Y];
                _lastMouseSelectedTile.MouseSelect();
            }

            if (ControlHandler.Direction != Point.Zero)
            {
                _tiles[_keyboardSelected.X, _keyboardSelected.Y].KeyboardDeselect();
                _keyboardSelected.X = Math.Clamp(_keyboardSelected.X + ControlHandler.Direction.X, 0, MAP_SIZE.X - 1);
                _keyboardSelected.Y = Math.Clamp(_keyboardSelected.Y + ControlHandler.Direction.X, 0, MAP_SIZE.Y - 1);
                _tiles[_keyboardSelected.X, _keyboardSelected.Y].KeyboardSelect();
            }
        }

        public void Draw()
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    _tiles[x, y].Draw();
                }
            }
        }
    }
}
