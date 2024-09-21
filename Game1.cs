﻿using MasterGame.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MasterGame
{
    public class Game1 : Game
    {
        public static Game1 self { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        public int state { get; set; }
        public int gameWidth { get; set; }
        public int gameHeight { get; set; }
        public int windowWidth { get; set; }
        public int windowHeight { get; set; }
        public bool IsFullscreen { get; set; }

        // TODO: Loosen coupling. GraphicsDeviceManager should probably not be public, but ToggleFullscreenCommand still needs to be able to work.
        public GraphicsDeviceManager graphics;
        private SpriteFont font;
        private MouseController mouse;
        private KeyboardController keyboard;

        // Test sprites
        public Sprite TestSprite1 { get; set; }
        public Sprite TestSprite2 { get; set; }

        public IPlayer kirby;
        // get kirby 
        
        public Game1()
        {
            self = this;
            graphics = new GraphicsDeviceManager(this);
            mouse = new MouseController();
            keyboard = new KeyboardController();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            state = 1;

            gameWidth = 240;
            gameHeight = 160;
            windowWidth = 720;
            windowHeight = 480;
            IsFullscreen = false;
            kirby = new Player(new Vector2(30, gameHeight * 4/5));
        }

        // will later be changed to read in mouse control input
        public void SetMouseControls(MouseController mouse)
        {
            mouse.leftClickIndex = 0;
            mouse.rightClickIndex = 1;
            mouse.quadrantIndex = 2;

            mouse.leftClickPressed = 1;
            mouse.rightClickPressed = 0;
            mouse.quadrant = 1;
        }

        // will later be changed to read in keyboard control input
        public void SetKeyboardControls(KeyboardController keyboard)
        {
            keyboard.RegisterCommand(Keys.Q, new QuitCommand(this));

            //keyboard.RegisterCommand(Keys.F, toggleFullscreen);

            keyboard.RegisterCommand(Keys.Right, new KirbyMoveRightCommand(kirby));
            keyboard.RegisterCommand(Keys.Left, new KirbyMoveLeftCommand(kirby));
            keyboard.RegisterCommand(Keys.T, new PreviousBlockCommand());
            keyboard.RegisterCommand(Keys.Y, new NextBlockCommand());

        }
        protected override void Initialize()
        {
            // true = exclusive fullscreen, false = borderless fullscreen
            graphics.HardwareModeSwitch = true;
            graphics.IsFullScreen = IsFullscreen;
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            base.Initialize();
            SetMouseControls(mouse);
            SetKeyboardControls(keyboard);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("DefaultFont");

            // Load all sprite factory textures and sprites.
            SpriteFactory.Instance.LoadAllTextures(Content);
            SpriteFactory.Instance.LoadAllSpriteAnimations();
            // Create a test sprite (TEMPORARY)
            kirby.PlayerSprite = SpriteFactory.Instance.createSprite("kirby_normal_standing_right");

            TestSprite1 = SpriteFactory.Instance.createSprite("kirby_normal_walking_right");
            TestSprite2 = SpriteFactory.Instance.createSprite("tile_waterfall");

            // Load textures for blocks and load the names of the sprites into a list
            List<Sprite> blockList = new List<Sprite>();
            blockList = new List<Sprite>
            {
                SpriteFactory.Instance.createSprite("tile_dirt"),
                SpriteFactory.Instance.createSprite("tile_grass"),
                SpriteFactory.Instance.createSprite("tile_platform"),
                SpriteFactory.Instance.createSprite("tile_rock"),
                SpriteFactory.Instance.createSprite("tile_rocksurface"),
                SpriteFactory.Instance.createSprite("tile_slope_gentle1_left"),
                SpriteFactory.Instance.createSprite("tile_slope_gentle1_right"),
                SpriteFactory.Instance.createSprite("tile_slope_gentle2_left"),
                SpriteFactory.Instance.createSprite("tile_slope_gentle2_right"),
                SpriteFactory.Instance.createSprite("tile_slope_steep_left"),
                SpriteFactory.Instance.createSprite("tile_slope_steep_right"),
                SpriteFactory.Instance.createSprite("tile_stoneblock"),
                SpriteFactory.Instance.createSprite("tile_waterfall"),
        };

            BlockList.Instance.setBlockList(blockList);

            //kirby.UpdateTexture();
            //toggleFullscreen = new ToggleFullscreenCommand();

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            mouse.Update();
            keyboard.Update();

            TestSprite1.Update();
            TestSprite2.Update();
            kirby.Update(); 
            //TestSprite.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //ICommand[] commands = { quit, kirbyMoveRight };

            base.Draw(gameTime);

            // draws the corresponding sprite given current game state
            //commands[state].Execute();

            // Start spriteBatch
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            // Debug text: keeping track of resolutions
            string text;
            text = "GraphicsAdapter.DefaultAdapter.CurrentDisplayMode: (" + GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width + ", " + GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height + ")";
            spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.Black);
            text = "graphics.PreferredBackBuffer______: (" + graphics.PreferredBackBufferWidth + ", " + graphics.PreferredBackBufferHeight + ")";
            spriteBatch.DrawString(font, text, new Vector2(10, 30), Color.Black);
            text = "GraphicsDevice.PresentationParameters: (" + GraphicsDevice.PresentationParameters.BackBufferWidth + ", " + GraphicsDevice.PresentationParameters.BackBufferHeight + ")";
            spriteBatch.DrawString(font, text, new Vector2(10, 50), Color.Black);
            text = "GraphicsDevice.Viewport: (" + GraphicsDevice.Viewport.Width + ", " + GraphicsDevice.Viewport.Height + ")";
            spriteBatch.DrawString(font, text, new Vector2(10, 70), Color.Black);

            float scale = windowHeight / gameHeight;
            // Draw test sprite
            TestSprite1.Draw(new Vector2(100, 100));
            //TestSprite2.Draw(new Vector2(130, 92));

            BlockList.Instance.Draw(new Vector2(200, 100));


            //TestSprite2.Draw(new Vector2((int)(Mouse.GetState().X/scale), (int)(Mouse.GetState().Y/scale)));

            kirby.Draw();
            // End spriteBatch
            spriteBatch.End();

        }
    }
}
