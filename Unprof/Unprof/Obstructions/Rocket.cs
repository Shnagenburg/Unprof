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
    class Rocket : Projectile
    {       

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
            CUtil.CurrentLevel.VisualEffectManager.AddExplosion(this.Position, this.mVelocity);
            bIsMarkedForDeletion = true;
        }
    }
}
