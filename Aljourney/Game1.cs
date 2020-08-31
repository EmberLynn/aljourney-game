using Aljourney.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System;

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
        private Texture2D xCharacter;
        private Texture2D yCharacter;
        private AnimateSprite animateRho;
        private AnimateSprite animateXCharacter;
        private AnimateSprite animateYCharacter;
        private float rhoTimer;

        //Dialog box
        private Texture2D textBubble;
        private SpriteFont retroFont;
        private string currentLine = "";
        private List<string> currentDialog = new List<string>();
        private int dialogCounter = 0;
        private DialogReader dialogReader;
        private MouseState oldMouseState;
        private int textCounter = 0;
        private string typedLine = "";
        private float textTimer;
        private int mouseClicks;
        //yes and no dialog options
        private Texture2D answerBox;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
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

            xCharacter = Content.Load<Texture2D>("XAtlas");
            animateXCharacter = new AnimateSprite(xCharacter, 1, 2);

            yCharacter = Content.Load<Texture2D>("YAtlas");
            animateYCharacter = new AnimateSprite(yCharacter, 1, 2);

            //load others
            textBubble = Content.Load<Texture2D>("TextBubble");
            answerBox = new Texture2D(GraphicsDevice, 1, 1);

            //load font
            retroFont = Content.Load<SpriteFont>("RetroFont");

        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            answerBox.SetData(new Color[] { Color.LemonChiffon });

            //singleton!!!
            if (dialogReader == null)
            {
                dialogReader = new DialogReader();
                currentDialog = dialogReader.getDialog
                    (@"C:\Users\ember\source\repos\Aljourney\Aljourney\Dialogs\IntroDialog.txt");
            }

            /*get mouse click to progress dialog box
            where dialogCounter is the current position in the array of dialogs
            currentDialog is the array of dialogs retrieved from the file
            currentLine is the line we are going to type to the screen*/
            if (dialogCounter == 0)
            {
                currentLine = currentDialog[dialogCounter];
                dialogCounter++;
            }
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed && !oldMouseState.Equals(mouseState))
            {
                mouseClicks++;
                if(mouseClicks % 2 == 1)
                {
                    textCounter = currentLine.Length;
                    typedLine = "";
                    foreach (char c in currentLine) 
                    {
                        typedLine += c;
                    }
                }
                else 
                {
                    if (dialogCounter < currentDialog.Count)
                    {
                        currentLine = currentDialog[dialogCounter];
                        textCounter = 0;
                        typedLine = "";
                    }
                    dialogCounter++;
                }
                
            }
            oldMouseState = mouseState;

            //time Rho's animation
            rhoTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (rhoTimer > 250)
            {
                animateRho.Update();
                animateXCharacter.Update();
                animateYCharacter.Update();
                rhoTimer = 0;
            }

            textTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (textTimer > 100)
            {
                if (textCounter < currentLine.Length)
                {
                    typedLine += currentLine[textCounter];
                }
                if (textCounter <= currentLine.Length)
                {
                    textCounter++;
                }
                if(textCounter == currentLine.Length)
                {
                    mouseClicks++;
                }
                textTimer = 0;
            }
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //draw background
            spriteBatch.Draw(forestBackground, new Rectangle(0, 0, 1300, 900), Color.White);

            //draw sprites
            animateRho.Draw(spriteBatch, new Vector2(160, 560));
            animateXCharacter.Draw(spriteBatch, new Vector2(600, 560));
            animateYCharacter.Draw(spriteBatch, new Vector2(700, 570));

            //draw others
            spriteBatch.Draw(answerBox, new Rectangle(300, 480, 80, 40), Color.White);
            spriteBatch.Draw(answerBox, new Rectangle(400, 480, 80, 40), Color.White);
            spriteBatch.Draw(textBubble, new Vector2(180, 250), Color.White);
            //add text to textBubble
            spriteBatch.DrawString(retroFont, typedLine, new Vector2(290, 410), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
