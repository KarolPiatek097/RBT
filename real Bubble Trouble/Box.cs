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
    public class Box
    {

        public Texture texture = new Texture(@"texture2.png");
        public Sprite background = new Sprite();
        public RectangleShape box = new RectangleShape();
        public List<SFML.Graphics.Drawable> allObjects = new List<SFML.Graphics.Drawable>();

        public List<Level> levelList = new List<Level>();
        
        public Box()
        {
            box.OutlineColor = Color.White;
            box.FillColor = Color.Transparent;
            box.OutlineThickness = 2f;
            box.Size = new Vector2f(MyGlobals.width - 100, MyGlobals.heigth - 100);
            box.Position = new Vector2f(50,50);

            background.Texture = texture;
            background.TextureRect = new IntRect(0, 0, MyGlobals.width, MyGlobals.heigth);


            
            allObjects.Add(background);
            allObjects.Add(box);
        }

    }
}
