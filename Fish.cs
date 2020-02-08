using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SplashKitSDK;

namespace GoFish
{
    public abstract class Fish
    {
        public Bitmap _FishBitmap;

        public double X { get; set; }
        public double Y { get; set; }
        private int Scale
        {
            get { return _FishBitmap.CellCount; }
        }
        public Vector2D Velocity { get; set; }

        public int Speed { get; set; }

        private int Width
        {
            // get { return SplashKit.Rnd(50); }
            get { return 50; }
        }

        private int Height
        {
           //get { return SplashKit.Rnd(50); }
            get { return 50; }
        }

        public Circle CollisionCircle
        {
            get
            {

                return SplashKit.CircleAt(X + (Width / 2), Y + (Height / 2), 20);
            }
        }


        protected Fish(Window gameWindow, Player player)
        {
           

            if (SplashKit.Rnd() < 0.5)
            {

                X = SplashKit.Rnd(gameWindow.Width);

                if (SplashKit.Rnd() < 0.5)
                {
                    Y = -gameWindow.Height;

                }
                else
                {
                    Y = gameWindow.Height;
                }
            }
            else
            {
                Y = SplashKit.Rnd(gameWindow.Height);

                if (SplashKit.Rnd() < 0.5)
                {
                    X = -Width;

                }
                else
                {
                    X = gameWindow.Width;
                }

            }

            Swim(player);

        }

        private  void Swim(Player player)
        {
            int speed = this.Speed;


            Point2D fromPt = new Point2D()
            {
                X = X,
                Y = Y
            };


            Point2D toPt = new Point2D()
            {
                X = player.X,
                Y = player.Y
            };


            Vector2D direction;
            direction = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));


            Velocity = SplashKit.VectorMultiply(direction, speed);
            
        }

        //private void RunAway(Window window)
        //{
        //    const int SPEED = 10;

        //    //get a point for the fish
        //    Point2D fromPt = new Point2D()
        //    {
        //        X = X,
        //        Y = Y
        //    };

        //    //get a point for the player
        //    Point2D toPt = new Point2D()
        //    {
        //        X = 1000,
        //        Y = 500


        //    };

        //    //calculate the direction to head
        //    Vector2D direction;
        //    direction = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

        //    //set the speed and assign to the velocity
        //    Velocity = SplashKit.VectorMultiply(direction, SPEED);
        //}

        private void RunAway(Window window)
        {
            int speed = this.Speed;

            Point2D fromPt = new Point2D()
            {
                X = X,
                Y = Y
            };

            Point2D toPt = new Point2D();

            if (X < 800)
            {
                toPt = new Point2D()
                {
                  X = SplashKit.Rnd(window.Width),
                  Y = -window.Height


            };

        }
            else if (X > 800)
            {
                toPt = new Point2D()
        {
            X = window.Width,
                    Y = SplashKit.Rnd(window.Height)



            };
    }
    //get a point for the player


    //calculate the direction to head
    Vector2D direction;
            direction = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

            //set the speed and assign to the velocity
            Velocity = SplashKit.VectorMultiply(direction, speed);
        }

        public void Update(Player player)
        {
            Swim(player);

            X += Velocity.X;
            Y += Velocity.Y;

            
        }

        public void Update(Window window)
        {
            RunAway(window);

            X += Velocity.X;
            Y += Velocity.Y;
        }

        public void Update()
        {
            
            X += Velocity.X;
            Y += Velocity.Y;
        }

        public abstract void Draw();


        public bool IsOffScreen(Window screen)
        {
            if (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height)
            {
                return true;
            }
            return false;
        }
    }

    public class Guppy : Fish
    {
        

        public Guppy(Window gameWindow, Player player) : base(gameWindow, player)
        {
            Random rnd = new Random();

            Speed = rnd.Next(8, 10);
            _FishBitmap = new Bitmap("Guppy", "./Resources/images/Small/Guppy Large Normal right.png");

        }


        public override void Draw()
        {
            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("Guppyleft", "./Resources/images/Small/Guppy Large Normal left.png");
            }
            else
            {
                _FishBitmap = new Bitmap("Guppyright", "./Resources/images/Small/Guppy Large Normal right.png");
            }
        }
    }

    public class Snapper : Fish
    {

        public Snapper(Window gameWindow, Player player) : base(gameWindow, player)
        {
            Random rnd = new Random();

            Speed = rnd.Next(10, 12);

            _FishBitmap = new Bitmap("Snapper", "./Resources/images/Small/Guppy Small Normal right.png");

        }


        public override void Draw()
        {
            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("Snapperleft", "./Resources/images/Small/Guppy Small Normal left.png");
            }
            else
            {
                _FishBitmap = new Bitmap("Snapperright", "./Resources/images/Small/Guppy Small Normal right.png");
            }

        }
    }

    public class Predetor : Fish
    {

        public Predetor(Window gameWindow, Player player) : base(gameWindow, player)
        {

            Random rnd = new Random();

            Speed = rnd.Next(7, 9);

            _FishBitmap = new Bitmap("Predetor", "./Resources/images/Small/Predator 1 right.png");

        }


        public override void Draw()
        {

            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("Predetorleft", "./Resources/images/Small/Predator 1 left.png");
            }
            else
            {
                _FishBitmap = new Bitmap("Predetorright", "./Resources/images/Small/Predator 1 right.png");
            }

        }
    }


    public class BigPredetor : Fish
    {

        public BigPredetor(Window gameWindow, Player player) : base(gameWindow, player)
        {

            Random rnd = new Random();

            Speed = rnd.Next(5, 7);

            _FishBitmap = new Bitmap("BigPredetor", "./Resources/images/Large/Predator 1 right.png");

        }


        public override void Draw()
        {

            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("BigPredetorLeft", "./Resources/images/Large/Predator 1 Right.png");
            }
            else
            {
                _FishBitmap = new Bitmap("BigPredetorRight", "./Resources/images/Large/Predator 1 Left.png");
            }

        }
    }

    public class Jellyfish : Fish
    {

        public Jellyfish(Window gameWindow, Player player) : base(gameWindow, player)
        {

            Random rnd = new Random();

            Speed = rnd.Next(3, 5);

            _FishBitmap = new Bitmap("Jellyfish", "./Resources/images/Large/Jellyfish2.png");

        }


        public override void Draw()
        {

            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("Jellyfishleft", "./Resources/images/Large/Jellyfish6.png");
            }
            else
            {
                _FishBitmap = new Bitmap("Jellyfishright", "./Resources/images/Large/Jellyfish3.png");
            }

        }
    }

    public class SmallJellyfish : Fish
    {

        public SmallJellyfish(Window gameWindow, Player player) : base(gameWindow, player)
        {

            Random rnd = new Random();

            Speed = rnd.Next(4, 6);

            _FishBitmap = new Bitmap("smallJellyfish", "./Resources/images/Medium/Jellyfish2.png");

        }


        public override void Draw()
        {

            SplashKit.DrawBitmap(_FishBitmap, X, Y);

            if (Velocity.Y > 0)
            {
                _FishBitmap = new Bitmap("smallJellyfishleft", "./Resources/images/Medium/Jellyfish6.png");
            }
            else
            {
                _FishBitmap = new Bitmap("smallJellyfishright", "./Resources/images/Medium/Jellyfish3.png");
            }

        }
    }

}
