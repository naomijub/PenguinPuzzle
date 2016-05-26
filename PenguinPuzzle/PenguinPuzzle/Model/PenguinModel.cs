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
    public class PenguinModel : Model
    {
        enum Movement {standStill, up, down, right, left };
        Rectangle source;
        Texture2D seal;
        InputHandler handler;
        Movement move;
        bool win;

        public PenguinModel(int x, int y, Texture2D seal, InputHandler handler) : base(x, y){
            source = new Rectangle(511, 0, 73, 72);
            this.seal = seal;
            this.handler = handler;
            move = Movement.standStill;
            win = false;
        }

        public bool update(GameTime gameTime, int[] map) {
            handler.update();
            if (!win)
            {
                int i = ((X - 200) / 73) + (((Y - 100) / 72) * 10);
                if (move == Movement.standStill)
                {
                    checkMovement(map, i);
                }
                else {
                    mover(map, i);
                }
                win = checkWin(map, i);
            }
            return win;
        }

        public override void draw(GameTime gameTime, SpriteBatch sb)
        {
            Vector2 pos = new Vector2(X, Y);
            sb.Draw(seal, pos, source, Color.White);
        }

        private void checkMovement(int[] map, int i) {
            if (handler.KeyPressed(Keys.Up) ) {
                moveUp(map, i);
                move = Movement.up;
            }
            if (handler.KeyPressed(Keys.Down)) {
                moveDown(map, i);
                move = Movement.down;
            }
            if (handler.KeyPressed(Keys.Right) )
            {
                moveRight(map, i);
                move = Movement.right;
            }
            if (handler.KeyPressed(Keys.Left) )
            {
                moveLeft(map, i);
                move = Movement.left;
            }
        }

        public void mover(int[] map, int i) {
            switch (move) {
                case Movement.up: moveUp(map, i); break;
                case Movement.down: moveDown(map, i); break;
                case Movement.right: moveRight(map, i); break;
                case Movement.left: moveLeft(map, i); break;
            }
        }

        private void moveUp(int[] map, int i) {
            int y = transformToY(i);
            if (y == 0 || Y < 100)
            {
                Y = 27;
            }
            else {
                if (map[i - 10] != 9)
                {
                    move = Movement.standStill;
                    Console.WriteLine("Stop moving up");
                }
                else {
                    Y -= 72;
                }
            }
            
        }

        private void moveDown(int[] map, int i) {
            int y = transformToY(i);
            if (y == 9 || Y > 815)
            {
                Y = 825;
            }
            else {
                if (map[i + 10] != 9)
                {
                    move = Movement.standStill;
                }
                else{
                    Y += 72;
                }
            }
        }

        private void moveRight(int[] map, int i) {
            int x = transformToX(i);
            if (x == 9 || X > 929)
            {
                X = 930;
            }
            else {
                if (map[i + 1] != 9)
                {
                    move = Movement.standStill;
                }
                else {
                    X += 73;
                }
            }
        }

        private void moveLeft(int[] map, int i) {
            if (X < 200)
            {
                X = 127;
            }
            else {
                if (map[i - 1] != 9)
                {
                    move = Movement.standStill;
                }
                else {
                    X -= 73;
                }
            }
        }

        private int transformToX(int i) {
            return i % 10;
        }

        private int transformToY(int i) {
            return i / 10;
        }

        public bool checkWin(int[] map, int i) {
            if (i < 80)
            {
                return ((map[i + 10] == 6) ? true : false);
            }
            return false;
        }

        public bool checkEaten(int i) {
            int idx = (X - 200) / 73 + (10 * ((Y - 100) / 72));
            return (i == idx ? true : false);
        }

        public bool outOfBounds() {
            if (X < 200 || X >= 930 || Y < 100 || Y > 820) {
                return true;
            }
            return false;
        }
    }
}
