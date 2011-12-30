using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unprof
{
    public class Projectile
    {
        protected bool bIsMarkedForDeletion;
        public bool IsMarkedForDeletion
        {
            get { return bIsMarkedForDeletion; }
            set { bIsMarkedForDeletion = value; }
        }

        protected SheetedSprite mCurrentSprite;
        protected SheetedSprite mSpriteIdle, mSpriteDying;

        protected State mState;
        public enum State
        {
            Idle,
            Dying
        }

        public Rectangle BoundingBox
        {
            get { return mCurrentSprite.Boundingbox; }
        }

        protected Vector2 mDirection;

        public Vector2 Position
        {
            get { return mCurrentSprite.Position; }
            set
            {
                mSpriteIdle.Position = value;
                mSpriteDying.Position = value;
            }
        }

        protected Vector2 mVelocity;
        public Vector2 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        protected float fPosX
        {
            get { return mCurrentSprite.Position.X; }
            set
            {
                mSpriteIdle.Position.X = value;
                mSpriteDying.Position.X = value;
            }
        }

        protected float fPosY
        {
            get { return mCurrentSprite.Position.Y; }
            set
            {
                mSpriteIdle.Position.Y = value;
                mSpriteDying.Position.Y = value;
            }
        }

        public float Rotation
        {
            get { return mCurrentSprite.Rotation; }
            set
            {
                mSpriteIdle.Rotation = value;
                mSpriteDying.Rotation = value;
            }
        }


        public virtual void Update(GameTime gameTime)
        { }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }

        public virtual void Die()
        { }
        public virtual void Explode()
        { }
    }
}
