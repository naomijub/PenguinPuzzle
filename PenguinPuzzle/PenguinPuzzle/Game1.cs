using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using PenguinPuzzle.Model;
using PenguinPuzzle.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenguinPuzzle
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, penguins, shark, blocks;
        SoundEffect sharkedSnd, bgMusic, lostSnd;
        SpriteFont font;
        PenguinModel seal;
        SharkModel sharkModel;
        MapGenerator map;
        MapReader mapReader;
        InputHandler handler;
        int level = 0;
        double time;
        bool lost = false, win;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            penguins = Content.Load<Texture2D>("penguins");
            shark = Content.Load<Texture2D>("sharked");
            blocks = Content.Load<Texture2D>("blocks");
            sharkedSnd = Content.Load<SoundEffect>("snd_sharked");
            bgMusic = Content.Load<SoundEffect>("snd_music");
            lostSnd = Content.Load<SoundEffect>("snd_lost");
            font = Content.Load<SpriteFont>("ScoreFont");
            map = new MapGenerator();
            mapReader = new MapReader();
            handler = new InputHandler();

            win = false;

            seal = new PenguinModel(638, 676, penguins, handler);
            sharkModel = new SharkModel(0, 0, shark);

            bgMusic.Play(0.7f, 0.0f, 0.0f);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            checkTime(gameTime);
            sharkModel.update(gameTime, sharkedSnd);
            sealUpdater(gameTime);
            lost = seal.outOfBounds();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            map.drawMap(spriteBatch, blocks);
            map.drawPenguin(spriteBatch, penguins, mapReader.returnCurrentLevel(level));
            drawTime(spriteBatch, gameTime);
            seal.draw(gameTime ,spriteBatch);
            sharkModel.draw(gameTime ,spriteBatch);
            winDraw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void drawTime(SpriteBatch sb, GameTime gameTime) {

            sb.DrawString(font, ("Time Left: " + time), new Vector2(200,50), Color.White);
        }

        public void checkTime(GameTime gameTime) {

            if (gameTime.TotalGameTime.TotalSeconds > 14.99999)
            {
                time = 0;
                lost = true;
            }
            else if (!win && !lost)
            {
                time = 15 - gameTime.TotalGameTime.TotalSeconds;
            }
            
        }

        public void sealUpdater(GameTime gameTime) {
            if (!lost) {
               win = seal.update(gameTime, mapReader.getCurrentLevel());
               lost = seal.checkEaten(sharkModel.returnIndexValue()); 
            }
        }

        public void winDraw(SpriteBatch sb) {
            if (win) {
                 sb.DrawString(font, "You Win!!!", new Vector2(400, 300), Color.Red);
                Console.WriteLine("You win");
            }
            if (lost)
            {
                sb.DrawString(font, "You Loose!!!", new Vector2(400, 300), Color.Red);
                Console.WriteLine("You win");
            }
        }
    }
}
