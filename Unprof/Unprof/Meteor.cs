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
    class Meteor : Projectile
    {
        const float SPIN_SPEED = 0.005f;

        public Meteor(Texture2D idleTexture, Texture2D deathTexture, Vector2 position, Vector2 velocity)
        {
            mSpriteIdle = new SheetedSprite(idleTexture, 1, 100);
            mSpriteDying = new SheetedSprite(deathTexture, 1, 100);
            mCurrentSprite = mSpriteIdle;
            mVelocity = velocity;
            Position = position;
            mState = State.Idle;
            bIsMarkedForDeletion = false;
        }


        public override void Update(GameTime gameTime)
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

            Rotation += SPIN_SPEED * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check if out of bounds
            if (fPosX > 800 || fPosX < 0 || fPosY < 0 || fPosY > 480)
                bIsMarkedForDeletion = true;

        }

        private void MoveForward(GameTime gameTime)
        {
            Position -= mVelocity * (float)gameTime.ElapsedGameTime.Milliseconds;
        }


        private void FlyOff(GameTime gameTime)
        {
            fPosX += mDirection.X * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            fPosY += mDirection.Y * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Rotation += (float)gameTime.ElapsedGameTime.Milliseconds / 100; // REVISIT 
        }


        public override void Die()
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

        public override void Explode()
        {
            CUtil.CurrentGame.VisualEffectManager.AddExplosion(this.Position, this.mVelocity);
            bIsMarkedForDeletion = true;
        }
    }
}
