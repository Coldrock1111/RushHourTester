using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RushHourTester;

public class Game1 : Game
{

    private string solution = "15-103+600+112-614-126-609+607+104+601+105+602+129-128-110+608+120-614-612+100+618+106+612+614-102+605-104-123-618+620-108+610-109-108-120-621-628-622-618+130-619+131-620+108+606+121+109+607+126+114+612+124-618-612-627+115+613+125-619-633-132-120+621+623-122-121-120-132-633-604+601+113-615-114-127-605+602+129-111+630+135-126-628-127-126-114+615+610+632-134-116+617+6";

    

    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private Texture2D grid;
    private KeyboardState keyboardState;

    private PeiceObj[] peices = new PeiceObj[13]
    {
        new PeiceObj(new Vector2(0, 0), 4),
        new PeiceObj(new Vector2(3, 0), 1),
        new PeiceObj(new Vector2(4, 0), 3),
        new PeiceObj(new Vector2(5, 0), 3),
        new PeiceObj(new Vector2(0, 1), 1),
        new PeiceObj(new Vector2(1, 1), 2),
        new PeiceObj(new Vector2(2, 2), 2),
        new PeiceObj(new Vector2(0, 3), 2),
        new PeiceObj(new Vector2(2, 3), 1),
        new PeiceObj(new Vector2(1, 4), 1),
        new PeiceObj(new Vector2(4, 4), 2),
        new PeiceObj(new Vector2(2, 5), 2),
        new PeiceObj(new Vector2(4, 5), 2)
    };

    private int moveIndex = 0;
    private int currentGridIndex;
    private Vector2 currentGridPos;
    private Vector2 movementVector;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        graphics.PreferredBackBufferWidth = 900;
        graphics.PreferredBackBufferHeight = 900;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        grid = Content.Load<Texture2D>("grid");
        for (int i = 0; i < peices.Length; i++) 
        {
            switch (peices[i].GetLength())
            {
                case 1:
                    peices[i].LoadContent(Content.Load<Texture2D>("2tall"));
                    break;
                case 2:
                    peices[i].LoadContent(Content.Load<Texture2D>("2long"));
                    break;
                case 3:
                    peices[i].LoadContent(Content.Load<Texture2D>("3tall"));
                    break;
                case 4:
                    peices[i].LoadContent(Content.Load<Texture2D>("3long"));
                    break;
            }
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Right)) 
        {
            currentGridIndex = ((int)solution[(moveIndex * 4)]-48) * 10 + ((int)solution[(moveIndex * 4)+1] - 48);
            currentGridPos = new Vector2(currentGridIndex % 6, currentGridIndex / 6);
            if (solution[(moveIndex * 4)+2] == '+')
            {
                if (solution[(moveIndex * 4) + 3] == '1') 
                {
                    movementVector = new Vector2(1, 0);
                }
                else if (solution[(moveIndex * 4) + 3] == '6')
                {
                    movementVector = new Vector2(0, 1);
                }
            }
            else if (solution[(moveIndex * 4) + 2] == '-')
            {
                if (solution[(moveIndex * 4) + 3] == '1')
                {
                    movementVector = new Vector2(-1, 0);
                }
                else if (solution[(moveIndex * 4) + 3] == '6')
                {
                    movementVector = new Vector2(0, -1);
                }
            }
            for(int i = 0; i < peices.Length; i++) 
            {
                for (int j = 0; j <= 2; j++) 
                {
                    if (peices[i].GetOccupiedSpace()[j] == currentGridPos) 
                    {
                        peices[i].ChangePosition(movementVector);
                        break;
                    }
                }
            }
            moveIndex++;
        }
        keyboardState = Keyboard.GetState();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        spriteBatch.Draw(grid, new Vector2(0, 0),Color.White);
        for (int i = 0; i < peices.Length; i++)
        {
            peices[i].draw(spriteBatch);
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
