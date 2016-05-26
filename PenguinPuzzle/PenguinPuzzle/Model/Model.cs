using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PenguinPuzzle.Model
{
    public abstract class Model
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Model(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public abstract void draw(GameTime gameTime, SpriteBatch sb);
    }
}
