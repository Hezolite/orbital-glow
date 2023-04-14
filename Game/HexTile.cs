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
    public class HexTile
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _position;
        private bool _keyboardSelected;
        private bool _mouseSelected;
        public HexTile(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public void KeyboardSelect()
        {
            _keyboardSelected = true;
        }

        public void KeyboardDeselect()
        {
            _keyboardSelected = false;
        }

        public void MouseSelect()
        {
            _mouseSelected = true;
        }

        public void MouseDeselect()
        {
            _mouseSelected = false;
        }

        public void Draw()
        {
            var selectHexColor = Color.White;
            if (_keyboardSelected) selectHexColor = Color.Red;
            if (_mouseSelected) selectHexColor = Color.Green;
            Globals.SpriteBatch.Draw(_texture, _position, selectHexColor);
        }
    }
}
