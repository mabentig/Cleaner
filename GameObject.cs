using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner
{
    class GameObject
    {
        //----------------------------------------Variabler och objekt
        public Color color;
        protected Texture2D texture;
        protected Vector2 position;
        protected float size;

        //----------------------------------------Egenskaper
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }
        public float Size { get; set; }

        public Vector2 PositionCenter
        {
            get
            {
                Vector2 positionCenter = new Vector2();
                positionCenter.X = position.X;// - Width / 2;
                positionCenter.Y = position.Y;// + Height/ 2;

                return positionCenter;
            }
        }

        public virtual BoundingRectangle Hitbox
        {
            get
            {
                BoundingRectangle hitbox = new BoundingRectangle(PositionCenter.ToPoint(), new Size2(Width / 2, Height / 2));
                return hitbox;
            }
        }

        //----------------------------------------Konstruktorer
        public GameObject(Vector2 position, ContentManager Content, string textureFilename, Color color)
        {
            Size = 1;
            texture = Content.Load<Texture2D>(textureFilename);
            this.color = color;
            this.position = position;// - new Vector2(Width / 2, Height / 2);
            size = Width/2;
        }

        //----------------------------------------Metoder
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //            spriteBatch.Draw(texture, position, color);
            spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, Size, SpriteEffects.None, 1);
        }

        public virtual void Update()
        {
        }

    }
}
