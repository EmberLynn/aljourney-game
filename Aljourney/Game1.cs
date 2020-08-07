using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Aljourney
{
    /// <summary>
    /// 
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Backgrounds
        private Texture2D forestBackground;

        //Characters
        private Texture2D rho;
        private AnimateSprite animateRho;
        private float rhoTimer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //set the background size
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            //load the backgrounds
            forestBackground = Content.Load<Texture2D>("backgroundforest");

            //load the characters
            rho = Content.Load<Texture2D>("RhoAtlas");
            animateRho = new AnimateSprite(rho, 1, 3);

        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            rhoTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (rhoTimer > 200)
            {
                animateRho.Update();
                rhoTimer = 0;
            }

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(forestBackground, new Rectangle(0, 0, 1300, 900), Color.White);

            animateRho.Draw(spriteBatch, new Vector2(150, 530));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
