using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace PenguinPuzzle.Model
{
    public class SharkModel : Model
    {
        public bool attackWhenTrue{ get; set; }
        public Vector2 pos { get; set; }
        private Random rg;
        private Texture2D shark;
        
        public SharkModel(int x, int y, Texture2D shark) : base(x, y) {
            attackWhenTrue = false;
            this.shark = shark;
            rg = new Random();
        }

        public override void draw(GameTime gameTime, SpriteBatch sb)
        {                                          
            if (attackWhenTrue) {
                sb.Draw(shark, pos, Color.White);
            }
        }

        public void update(GameTime gameTime, SoundEffect sharkedSnd)
        {
            if (gameTime.TotalGameTime.TotalSeconds >= 15 && !attackWhenTrue) {
                attackWhenTrue = true;
                X = rg.Next(0, 10);
                Y = rg.Next(0, 10);
                pos = new Vector2(200 + (X * 73), 100 + (Y * 72));
                sharkedSnd.Play(1.0f, 0.0f, 0.0f);
                Console.WriteLine("Shark = true");
                Console.WriteLine("X: " + X + " Y: " + Y);
            }
        }

        public int returnIndexValue() {
            return (X - 200) / 73 + (10 * ((Y - 100) / 72));
        }
    }
}
