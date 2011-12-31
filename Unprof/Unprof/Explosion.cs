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
    class Explosion
    {
        bool bIsMarkedForDeletion;
        public bool IsMarkedForDeletion
        {
            get { return bIsMarkedForDeletion; }
            set { bIsMarkedForDeletion = value; }
        }

        const float RUN_SPEED = 0.12f;

        SheetedSprite mCurrentSprite;
        SheetedSprite mSpriteIdle, mSpriteDying;

        State mState;
        enum State
        {
            Idle,
            Dying
        }

        public Rectangle BoundingBox
        {
            get { return mCurrentSprite.Boundingbox; }
        }

        Vector2 mDirection;

        public Vector2 Position
        {
            get { return mCurrentSprite.Position; }
            set
            {
                mSpriteIdle.Position = value;
                mSpriteDying.Position = value;
            }
        }

        float fPosX
        {
            get { return mCurrentSprite.Position.X; }
            set
            {
                mSpriteIdle.Position.X = value;
                mSpriteDying.Position.X = value;
            }
        }

        float fPosY
        {
            get { return mCurrentSprite.Position.Y; }
            set
            {
                mSpriteIdle.Position.Y = value;
                mSpriteDying.Position.Y = value;
            }
        }


        Vector2 mVelocity;
        public Vector2 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
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

        public Explosion(Texture2D idleTexture, Texture2D deathTexture, Vector2 position, Vector2 velocity)
        {
            mSpriteIdle = new SheetedSprite(idleTexture, 6, 75);
            mSpriteDying = new SheetedSprite(deathTexture, 6, 75);
            mCurrentSprite = mSpriteIdle;
            mVelocity = velocity;
            Position = position;
            mState = State.Idle;
            bIsMarkedForDeletion = false;
        }

        public void Update(GameTime gameTime)
        {
            switch (mState)
            {
                case State.Idle:
                    MoveForward(gameTime);
                    break;
                case State.Dying:
                    break;

            }
            mCurrentSprite.Update(gameTime);

            // Check if out of bounds
            if (fPosX > 800 || fPosX < 0 || fPosY < 0 || fPosY > 480)
                bIsMarkedForDeletion = true;

            if (mCurrentSprite.DidLoop == true)
                bIsMarkedForDeletion = true;

        }

        private void MoveForward(GameTime gameTime)
        {
            fPosX -= mVelocity.X * CUtil.GameMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }
    }
}
