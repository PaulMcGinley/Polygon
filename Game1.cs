using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Polygon;

public class Game1 : Game {

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    PointShape pointShape;

    public Game1() {

        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = (int)(1920*1.5f);
        _graphics.PreferredBackBufferHeight = (int)(1080*1.5f);

    }

    protected override void Initialize() {

        pointShape = new PointShape(GraphicsDevice);

        // Create a square
        // pointShape.AddPoint(new Vector2(100, 100));
        // pointShape.AddPoint(new Vector2(200, 100));
        // pointShape.AddPoint(new Vector2(200, 200));
        // pointShape.AddPoint(new Vector2(100, 200));
        // pointShape.AddPoint(new Vector2(100, 100));

        // Create a hexagon
        // pointShape.AddPoint(new Vector2(150, 50));
        // pointShape.AddPoint(new Vector2(250, 50));
        // pointShape.AddPoint(new Vector2(300, 150));
        // pointShape.AddPoint(new Vector2(250, 250));
        // pointShape.AddPoint(new Vector2(150, 250));
        // pointShape.AddPoint(new Vector2(100, 150));
        // pointShape.AddPoint(new Vector2(150, 50));

        // Working
        // Create a triangle
        // pointShape.AddPoint(new Vector2(400, 100));
        // pointShape.AddPoint(new Vector2(500, 200));
        // pointShape.AddPoint(new Vector2(300, 200));
        // pointShape.AddPoint(new Vector2(400, 100));

        // Working
        // Create a star
        // pointShape.AddPoint(new Vector2(750, 50));
        // pointShape.AddPoint(new Vector2(800, 150));
        // pointShape.AddPoint(new Vector2(900, 150));
        // pointShape.AddPoint(new Vector2(825, 200));
        // pointShape.AddPoint(new Vector2(850, 300));
        // pointShape.AddPoint(new Vector2(750, 250));
        // pointShape.AddPoint(new Vector2(650, 300));
        // pointShape.AddPoint(new Vector2(675, 200));
        // pointShape.AddPoint(new Vector2(600, 150));
        // pointShape.AddPoint(new Vector2(700, 150));
        // pointShape.AddPoint(new Vector2(750, 50));
        
        // Working
        // Create a cross
        pointShape.AddPoint(new Vector2(100, 400));
        pointShape.AddPoint(new Vector2(200, 400));
        pointShape.AddPoint(new Vector2(200, 500));
        pointShape.AddPoint(new Vector2(300, 500)); 
        pointShape.AddPoint(new Vector2(300, 400));
        pointShape.AddPoint(new Vector2(400, 400));
        pointShape.AddPoint(new Vector2(400, 300));
        pointShape.AddPoint(new Vector2(300, 300));
        pointShape.AddPoint(new Vector2(300, 200));
        pointShape.AddPoint(new Vector2(200, 200));
        pointShape.AddPoint(new Vector2(200, 300));
        pointShape.AddPoint(new Vector2(100, 300));
        pointShape.AddPoint(new Vector2(100, 400));

        // Broken
        // Create a heart
        // pointShape.AddPoint(new Vector2(450, 400));
        // pointShape.AddPoint(new Vector2(550, 400));
        // pointShape.AddPoint(new Vector2(600, 450));
        // pointShape.AddPoint(new Vector2(600, 550));
        // pointShape.AddPoint(new Vector2(550, 600));
        // pointShape.AddPoint(new Vector2(450, 600));
        // pointShape.AddPoint(new Vector2(400, 550));
        // pointShape.AddPoint(new Vector2(400, 450));
        // pointShape.AddPoint(new Vector2(450, 400));

        // Broken
        // Create a moon
        // pointShape.AddPoint(new Vector2(700, 400));
        // pointShape.AddPoint(new Vector2(800, 400));
        // pointShape.AddPoint(new Vector2(800, 500));
        // pointShape.AddPoint(new Vector2(700, 500));
        // pointShape.AddPoint(new Vector2(700, 400));
        // pointShape.AddPoint(new Vector2(750, 450));
        // pointShape.AddPoint(new Vector2(700, 500));
        // pointShape.AddPoint(new Vector2(750, 450));
        // pointShape.AddPoint(new Vector2(800, 400));

        // Working
        // Create a diamond
        // pointShape.AddPoint(new Vector2(100, 600));
        // pointShape.AddPoint(new Vector2(200, 500));
        // pointShape.AddPoint(new Vector2(300, 600));
        // pointShape.AddPoint(new Vector2(200, 700));
        // pointShape.AddPoint(new Vector2(100, 600));

        // Working
        // Create a trapezoid
        // pointShape.AddPoint(new Vector2(400, 600));
        // pointShape.AddPoint(new Vector2(600, 600));
        // pointShape.AddPoint(new Vector2(700, 700));
        // pointShape.AddPoint(new Vector2(300, 700));
        // pointShape.AddPoint(new Vector2(400, 600));

        // Working
        // Create a parallelogram
        // pointShape.AddPoint(new Vector2(750, 600));
        // pointShape.AddPoint(new Vector2(850, 600));
        // pointShape.AddPoint(new Vector2(900, 700));
        // pointShape.AddPoint(new Vector2(800, 700));
        // pointShape.AddPoint(new Vector2(750, 600));

        // Broke
        // Create a pentagram
        // pointShape.AddPoint(new Vector2(100, 800));
        // pointShape.AddPoint(new Vector2(200, 800));
        // pointShape.AddPoint(new Vector2(250, 900));
        // pointShape.AddPoint(new Vector2(300, 800));
        // pointShape.AddPoint(new Vector2(400, 800));
        // pointShape.AddPoint(new Vector2(350, 700));
        // pointShape.AddPoint(new Vector2(400, 600));
        // pointShape.AddPoint(new Vector2(300, 600));
        // pointShape.AddPoint(new Vector2(250, 700));
        // pointShape.AddPoint(new Vector2(200, 600));
        // pointShape.AddPoint(new Vector2(100, 600));
        // pointShape.AddPoint(new Vector2(150, 700));
        // pointShape.AddPoint(new Vector2(100, 800));


        base.Initialize();
    }

    protected override void LoadContent() {

        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        pointShape.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        
        pointShape.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}