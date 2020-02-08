using SplashKitSDK;

namespace GoFish
{
    public class Program
    {
        public static void Main()
        {
            var window = new Window("window", 1600, 1000);

            var player = new Player(window);  

            Game game = new Game(window, player);

            while (!window.CloseRequested && player.Quit != true)
            {



                SplashKit.ProcessEvents();
                window.Clear(Color.White);
                game.Draw();
                game.HandleInput();
                game.Update();
                window.Refresh(60);


            }

        }
    }
}
