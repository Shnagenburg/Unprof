using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Unprof
{
    public class Sprite
    {
        protected float fScale;
        public virtual float Scale
        {
            get { return fScale; }
            set
            {
                fScale = value;
                mBoundingBox.Width = (int) ( (float)mTexture.Width * fScale);
                mBoundingBox.Height = (int) ( (float)mTexture.Height * fScale); 
            }
        }

        protected float fRotation;
        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        protected Rectangle mBoundingBox;
        /// <summary>
        /// Does NOT track x and y!
        /// </summary>
        public Rectangle Boundingbox
        {
            get { return mBoundingBox;}
            set { mBoundingBox = value; }
        }

        protected Texture2D mTexture;

        public Vector2 Position;

        protected Vector2 mRotationCenter;

        public Sprite()
        {

        }
        public Sprite(Texture2D texture)
        {
            // load texture
            mTexture = texture;
            mRotationCenter = new Vector2(texture.Width / 2, texture.Height / 2);
            fScale = 1.0f;

            mBoundingBox = new Rectangle(0, 0, mTexture.Width, mTexture.Height);
        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(mTexture, 
                Position, 
                null, 
                Color.White, 
                fRotation,
                mRotationCenter,
                fScale,
                SpriteEffects.None, 
                0);  
        }
    }
}
