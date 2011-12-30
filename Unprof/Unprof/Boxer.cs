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
    class Boxer
    {
        SheetedSprite mCurrentSprite;
        SheetedSprite mSpriteIdle, mSpriteJabbing, mSpriteDuckAndCover;

        State mState;
        public State CurrentState
        {
            get { return mState; }
            set { mState = value; }
        }
        public enum State
        {
            Idle,
            Jabbing,
            DuckAndCovering
        }

        public Rectangle BoundingBox
        {
            get { return mCurrentSprite.Boundingbox; }
        }
        public Vector2 Position
        {
            get { return mCurrentSprite.Position;}
            set
            {
                mSpriteIdle.Position = value;
                mSpriteJabbing.Position = value;
                mSpriteDuckAndCover.Position = value;
            }
        }
        public float Rotation
        {
            get { return mCurrentSprite.Rotation; }
            set
            {
                mSpriteIdle.Rotation = value;
                mSpriteJabbing.Rotation = value;
                mSpriteDuckAndCover.Rotation = value;
            }
        }

        public Boxer(ResourcePool resourcePool)
        {
            mSpriteIdle = new SheetedSprite(resourcePool.BoxerIdle, 4, 100);
            mSpriteJabbing = new SheetedSprite(resourcePool.BoxerJabbing, 4, 100);
            mSpriteDuckAndCover = new SheetedSprite(resourcePool.BoxerDuckAndCover, 4, 100);
            mCurrentSprite = mSpriteIdle;
            Position = new Vector2(300, 300);
            mState = State.Idle;
        }

        public void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            HandleInputs(gameTime, keyState, prevState);
            CollisionManager.CheckBoxerAgainstRockets(this);

            switch (mState)
            {
                case State.Idle:
                    break;
                case State.Jabbing:
                    CollisionManager.CheckJabAgainstBadGuys(this);
                    if (mSpriteJabbing.DidLoop)
                    {
                        mCurrentSprite = mSpriteIdle;
                        mState = State.Idle;
                    }

                    break;
                case State.DuckAndCovering:
                    if (mSpriteDuckAndCover.DidLoop)
                    {
                        mCurrentSprite = mSpriteIdle;
                        mState = State.Idle;
                    }
                    break;

            }
            mCurrentSprite.Update(gameTime);
        }

        private void HandleInputs(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            if (keyState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
            {
                Jab();
            }
            if (keyState.IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
            {
                DuckAndCover();
            }
        }

        private void Jab()
        {
            if (mCurrentSprite != mSpriteJabbing)
            {
                mState = State.Jabbing;
                mSpriteJabbing.DidLoop = false;
                mCurrentSprite = mSpriteJabbing;
            }
        }

        private void DuckAndCover()
        {
            if (mCurrentSprite != mSpriteDuckAndCover)
            {
                mState = State.DuckAndCovering;
                mSpriteDuckAndCover.DidLoop = false;
                mCurrentSprite = mSpriteDuckAndCover;
            }
        }
            

        public void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }


    }
}
