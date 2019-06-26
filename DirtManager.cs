using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner
{
    class DirtManager
    {
        //----------------------------------------Variabler och objekt
        public List<GameObject> dirts = new List<GameObject>();
        public List<GameObject> cleans = new List<GameObject>();

        //----------------------------------------Egenskaper
        public int Cleaned
        {
            get
            {
                return cleans.Count;
            }
        }

        ////----------------------------------------Konstruktorer
        public DirtManager(ContentManager Content)
        {
            //Fyller med smutsrutor
            for (int x = 0; x < Game1.ScreenWidth - 10; x += 10 + 1)
            {
                for (int y = 0; y < Game1.ScreenHeight - 10; y += 10 + 1)
                    dirts.Add(new GameObject(new Vector2(x, y), Content, "tile", Color.DarkGray));
            }
        }

        ////----------------------------------------Metoder
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject dirt in dirts)
                dirt.Draw(spriteBatch);
            foreach (GameObject clean in cleans)
                clean.Draw(spriteBatch);
        }
    }
}