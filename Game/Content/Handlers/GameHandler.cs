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
    public class GameHandler
    {
        private readonly HexMap _map = new();
        public void Update()
        {
            ControlHandler.Update();
            _map.Update();
        }

        public void Draw()
        {
            _map.Draw();
        }
    }
}
