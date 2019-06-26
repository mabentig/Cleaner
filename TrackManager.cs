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
    class TrackManager
    {
        //----------------------------------------Variabler och objekt
        ContentManager Content;
        public List<GameObject> tracks = new List<GameObject>();

        //----------------------------------------Egenskaper
        public int Count
        {
            get
            {
                return tracks.Count;
            }
        }

        //----------------------------------------Konstruktorer
        public TrackManager(ContentManager Content)
        {
            this.Content = Content;
        }

        //----------------------------------------Metoder
        public void Add(Vector2 position)
        {
            tracks.Add(new GameObject(position, Content, "tile", Color.Blue));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject track in tracks)
                track.Draw(spriteBatch);
        }
    }
}
