using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PenguinPuzzle.Map
{
    public class MapGenerator
    {

        public void drawMap(SpriteBatch sb, Texture2D blocks) {
            for (int counter = 0; counter < 100; counter++)
            {
                int variation = (counter + 1) % 2;
                Rectangle source = new Rectangle((73 * variation), 0, 73, 72);
                int x = counter % 10;
                int y = counter / 10;
                Vector2 pos = new Vector2(200 + (x * 73), 100 + (y * 72));
                sb.Draw(blocks, pos, source, Color.White);
            }
        }

        public void drawPenguin(SpriteBatch sb, Texture2D penguins, int[] penguinPositioning)
        {
            for (int i = 0; i < penguinPositioning.Length; i++)
            {
                if (penguinPositioning[i] != 9)
                {
                    Rectangle source = new Rectangle(73 * penguinPositioning[i], 0, 73, 72);
                    int x = i % 10;
                    int y = i / 10;
                    Vector2 pos = new Vector2(200 + (x * 73), 100 + (y * 72));
                    sb.Draw(penguins, pos, source, Color.White);
                }
            }
        }
    }
}
