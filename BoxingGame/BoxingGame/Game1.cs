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
        Texture2D RboxerDuckTexture;
        Vector2 RboxerPosition;
        string RboxerState;
        int RboxerEnergy;
        int RboxerDamadge;

        //blue boxer - boxer on the right
        Texture2D BboxerTexture;
        Texture2D BboxerGuardTexture;
        Texture2D BboxerPunchTexture;
        Texture2D BboxerDuckTexture;
        Vector2 BboxerPosition;
        string BboxerState;
        int BboxerEnergy;
        int BboxerDamadge;

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

            RboxerDamadge = 100;
            RboxerEnergy = 10000;
            BboxerDamadge = 100;
            BboxerEnergy = 10000;
            BboxerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 - 100);
            RboxerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2 - 100);
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
            BboxerDuckTexture = Content.Load<Texture2D>("blueduck");
            RboxerDuckTexture = Content.Load<Texture2D>("redduck");

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
                if (kstate.IsKeyDown(Keys.Enter)) {
                    page = "game";
                }
            }
            else if (page == "game")
            {
                if (RboxerPosition.X + RboxerTexture.Width <= BboxerPosition.X + 30)
                {
                    if (kstate.IsKeyDown(Keys.D))
                    {
                        RboxerPosition.X += 5;
                        RboxerEnergy -= 2; 
                    }
                    if (kstate.IsKeyDown(Keys.Left))
                    {
                        BboxerPosition.X -= 5;
                        BboxerEnergy -= 2;
                    }
                }
                if (RboxerPosition.X > 0)
                {
                    if (kstate.IsKeyDown(Keys.A))
                    {
                        RboxerPosition.X -= 5;
                        RboxerEnergy--;
                    }

                }
                if (BboxerPosition.X < _graphics.PreferredBackBufferWidth - BboxerTexture.Width)
                {
                    if (kstate.IsKeyDown(Keys.Right))
                    {
                        BboxerPosition.X += 5;
                        BboxerEnergy--;
                        
                    }

                }

                if (kstate.IsKeyDown(Keys.S))
                {
                    RboxerTexture = RboxerDuckTexture;
                    RboxerState = "ducking";
                }
                else if (kstate.IsKeyDown(Keys.W))
                {
                    RboxerTexture = RboxerPunchTexture;
                    RboxerEnergy -= 10;
                    RboxerState = "punching";
                    if (RboxerPosition.X + RboxerTexture.Width >= BboxerPosition.X + 25)
                    {


                        if (BboxerState == "guard")
                        {
                            BboxerDamadge++;
                        }
                        else if (BboxerState == "punching")
                        {
                            BboxerDamadge += 2;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    RboxerTexture = RboxerGuardTexture;
                    RboxerState = "guard";
                }

                if (kstate.IsKeyDown(Keys.Down))
                {
                    BboxerTexture = BboxerDuckTexture;
                    BboxerState = "ducking";
                }
                else if (kstate.IsKeyDown(Keys.Up))
                {
                    if (RboxerPosition.X + RboxerTexture.Width >= BboxerPosition.X + 25)
                    {
                        BboxerTexture = BboxerPunchTexture;
                        BboxerEnergy -= 10;
                        BboxerState = "punching";
                        if (RboxerState == "guard")
                        {
                            RboxerDamadge++;
                        }
                        else if (BboxerState == "punching")
                        {
                            RboxerDamadge += 2;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    BboxerTexture = BboxerGuardTexture;
                    BboxerState = "guard";
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
                _spriteBatch.DrawString(font, "Energy: " + ((int)RboxerEnergy/100), new Vector2(100, 50), Color.White);
                _spriteBatch.DrawString(font, "Damadge: " + RboxerDamadge, new Vector2(100, 70), Color.White);
                _spriteBatch.DrawString(font, "Energy: " + ((int)BboxerEnergy/100), new Vector2(500, 50), Color.White);
                _spriteBatch.DrawString(font, "Damadge: " + BboxerDamadge, new Vector2(500, 70), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}