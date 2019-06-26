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
    class MovingGameObject: GameObject
    {
        //----------------------------------------Variabler och objekt
        protected Vector2 velocity;

        protected int rotation=90;
        protected int speed;

        //----------------------------------------Konstruktorer
        public MovingGameObject(Vector2 position, Vector2 velocity, ContentManager Content, string textureFilename, Color color):base(position,Content,textureFilename,color)
        {
            this.velocity = velocity;
        }

        //----------------------------------------Metoder
        public override void Update()
        {
            position += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, MathHelper.ToRadians(rotation), new Vector2(Width / 2, Height / 2), Size, SpriteEffects.None, 1);
        }
    }
}
