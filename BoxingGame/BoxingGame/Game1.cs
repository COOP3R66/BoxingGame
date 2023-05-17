using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxingGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        string page;

        SpriteFont font;

        //red boxer - boxer on the left
        Texture2D RboxerTexture;
        Texture2D RboxerGuardTexture;
        Texture2D RboxerPunchTexture;
        Vector2 RboxerPosition;
        string RboxerState;

        //blue boxer - boxer on the right
        Texture2D BboxerTexture;
        Texture2D BboxerGuardTexture;
        Texture2D BboxerPunchTexture;
        Vector2 BboxerPosition;
        string BboxerState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            page = "start";
            
            BboxerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight /2  - 100);
            RboxerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100 , _graphics.PreferredBackBufferHeight/ 2 - 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("font");
            RboxerGuardTexture = Content.Load<Texture2D>("redguard");
            BboxerGuardTexture = Content.Load<Texture2D>("blueguard");
            RboxerPunchTexture = Content.Load<Texture2D>("redpunch");
            BboxerPunchTexture = Content.Load<Texture2D>("bluepunch");

            BboxerTexture = BboxerGuardTexture;
            RboxerTexture = RboxerGuardTexture;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();
            if (page == "start")
            {
                if (kstate.IsKeyDown(Keys.Enter)){
                    page = "game";
                }
            }
            else if (page == "game")
            {
                if (kstate.IsKeyDown(Keys.D))
                {
                    RboxerPosition.X += 5;
                }
                else if (kstate.IsKeyDown(Keys.A))
                {
                    RboxerPosition.X -= 5;
                }
                if (kstate.IsKeyDown(Keys.W))
                {
                    RboxerTexture = RboxerPunchTexture;
                }
                else
                {
                    RboxerTexture = RboxerGuardTexture;
                }

                if (kstate.IsKeyDown(Keys.Right))
                {
                    BboxerPosition.X += 5;
                }
                else if (kstate.IsKeyDown(Keys.Left))
                {
                    BboxerPosition.X -= 5;
                }
                if (kstate.IsKeyDown(Keys.Up))
                {
                    BboxerTexture = BboxerPunchTexture;
                }
                else
                {
                    BboxerTexture = BboxerGuardTexture;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (page == "start") {
                _spriteBatch.DrawString(font, "Press Enter To Start", new Vector2(100, _graphics.PreferredBackBufferHeight / 2), Color.White);
            }
            else if (page == "game")
            {
                _spriteBatch.Draw(RboxerTexture, RboxerPosition, Color.White);
                _spriteBatch.Draw(BboxerTexture, BboxerPosition, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}