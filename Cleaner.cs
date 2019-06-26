using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

using MonoGame.Extended;

namespace Cleaner
{
    class Cleaner:MovingGameObject
    {
        //----------------------------------------Variabler och objekt
        bool isColliding;
        Random rng = new Random();
        int rotateLeft = 0, ticksLeft = 0;

        //----------------------------------------Egenskaper
        public bool IsColliding
        {
            get
            {
                return isColliding;
            }
        }
        public bool IsRotating
        {
            get
            {
                return rotateLeft != 0;
            }
        }

        public bool IsDriving
        {
            get
            {
                return speed != 0;
            }
        }

        public new CircleF Hitbox
        {
            get
            {
                CircleF hitbox = new CircleF(PositionCenter.ToPoint(), size * 0.90f);
                return hitbox;
            }
        }

        public CircleF NextHitbox
        {
            get
            {

                Vector2 nextCenter = new Vector2(PositionCenter.X, PositionCenter.Y);
                nextCenter += velocity;

                return new CircleF(nextCenter, size * 0.90f);

            }
        }

        //----------------------------------------Konstruktorer
        public Cleaner(Vector2 position, ContentManager Content,string textureFilename, Color color):base(position,Vector2.Zero, Content, textureFilename, color)
        {

        }

        //----------------------------------------Metoder
        public void Rotate(int degrees)
        {
            rotateLeft = degrees;
                
        }

        public void Forward()
        {
            speed = 3;
        }

        public void Forward(int ticks)
        {
            ticksLeft = ticks;
            speed = 3;
        }

        public void Stop()
        {
            speed = 0;
            velocity = Vector2.Zero;
            ticksLeft = 0;
        }

        public void Reverse()
        {
            speed = -1;
        }
        public void Reverse(int ticks)
        {
            ticksLeft = ticks;
            speed = -1;
        }

        public List<GameObject> Update(List<GameObject> dirts, List<GameObject> obstacles, TrackManager trackManager)
        {
            //Sväng lite ibland
            if (rng.Next(100) <= 1 && !IsRotating)
                Rotate(rng.Next(5) - 2);

            List<GameObject> cleans = new List<GameObject>();

            //Stanna - kört färdigt
            if (ticksLeft != 0)
            {
                ticksLeft--;
                if (ticksLeft == 0)
                    Stop();
            }

            isColliding = false;

            //Har inte svängt färdigt
            if (rotateLeft != 0)
            {
                int rotateNow = Math.Sign(rotateLeft);

                rotation += rotateNow;
                rotateLeft -= rotateNow;
            }

            velocity.X = speed * (float)Math.Cos(MathHelper.ToRadians(rotation - 90));
            velocity.Y = speed * (float)Math.Sin(MathHelper.ToRadians(rotation - 90));

            //Städat ny ruta?
            for (int i = 0; i < dirts.Count;)
            {
                if (Colliding(dirts[i].Hitbox))
                {
                    dirts[i].color = Color.SandyBrown;
                    cleans.Add(dirts[i]);
                    dirts.RemoveAt(i);
                }

                else
                    i++;
            }

            //Kollision?
            foreach (GameObject obstacle in obstacles)
            {
                if (WillCollide(obstacle.Hitbox))
                {
                    isColliding = true;
                    Stop();
                    break;
                }
            }


            //Spåret!
            if(IsDriving)
                trackManager.Add(position);

            //Superklassens Update
            this.Update();

            return cleans;
        }

        //Kolliderar nu?
        private bool Colliding(BoundingRectangle otherItem)
        {
            return Hitbox.Intersects(otherItem);
        }

        //Kolliderar i nästa frame?
        private bool WillCollide(BoundingRectangle otherItem)
        {
            return NextHitbox.Intersects(otherItem);
        }


    }
}
