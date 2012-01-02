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
    class BadGuy
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

        public float Scale
        {
            get { return mCurrentSprite.Scale; }
            set
            {
                mSpriteIdle.Scale = value;
                mSpriteDying.Scale = value;
            }
        }

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
            get { return mCurrentSprite.Position;}
            set
            {
                mSpriteIdle.Position = value;
                mSpriteDying.Position = value;
            }
        }

        Vector2 mVelocity;
        public Vector2 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        public float fPosX
        {
            get { return mCurrentSprite.Position.X;}
            set 
            {
                mSpriteIdle.Position.X = value;
                mSpriteDying.Position.X = value;
            }
        }

        public float fPosY
        {
            get { return mCurrentSprite.Position.Y;}
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

        public BadGuy(Texture2D idleTexture, Texture2D deathTexture, Vector2 position, Vector2 velocity)
        {
            mSpriteIdle = new SheetedSprite(idleTexture, 4, 100);
            mSpriteDying = new SheetedSprite(deathTexture, 4, 100);
            mCurrentSprite = mSpriteIdle;
            Position = position;
            mVelocity = velocity;
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
                    FlyOff(gameTime);
                    break;

            }
            mCurrentSprite.Update(gameTime);

            // Check if out of bounds
            if (fPosX > 800 - CUtil.Camera.XOffset || fPosX < 0 - CUtil.Camera.XOffset || fPosY < 0 || fPosY > 480)
                bIsMarkedForDeletion = true;
            
        }

        private void MoveForward(GameTime gameTime)
        {
            fPosX -= mVelocity.X * CUtil.GameMilliseconds;
        }

        private void FlyOff(GameTime gameTime)
        {
            fPosX += mDirection.X * CUtil.GameMilliseconds / 1000;
            fPosY += mDirection.Y * CUtil.GameMilliseconds / 1000;
            Rotation += CUtil.GameMilliseconds / 100; // REVISIT 
        }

        public void Die()
        {
            if (mState == State.Dying)
                return;

            mState = State.Dying;
            mCurrentSprite = mSpriteDying;

            // Randomize how they fly off the screen
            Random rand = new Random();
            int xVal = rand.Next(-500, 500);
            int yVal = rand.Next(-500, 0);

            mDirection = new Vector2(xVal, yVal);
        }




        public void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }
    }
}
