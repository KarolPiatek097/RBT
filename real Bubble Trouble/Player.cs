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
    public class Player
    {
        public Texture texture = new Texture(@"texture.png");
        public Sprite player = new Sprite();

        public RectangleShape chain = new RectangleShape(new Vector2f(7,MyGlobals.heigth-100));
        public Vector2f chainVelocity = new Vector2f(0, -15);
        public bool isShot = false;
        public Player()
        {
            
            player.Texture = texture;
            player.TextureRect = new IntRect(0, 0, 32, 32);
            player.Scale = new Vector2f(2, 2);
            player.Position = new Vector2f((MyGlobals.width) / 2 - 34, MyGlobals.heigth - 114);

            chain.FillColor = Color.White;
            chain.Origin = new Vector2f(0, 0);
            chain.Position = new Vector2f(player.Position.X, MyGlobals.heigth-50);
        }

        public void changeTexture()
        {
            int textureFrame = 0;
            textureFrame = MyGlobals.frame % 3;
            player.TextureRect = new IntRect(textureFrame * 32, (int)MyGlobals.dir * 32, 32, 32);
        }

        public void idle()
        {
            MyGlobals.dir = MyGlobals.Direction.Default;
            MyGlobals.playerVelocity = new Vector2f(0, 0);

        }

        void checkChainCollision()
        {
            if (MyGlobals.chainPosition.Y <= 50)
            {
                isShot = false;
                chain.Position = new Vector2f(player.Position.X, MyGlobals.heigth - 50);
            }
        }

        public void moveChain()
        {
            if(isShot==true)
            {
                chain.Position += chainVelocity;
                checkChainCollision();
                if (chain.Position.Y == 70)
                    chain.Position += new Vector2f(0, -20);
            }
        }




        public void move(SFML.Window.Keyboard.Key key)
        {

                switch (key)
                {
                    case Keyboard.Key.W:
                    case Keyboard.Key.Up:
                    case Keyboard.Key.Space:
                        if (isShot == false)
                        {
                            MyGlobals.dir = MyGlobals.Direction.Shot;
                            MyGlobals.playerVelocity = new Vector2f(0, 0);
                            isShot = true;
                            chain.Position = new Vector2f(player.Position.X + 32, MyGlobals.floorLevel);
                        }
                        break;
                    case Keyboard.Key.A:
                    case Keyboard.Key.Left:
                        MyGlobals.dir = MyGlobals.Direction.Left;
                        MyGlobals.playerVelocity = new Vector2f(-1, 0);

                        break;
                    case Keyboard.Key.D:
                    case Keyboard.Key.Right:
                        MyGlobals.dir = MyGlobals.Direction.Right;
                        MyGlobals.playerVelocity = new Vector2f(1, 0);
                        break;
                }
            

        }
    }
}