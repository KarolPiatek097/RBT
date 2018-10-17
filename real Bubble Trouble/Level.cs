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
    public class Level
    {
        public List<Ball> ballList = new List<Ball>();

        public int nr;
        public int ballQuantity;
        public int time;

        public Level(int _nr)
        {
            nr = _nr;

            switch(_nr)
            {
                case 1:
                    ballList.Add(new Ball(6, MyGlobals.width/2, 1,1));
                    //ballList.Add(new Ball(1, MyGlobals.width / 2 + 20, 1, -1));
                    ballQuantity = 1;
                    break;
            }
        }

    }
}
