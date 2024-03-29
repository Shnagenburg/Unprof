﻿using System;
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
         const float MOVE_SPEED = 0.20f;
        const float GRAVITY = 0.12f;
        const int JUMP_POWER = 3;
        bool bCanJump;
        int iLastHeightOfTerrain;
        SheetedSprite mCurrentSprite;
        SheetedSprite mSpriteIdle, mSpriteJabbing, mSpriteDuckAndCover;
        Vector2 mMoveVector;
        Vector2 mLastPosition;

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


        public float Scale
        {
            get { return mCurrentSprite.Scale; }
            set
            {
                mSpriteJabbing.Scale = value;
                mSpriteIdle.Scale = value;
                mSpriteDuckAndCover.Scale = value;
            }
        }


        protected float fPosX
        {
            get { return mCurrentSprite.Position.X; }
            set
            {
                mSpriteIdle.Position.X = value;
                mSpriteJabbing.Position.X = value;
                mSpriteDuckAndCover.Position.X = value;
            }
        }

        protected float fPosY
        {
            get { return mCurrentSprite.Position.Y; }
            set
            {
                mSpriteIdle.Position.Y = value;
                mSpriteJabbing.Position.Y = value;
                mSpriteDuckAndCover.Position.Y = value;
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
            Position = new Vector2(40, 0);
            mState = State.Idle;
            mMoveVector = Vector2.Zero;
            bCanJump = true;
            iLastHeightOfTerrain = 999;
            mLastPosition = new Vector2();
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
            mLastPosition.X = fPosX;
            mLastPosition.Y = fPosY;

            if (keyState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
            {
                Jab();
            }
            if (keyState.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S))
            {
                DuckAndCover();
            }
            if (keyState.IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
            {
                Jump();
            }
            
            // slo mo testing
            if (keyState.IsKeyDown(Keys.Q))
                CUtil.GameRate = 1.0f;
            if (keyState.IsKeyDown(Keys.E))
                CUtil.GameRate = 0.5f;

            // X Movement
            mMoveVector.X = 0;
            if (keyState.IsKeyDown(Keys.D))
                mMoveVector.X += 1;
            if (keyState.IsKeyDown(Keys.A))
                mMoveVector.X -= 1;
            Position += mMoveVector * MOVE_SPEED * CUtil.GameMilliseconds;

            // Screen scrolling
            fPosX += CUtil.CameraScrollSpeed * CUtil.GameMilliseconds;

            //////// testing
            //mMoveVector.Y = 0;
            //if (keyState.IsKeyDown(Keys.W))
            //    mMoveVector.Y -= 1;
            //if (keyState.IsKeyDown(Keys.S))
            //    mMoveVector.Y += 1;
            ////////

            mMoveVector.Y += GRAVITY;
            
            // Y adjustments
            CheckIfInGround(mLastPosition);

            // X adjustments
            CheckIfHittingWall(mLastPosition);
            
        }

        // Check to see if the boxer is in the ground.
        private bool CheckIfInGround(Vector2 lastPosition)
        {
            Point[] heightMap = CUtil.CurrentLevel.Terrain.MasterHeights;

            //int currentX = (int)Position.X;
            int currentX = (int)lastPosition.X;
            int currentHeightOfTerrain = -1;
            int heightOfBoxer = mCurrentSprite.Boundingbox.Height / 2;

            // Find the height that we stand on
            for (int i = 0; i < heightMap.Length - 1; i++)
            {
                if (currentX > heightMap[i].X && currentX <= heightMap[i + 1].X)
                {
                    currentHeightOfTerrain = heightMap[i].Y;
                }
            }


            if (currentHeightOfTerrain == -1) // Are we on the last stretch?
                currentHeightOfTerrain = heightMap[heightMap.Length - 1].Y;

            // Now that we know the height of the terrain, check if boxer is inside the terrain
            if (Position.Y + heightOfBoxer > CUtil.SCREEN_HEIGHT - currentHeightOfTerrain)
            {
                // hes inside of terrain, resolve y
                // move on top if he was above
                if (lastPosition.Y + heightOfBoxer < CUtil.SCREEN_HEIGHT - currentHeightOfTerrain)
                {
                    fPosY = CUtil.SCREEN_HEIGHT - currentHeightOfTerrain - 1 - (mCurrentSprite.Boundingbox.Height / 2);

                    bCanJump = true;
                }


                mMoveVector.Y = 0;

                return true;
            }
            return false;
        }

        // Handle X stuffs
        private bool CheckIfHittingWall(Vector2 lastposition)
        {

            Point[] heightMap = CUtil.CurrentLevel.Terrain.MasterHeights;
            int targetWallX = -1;
            int currentX = (int)Position.X;
            int currentHeightOfTerrain = -1;
            int heightOfBoxer = mCurrentSprite.Boundingbox.Height / 2;

            // Find the height that we stand on
            for (int i = 0; i < heightMap.Length - 1; i++)
            {
                if (currentX > heightMap[i].X && currentX < heightMap[i + 1].X)
                {
                    currentHeightOfTerrain = heightMap[i].Y;

                    // which side are we closer to?
                    if (  Math.Abs (fPosX - heightMap[i].X)  <  Math.Abs( fPosX - heightMap[i + 1].X) )
                    {
                        targetWallX = heightMap[i].X;
                    }
                    else
                    {
                        targetWallX = heightMap[i + 1].X;
                    }
                }
            }
            if (currentHeightOfTerrain == -1) // Are we on the last stretch?
            {
                currentHeightOfTerrain = heightMap[heightMap.Length - 1].Y;
                targetWallX = heightMap[heightMap.Length - 1].X;
            }

            // the boxer is inside the box
            if (Position.Y + heightOfBoxer > CUtil.SCREEN_HEIGHT - currentHeightOfTerrain)
            {
                // push him to the side
                if (lastposition.X < fPosX)
                    fPosX = targetWallX;
                else
                    fPosX = targetWallX + 1;

            }

            return false;
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

        private void Jump()
        {
            if (bCanJump)
            {
                mMoveVector.Y = -JUMP_POWER;
                bCanJump = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mCurrentSprite.Draw(spriteBatch);
        }


    }
}
