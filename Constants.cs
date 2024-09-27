
using System.Numerics;

namespace MasterGame
{
    public static class Constants
    {
        public static class Graphics
        {
            public const int GAME_HEIGHT = 160;
            public const int GAME_WIDTH = 240;
            public const int WINDOW_HEIGHT = 480;
            public const int WINDOW_WIDTH = 720;

            public const bool IS_FULL_SCREEN = false;
            public const int FLOOR = 128;

            public const bool DEBUG_SPRITE_MODE = false;
        } 

        public static class Physics
        {
            public const float GRAVITY = -10;
        }

        public class Kirby
        {
            public const int MAX_HEALTH = 6;
            public const int MAX_LIVES = 3;
        }

        public class Controller
        {
            // determines max time that can elapse for double button presses to register as a command
            public const double RESPONSE_TIME = 250;
            public const double SLIDE_TIME = 250;
        }

    }
}