using SplashKitSDK;

namespace GoFish
{
    public class Player
    {
        public Bitmap _PlayerBitmap;

        public double X { get; set; }
        public double Y { get; set; }


        public bool Quit { get;  set; }

        private int Width
        {
            get
            {
                return _PlayerBitmap.Width;
            }
        }

        private int Height
        {
            get
            {
                return _PlayerBitmap.Height;
            }

        }

        private int Scale
        {
            get { return _PlayerBitmap.CellCount; }
        }
        


        public Player(Window gameWindow) 
        {
            _PlayerBitmap = new Bitmap("playerRight", "./Resources/images/Small/Guppy Small Sick right.png");


            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;


        }

        public void Draw() // Draws the player to the screen
        {
            var x = X;
            var y = Y;

            //DrawingOptions scale = SplashKit.OptionScaleBmp(1, 1);
            SplashKit.DrawBitmap(_PlayerBitmap,x,y);



        }

        public int CurrentBitmapSize()
        {
            return _PlayerBitmap.CellHeight;
        }


        public void HandleInput()
        {


            const int SPEED = 20;

            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Y += SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Y -= SPEED;
            }

            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                switch (CurrentBitmapSize())
                {
                    case 30:
                      _PlayerBitmap = new Bitmap("playerRight", "./Resources/images/Small/Guppy Small Sick right.png");
                        break;
                    case 40:
                        _PlayerBitmap = new Bitmap("playerMediumRight", "./Resources/images/Small/Guppy Medium Sick right.png");
                        break;
                    case 54:
                        _PlayerBitmap = new Bitmap("playerLargeRight", "./Resources/images/Small/Guppy Large Sick right.png");
                        break;
                    case 73:
                        _PlayerBitmap = new Bitmap("playerXLRight", "./Resources/images/Large/Guppy Small Sick right.png");
                        break;
                    case 98:
                        _PlayerBitmap = new Bitmap("playerMLRight", "./Resources/images/Large/Guppy Medium Sick right.png");
                        break;
                    case 132:
                        _PlayerBitmap = new Bitmap("playerXXLargeRight", "./Resources/images/Large/Guppy large Sick right.png");
                        break;
                    case 162:
                        _PlayerBitmap = new Bitmap("playerPredatorRight", "./Resources/images/Large/Predator Sick right.png");
                        break;
                    default:
                        _PlayerBitmap = new Bitmap("playerRight", "./Resources/images/Small/Guppy Small Sick right.png");
                        break;
                        

                }




                X += SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                switch (CurrentBitmapSize())
                {
                    case 30:
                        _PlayerBitmap = new Bitmap("playerLeft", "./Resources/images/Small/Guppy Small Sick left.png");
                        break;
                    case 40:
                        _PlayerBitmap = new Bitmap("playerMediumLeft", "./Resources/images/Small/Guppy Medium Sick left.png");
                        break;
                    case 54:
                        _PlayerBitmap = new Bitmap("playerLargeLeft", "./Resources/images/Small/Guppy Large Sick left.png");
                        break;
                    case 73:
                        _PlayerBitmap = new Bitmap("playerXLLeft", "./Resources/images/Large/Guppy small Sick left.png");
                        break;
                    case 98:
                        _PlayerBitmap = new Bitmap("playerMLLeft", "./Resources/images/Large/Guppy Medium Sick left.png");
                        break;
                    case 132:
                        _PlayerBitmap = new Bitmap("playerXXLargeLeft", "./Resources/images/Large/Guppy large Sick left.png");
                        break;
                    case 162:
                        _PlayerBitmap = new Bitmap("playerPredatorleft", "./Resources/images/Large/Predator Sick left.png");
                        break;
                    default:
                        _PlayerBitmap = new Bitmap("playerLeft", "./Resources/images/Small/Guppy Small Sick left.png");
                        break;
                }
                X -= SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.SpaceKey))
            {
               
            }
            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {

                var font = SplashKit.LoadFont("Hand Scribble Sketch Times",
                    "./resources/fonts/Hand Scribble Sketch Times.otf");
    
                SplashKit.DisplayDialog("gameover", "Are you sure you want to Quit", font, 25);

                Quit = true;
            }

        }

        public void StayOnWindow(Window window)
        {
            const int GAP = 5;

            if (X < GAP)
            {
                X = GAP;
            }
            if (Y < GAP)
            {
                Y = GAP;
            }

            if (X > window.Width - _PlayerBitmap.Width - GAP)
            {
                X = window.Width - GAP - _PlayerBitmap.Width;
            }

            if (Y > window.Height - _PlayerBitmap.Height - GAP)
            {
                Y = window.Height - GAP - _PlayerBitmap.Height;
            }

        }

        public int CheckScale()
        {
            var bitmap = _PlayerBitmap;

            return bitmap.CellHeight;

        }

        public bool CollideWith(Fish fish)
        {

            return _PlayerBitmap.CircleCollision(X, Y, fish.CollisionCircle);
        }


    }
}