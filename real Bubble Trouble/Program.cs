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
    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box();
            Player player = new Player();

            box.allObjects.Add(player.chain);
            box.allObjects.Add(player.player);
            box.levelList.Add(new Level(1));


            var window = new RenderWindow(new VideoMode(Convert.ToUInt32(MyGlobals.width), Convert.ToUInt32(MyGlobals.heigth)), "Bubble Trouble", Styles.Default);
            window.SetFramerateLimit(Convert.ToUInt32(MyGlobals.frameRate));
            window.Closed += (s, a) => window.Close();
            window.KeyPressed += (s, a) => player.move(a.Code);
            window.KeyReleased += (s, a) => player.idle();

            void update()
            {
                MyGlobals.playerHitbox.TextureRect = player.player.TextureRect;
                MyGlobals.playerHitbox.Position = player.player.Position;
                MyGlobals.playerHitbox.Scale = player.player.Scale;

                MyGlobals.chainPosition = player.chain.Position;

                foreach (var obj in box.levelList[MyGlobals.currentLevel - 1].ballList)
                    obj.positionX = (int)obj.ball.Position.X;

            }

            while(window.IsOpen)
            {
                window.DispatchEvents();

                update();


                for(int i =0; i< box.levelList[MyGlobals.currentLevel - 1].ballQuantity; i++)
                {
                    update();
                    if (box.levelList[MyGlobals.currentLevel - 1].ballList[i].checkChainCollision())
                    {
                        player.isShot = false;
                        player.chain.Position = new Vector2f(50, MyGlobals.heigth - 50);
                        update();
                        Ball b1 = new Ball(box.levelList[MyGlobals.currentLevel - 1].ballList[i].size - 1,
                                                                                        box.levelList[MyGlobals.currentLevel - 1].ballList[i].positionX,
                                                                                        box.levelList[MyGlobals.currentLevel - 1].ballList[i].color,
                                                                                        1);
                        Ball b2 = new Ball(box.levelList[MyGlobals.currentLevel - 1].ballList[i].size - 1,
                                                                                        box.levelList[MyGlobals.currentLevel - 1].ballList[i].positionX,
                                                                                        box.levelList[MyGlobals.currentLevel - 1].ballList[i].color,
                                                                                        -1);
                        b1.ballVelocity.Y = b1.bounceHeigth;
                        b2.ballVelocity.Y = b2.bounceHeigth;
                        b1.ball.Position = new Vector2f(box.levelList[MyGlobals.currentLevel - 1].ballList[i].positionX, box.levelList[MyGlobals.currentLevel - 1].ballList[i].ball.Position.Y);
                        b2.ball.Position = new Vector2f (box.levelList[MyGlobals.currentLevel - 1].ballList[i].positionX, box.levelList[MyGlobals.currentLevel - 1].ballList[i].ball.Position.Y);

                        box.levelList[MyGlobals.currentLevel - 1].ballList.Add(b1);
                        box.levelList[MyGlobals.currentLevel - 1].ballList.Add(b2);

                        box.levelList[MyGlobals.currentLevel - 1].ballQuantity += 2;

                        box.levelList[MyGlobals.currentLevel - 1].ballList.Remove(box.levelList[MyGlobals.currentLevel - 1].ballList[i]);
                        box.levelList[MyGlobals.currentLevel - 1].ballQuantity--;

                        update();
                    }
                }


                #region game time
                MyGlobals.frame++;
                if (MyGlobals.frame == MyGlobals.frameRate)
                {
                    MyGlobals.gameTime++;
                    MyGlobals.frame = 0;
                    //Console.WriteLine(MyGlobals.gameTime);
                }
                #endregion
                //freeze
                if (MyGlobals.gameTime >= 1)
                {
                    if(MyGlobals.frame % 2 == 0)
                    player.changeTexture();
                    if (player.player.Position.X + MyGlobals.playerVelocity.X + MyGlobals.playerSpeed > 36 && player.player.Position.X + MyGlobals.playerVelocity.X + MyGlobals.playerSpeed < MyGlobals.width - 94)
                        player.player.Position += MyGlobals.playerVelocity * MyGlobals.playerSpeed;

                    player.moveChain();
                    foreach (var obj in box.levelList[MyGlobals.currentLevel-1].ballList)
                        obj.move();
                }


                window.Clear();

                foreach (var obj in box.allObjects)
                    window.Draw(obj);
                foreach (var obj in box.levelList[MyGlobals.currentLevel-1].ballList)
                    window.Draw(obj.ball);


                window.Display();
            }

        }
    }
}
