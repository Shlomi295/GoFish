using System;
using System.Collections.Generic;
using SplashKitSDK;


namespace GoFish
{
    class Game
    {

        private Player _Player;
        private Window _GameWindow;
        private Bitmap _seeWeedBitmap;
        private Bitmap _backgroundBitmap;

        private List<Fish> _fishes = new List<Fish>();

        private List<Fish> _fishEaten = new List<Fish>();
        private List<Fish> _score = new List<Fish>();

        public bool Quit
        {
            get { return _Player.Quit; }
        }

        public Game(Window window, Player player)
        {
            _GameWindow = window;
            _Player = player;


            LoadResources();

          
        }

        private void LoadResources()
        {
            _seeWeedBitmap = new Bitmap("seeweed", "./Resources/images/seeweeds.png");
            _backgroundBitmap = new Bitmap("background", "./Resources/images/Background.png");
            var gameMusic = new Music("gamemusic", "./resources/sounds/gamemusic.mp3");
            var endMusic = new Music("endMusic", "./resources/sounds/endmusic.mp3");

            gameMusic.Play(10,0.5f);

            if (_Player.Quit)
            {
                endMusic.Play(1);
            }
        }

        public void HandleInput()
        {
            _Player.HandleInput();
            _Player.StayOnWindow(_GameWindow);
        }

        public void Update()
        {


            if (_Player.CurrentBitmapSize() < 160)
            {
                if (SplashKit.Rnd() < 0.02)
                {
                    var snapper = new Snapper(_GameWindow, _Player);

                    _fishes.Add(snapper);

                }


                if (SplashKit.Rnd() < 0.01)
                {
                    var smallJellyfish = new SmallJellyfish(_GameWindow, _Player);

                    _fishes.Add(smallJellyfish);

                }

                if (SplashKit.Rnd() < 0.01)
                {
                    var salmon = new Guppy(_GameWindow, _Player);

                    var predetor = new Predetor(_GameWindow, _Player);

                    _fishes.Add(salmon);
                    _fishes.Add(predetor);


                }

                if (SplashKit.Rnd() < 0.002)
                {
                    var jellyfish = new Jellyfish(_GameWindow, _Player);

                    _fishes.Add(jellyfish);

                }

                if (SplashKit.Rnd() < 0.001)
                {
                    var bigPredator = new BigPredetor(_GameWindow, _Player);

                    _fishes.Add(bigPredator);

                }
            }
            //else
            //{
            //    var mermaid = new Mermaid(_GameWindow,_Player);

            //    _fishes.Add(mermaid);
            //}
            
               
            
               


            foreach (var fish in _fishes) //check the size of the player and fish
            {
                if (_Player.CheckScale() < fish._FishBitmap.CellHeight)
                {
                    fish.Update(_Player);  //if bigger then chase the player 
                }
                else if(_Player.CheckScale() > fish._FishBitmap.CellHeight)
                {
                    fish.Update( _GameWindow); // if small run away
                }
                else
                {
                    fish.Update(_Player);
                }

            }

            CheckCollisions();
        }

        public void Draw()
        {
            _GameWindow.Clear(Color.DeepSkyBlue);

            SplashKit.DrawBitmap(_backgroundBitmap, 0, 0);

            string fishEaten = _fishEaten.Count.ToString();

            var font = SplashKit.LoadFont("Hand Scribble Sketch Times",
                "./resources/fonts/Hand Scribble Sketch Times.otf");

            SplashKit.DrawText($"Score: {fishEaten}", Color.Black, font, 30, 1400, 100);

            foreach (var fish in _fishes)
            {
                fish.Draw();
            }

            _Player.Draw();

            SplashKit.DrawBitmap(_seeWeedBitmap, 800, 700);
           

            _GameWindow.Refresh(60);
        }

        private void CheckCollisions()
        {
            List<Fish> fishToRemoveList = new List<Fish>();


            //check for collisions with the fish in the list 
            foreach (var fish in _fishes)
            {
                if (fish.IsOffScreen(_GameWindow))
                {
                    fishToRemoveList.Add(fish);

                }

                if (_Player.CollideWith(fish) && _Player.CheckScale() >= fish._FishBitmap.CellHeight)
                {
                    _fishEaten.Add(fish);
                    _score.Add(fish);
                    fishToRemoveList.Add(fish);
                    var biteSound = new SoundEffect("bite","./resources/sounds/bite.wav");
                    SplashKit.LoadSoundEffect("bite", "./resources/sounds/bite.wav");

                    SplashKit.PlaySoundEffect("bite");

                    switch (_fishEaten.Count)
                    {
                        case 1:
                            _Player._PlayerBitmap = new Bitmap("playerMedium", "./Resources/images/Small/Guppy Medium Sick right.png");//

                            SplashKit.DrawBitmap("playerMedium", _Player.X, _Player.Y);
                            break;
                        case 5:
                            _Player._PlayerBitmap = new Bitmap("playerLarge", "./Resources/images/Small/Guppy Large Sick right.png");//

                            SplashKit.DrawBitmap("playerLarge", _Player.X, _Player.Y);
                            break;
                        case 7:
                            _Player._PlayerBitmap = new Bitmap("playerMedium2", "./Resources/images/Large/Guppy Small Sick right.png");//

                            SplashKit.DrawBitmap("playerMedium2", _Player.X, _Player.Y);
                            break;
                        case 10:
                            _Player._PlayerBitmap = new Bitmap("playerMedium3", "./Resources/images/Large/Guppy Medium Sick right.png");

                            SplashKit.DrawBitmap("playerMedium3", _Player.X, _Player.Y);
                            break;
                        case 12:
                            _Player._PlayerBitmap = new Bitmap("playerMedium4", "./Resources/images/Large/Guppy Large Sick right.png");

                            SplashKit.DrawBitmap("playerMedium4", _Player.X, _Player.Y);
                            break;
                        case 18:
                            _Player._PlayerBitmap = new Bitmap("playerPredator", "./Resources/images/Large/Predator Sick right.png");

                            SplashKit.DrawBitmap("playerPredator", _Player.X, _Player.Y);
                            break;


                    }


                }

                if (_Player.CollideWith(fish) && _Player.CheckScale() < fish._FishBitmap.CellHeight)
                {
                    int remove = Math.Min(_fishEaten.Count, 2);
                    _fishEaten.RemoveRange(0,remove);
                    



                    if (_fishEaten.Count == 0)
                    {
                        _Player._PlayerBitmap = new Bitmap("DeadFish", "./Resources/images/Large/Guppy Small Dead.png");

                        SplashKit.DrawBitmap("DeadFish", _Player.X, _Player.Y);
                        SplashKit.LoadMusic("endMusic", "./resources/sounds/endmusic.mp3");

                        var font = SplashKit.LoadFont("Hand Scribble Sketch Times",
                            "./resources/fonts/Hand Scribble Sketch Times.otf");

                        SplashKit.PlayMusic("endMusic");
                        SplashKit.DisplayDialog("gameover",($"Game Over Your Score is {_score.Count}"),font,25);

                        _Player.Quit = true;
                    }
                    else 
                    {
                        _Player._PlayerBitmap = new Bitmap("Small", "./Resources/images/Small/Guppy Small Sick right.png");

                        SplashKit.DrawBitmap("Small", _Player.X, _Player.Y);
                        
                    }

                    
                }

            }

            foreach (var fish in fishToRemoveList)
            {
                _fishes.Remove(fish);
            }
        }
    }
}
