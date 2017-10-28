using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Ping_Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle p1, p2, ball;
        SpriteFont font;
        int paddleH, velX, velY, score1 ,score2;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            paddleH = 135;
            ball.X = Window.ClientBounds.Width / 2;
            ball.Y = Window.ClientBounds.Height / 2;
            velX = 6;
            velY = -6;
            p1 = new Rectangle(30, Window.ClientBounds.Height / 2 - paddleH / 2, 10, paddleH);
            p2 = new Rectangle(Window.ClientBounds.Width - 30, Window.ClientBounds.Height / 2 - paddleH / 2, 10, paddleH);
            ball = new Rectangle(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2, 10, 10);
            score1 = 0;
            score2 = 0;
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
            font = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
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

            if (Keyboard.GetState().IsKeyDown(Keys.W) && p1.Y >= 0) p1.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && p1.Y <= Window.ClientBounds.Height - p1.Height) p1.Y += 5;

            if (Keyboard.GetState().IsKeyDown(Keys.I) && p2.Y >= 0) p2.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.K) && p2.Y <= Window.ClientBounds.Height - p2.Height) p2.Y += 5;

            if (p1.Intersects(ball) || p2.Intersects(ball)) velX = -velX;
            if (ball.Y == Window.ClientBounds.Height || ball.Y == 0) velY = -velY;

            if (ball.X >= Window.ClientBounds.Width)
            {
                score1++;
                Console.WriteLine("Red scores");
                ball.X = Window.ClientBounds.Width / 2;
                ball.Y = Window.ClientBounds.Height / 2;
            }

            if (ball.X <= 0)
            {
                score2++;
                Console.WriteLine("Blue scores");
                ball.X = Window.ClientBounds.Width / 2;
                ball.Y = Window.ClientBounds.Height / 2;
            }
            ball.X += velX;
            ball.Y += velY;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            drawRecangle(Color.Red, p1);
            drawRecangle(Color.Blue, p2);
            drawRecangle(Color.Magenta, ball);
            spriteBatch.DrawString(font, score1.ToString(), new Vector2(100,20),Color.Red);
            spriteBatch.DrawString(font, score2.ToString(), new Vector2(p2.X - 70, 20), Color.Blue);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawRecangle(Color color, Rectangle rectangle)
        {
            var rectTexture = new Texture2D(GraphicsDevice, rectangle.Width, rectangle.Height);
            Color[] data = new Color[rectangle.Width * rectangle.Height];
            for (int x = 0; x < data.Length; ++x) data[x] = color;
            rectTexture.SetData<Color>(data);
            spriteBatch.Draw(rectTexture, rectangle, color);
        }
    }
}
