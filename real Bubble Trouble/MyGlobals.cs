using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace real_Bubble_Trouble
{
    public static class MyGlobals
    {
        public enum Direction { Default, Left, Right, Shot };
        public static Direction dir = Direction.Default;
        public static int heigth= 1080;
        public static int width = 1920;
        public static float floorLevel = heigth - 50;

        public static int currentLevel = 1;

        public static Vector2f chainPosition = new Vector2f();

        public static int frameRate = 60;
        public static int frame = 0;
        public static int gameTime = 0;

        public static Vector2f playerVelocity = new Vector2f(0, 0);
        public static int playerSpeed = 5;
        public static Sprite playerHitbox = new Sprite();

        public static int sphereRadius = 300;

        public static Vector2f gravity = new Vector2f(0, -2);
        

        public static int getRand(int from, int to)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int rnd = r.Next(from, to);
            return rnd;
        }

    }
}
