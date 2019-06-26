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
    class ObstacleManager
    {
        //----------------------------------------Variabler och objekt
        Random rng;
        public List<GameObject> obstacles = new List<GameObject>();

        //----------------------------------------Egenskaper
        public ObstacleManager(ContentManager Content, int randomSeed)
        {
            //Ytterväggar
            int bottomY = (10 + 1) * (Game1.ScreenHeight / (10 + 1));
            int rightX = (10 + 1) * (Game1.ScreenWidth / (10 + 1));

            for (int x = 0; x < Game1.ScreenWidth; x += 10 + 1)
            {
                obstacles.Add(new GameObject(new Vector2(x, 0), Content, "tile", Color.Turquoise));
                obstacles.Add(new GameObject(new Vector2(x, bottomY), Content, "tile", Color.Turquoise));
            }
            
            for (int y = 0; y < Game1.ScreenHeight; y += 10 + 1)
            {
                obstacles.Add(new GameObject(new Vector2(0, y), Content, "tile", Color.Turquoise));
                obstacles.Add(new GameObject(new Vector2(rightX, y), Content, "tile", Color.Turquoise));
            }

            //Hinder
            if (randomSeed != 0)
            {
                rng = new Random(randomSeed);
                int nrOfObstacles = rng.Next(3, 8);

                for (int i = 0; i < nrOfObstacles; i++)
                {
                    int width = rng.Next(3, 15);
                    int height = rng.Next(3, 15);

                    int xStart = (10 + 1) * (rng.Next(0, Game1.ScreenWidth - width * (10 + 1)) / (10 + 1));
                    int yStart = (10 + 1) * (rng.Next(0, Game1.ScreenHeight - height * (10 + 1)) / (10 + 1));

                    int x = xStart;
                    for (int j = 0; j < width; j++, x += (10 + 1))
                    {
                        int y = yStart;
                        for (int k = 0; k < height; k++, y += (10 + 1))
                        {
                            obstacles.Add(new GameObject(new Vector2(x, y), Content, "tile", Color.Turquoise));
                        }
                    }
                }
            }
        }

        //----------------------------------------Metoder
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject wall in obstacles)
                wall.Draw(spriteBatch);
        }

    }
}
