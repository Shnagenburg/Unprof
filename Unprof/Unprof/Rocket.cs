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
    class Rocket
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

        float fPosX
        {
            get { return mCurrentSprite.Position.X;}
            set 
            {
                mSpriteIdle.Position.X = value;
                mSpriteDying.Position.X = value;
            }
        }

        float fPosY
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

        public Rocket(Texture2D idleTexture, Texture2D deathTexture, Vector2 position, Vector2 velocity)
        {
            mSpriteIdle = new SheetedSprite(idleTexture, 3, 100);
            mSpriteDying = new SheetedSprite(deathTexture, 3, 100);
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
                    FlyOff(gameTime);
                    break;

            }
            mCurrentSprite.Update(gameTime);

            // Check if out of bounds
            if (fPosX > 800 || fPosX < 0 || fPosY < 0 || fPosY > 480)
                bIsMarkedForDeletion = true;
            
        }

        private void MoveForward(GameTime gameTime)
        {
            fPosX -= mVelocity.X * (float)gameTime.ElapsedGameTime.Milliseconds;
        }

        private void FlyOff(GameTime gameTime)
        {
            fPosX += mDirection.X * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            fPosY += mDirection.Y * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Rotation += (float)gameTime.ElapsedGameTime.Milliseconds / 100; // REVISIT 
        }



        public void Die()
        {
            if (mState == State.Dying)
                return;

            mState = State.Dying;
            mCurrentSprite = mSpriteDying;

            // Randomize how they fly off the screen
            Random rand = new Random();
            int xVal = rand.Next(-500, 0);
            int yVal = rand.Next(-500, 0);

            mDirection = new Vector2(xVal, yVal);
        }

        public void Explode()
        {
            CUtil.CurrentGame.VisualEffectManager.AddExplosion(this.Position, this.mVelocity);
            bIsMarkedForDeletion = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }
    }
}
