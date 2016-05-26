using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace PenguinPuzzle
{
    public class InputHandler
    {
        KeyboardState currentKeyboardState, previousKeyboardState;

        public InputHandler() {
            currentKeyboardState = Keyboard.GetState();
        }

        public void update() {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }
    }
}
