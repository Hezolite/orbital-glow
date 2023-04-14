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
    internal class ControlHandler : Game1
    {
        private static KeyboardState _keyboardState;
        private static Point _direction;
        public static Point Direction => _direction;
        public static Point MousePosition => Mouse.GetState().Position;

        public static void Update()
        {
            var keyboardState = Keyboard.GetState();

            _direction = Point.Zero;

            if (keyboardState.IsKeyDown(Keys.W) && _keyboardState.IsKeyDown(Keys.W)) _direction.Y--;
            if (keyboardState.IsKeyDown(Keys.S) && _keyboardState.IsKeyDown(Keys.S)) _direction.Y++;
            if (keyboardState.IsKeyDown(Keys.A) && _keyboardState.IsKeyDown(Keys.A)) _direction.X--;
            if (keyboardState.IsKeyDown(Keys.D) && _keyboardState.IsKeyDown(Keys.D)) _direction.X++;

            _keyboardState = keyboardState;
        }
    }
}
