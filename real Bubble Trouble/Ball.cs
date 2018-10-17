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
    public class Ball
    {
        public int size;
        public CircleShape ball = new CircleShape();
        public Vector2f ballVelocity = new Vector2f(-2, 10);
        public Vector2f gravity = new Vector2f(0, 15f);
        public int bounceHeigth;
        public int verticalVelocity = -5;
        public int color;
        public int positionX;

        bool start = true;

        float inAirTime = 0;

        public Ball(int _size, int _positionX, int _color, int _sign)
        {
            color = _color;
            size = _size;

            verticalVelocity *= _sign;


            switch (_color)
            {
                case 1:
                    ball.Texture = new Texture(@"spheres\s1.png");

                    break;
                case 2:
                    ball.Texture = new Texture(@"spheres\s2.png");
                    break;
                case 3:
                    ball.Texture = new Texture(@"spheres\s3.png");
                    break;
                case 4:
                    ball.Texture = new Texture(@"spheres\s4.png");
                    break;
                case 5:
                    ball.Texture = new Texture(@"\spheres\s5.png");
                    break;
            }

            switch (_size)
            {
                case 1:
                    bounceHeigth = -55;
                    ball.Radius = 10;
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth-140);
                    break;
                case 2:
                    bounceHeigth = -70;
                    ball.Radius = 20;
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth - 180);
                    break;
                case 3:
                    bounceHeigth = -85;
                    ball.Radius = 40;
                    ball.Position = new Vector2f(positionX, MyGlobals.heigth - 280);
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth - 240);
                    break;
                case 4:
                    bounceHeigth = -100;
                    ball.Radius = 80;
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth - 380);
                    break;
                case 5:
                    bounceHeigth = -120;
                    ball.Radius = 160;
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth - 660);
                    break;
                case 6:
                    bounceHeigth = -120;
                    ball.Radius = 220;
                    ball.Position = new Vector2f(checkPosition(ball.Radius, _positionX), MyGlobals.heigth - 710);
                    break;
            }


            ball.Origin = new Vector2f(ball.Radius, ball.Radius);
            ballVelocity.Y = 10;


        }

        public Ball(int _size, Texture _texture)
        {
            if (size != 1)
            {
                size = size - 1;
                ball.Texture = _texture;
            }
        }

        public int checkPosition(float _ballRadius, int _positionX)
        {
            int positionX = _positionX;

            if(positionX - ball.Radius <= 50 )
            {
               positionX = (int)_ballRadius + 52;
            }else if (_positionX + ball.Radius >= MyGlobals.width - 50)
            {
               positionX = MyGlobals.width - (int)_ballRadius - 51;
            }

            return positionX;
        }

        public bool checkWallColision()
        {
            bool doesCollide = true;
            if (ball.Position.X < MyGlobals.width - ball.Radius - 50 && ball.Position.X > 50 + ball.Radius)
                doesCollide = false;

            return doesCollide;
        }

        public bool checkPlayerCollision()
        {
            bool doesColide = false;

            float distX = MyGlobals.playerHitbox.Position.X - ball.Position.X;
            float distY = MyGlobals.playerHitbox.Position.Y - ball.Position.Y;

            if ((distX + 20) * (distX + 20) + distY * distY < ball.Radius * ball.Radius ||
                (distX + 46) * (distX + 46) + distY * distY < ball.Radius * ball.Radius ||
                (distX + 20) * (distX + 20) + (distY+30) * (distY+30) < ball.Radius * ball.Radius ||
                (distX + 46) * (distX + 46) + (distY + 30) * (distY + 30) < ball.Radius * ball.Radius ||
                (distX + 20) * (distX + 20) + (distY + 30) * (distY + 30) < ball.Radius * ball.Radius ||
                (distX + 46) * (distX + 46) + (distY + 55) * (distY + 55) < ball.Radius * ball.Radius)
            {
                doesColide = true;
                //game over
            }


            return doesColide;
        }

        public bool checkChainCollision()
        {
            bool doesColide = false;

            float distX = MyGlobals.chainPosition.X - ball.Position.X;
            float distY = MyGlobals.chainPosition.Y - ball.Position.Y;

            for(int i=0;i<980; i+=5)
            {
                if (distX * distX + (distY + i) * (distY + i) < ball.Radius * ball.Radius)
                {
                    doesColide = true;
                }

            }
                return doesColide;
        }

        void roundPosition()
        {
            if (ball.Position.Y % 10 != 0)
            {
                if (ball.Position.Y % 10 < 5)
                    while (ball.Position.Y % 10 != 0)
                        ball.Position -= new Vector2f(0, 1);

                if (ball.Position.Y % 10 >= 5)
                    while (ball.Position.Y % 10 != 0)
                        ball.Position += new Vector2f(0, 1);
            }
        }


        public void move()
        {

            if (checkPlayerCollision() == true)
                ballVelocity = new Vector2f(0, 0);
            if (checkWallColision() == true)
                verticalVelocity *= -1;



            if (ball.Position.Y <= MyGlobals.heigth - 50 - ball.Radius && checkPlayerCollision() == false)
            {

                inAirTime += 0.5f;
                if (inAirTime > 0.15f)
                    inAirTime = 0.15f;

                double velMultTime = Math.Round(ballVelocity.Y * inAirTime);

                ballVelocity.Y += gravity.Y * inAirTime;
                ball.Position += new Vector2f(verticalVelocity, (float)velMultTime);

                if (ball.Position.Y + ball.Radius >= MyGlobals.heigth - 50 && checkPlayerCollision() == false)
                {
                    
                    ballVelocity.Y =  bounceHeigth;
                    float ballX = ball.Position.X;
                    ball.Position = new Vector2f(ballX, MyGlobals.floorLevel-ball.Radius);
                    inAirTime = 0;
                }


            }
        }
    }
}
