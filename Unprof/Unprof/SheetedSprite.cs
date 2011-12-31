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
    public class SheetedSprite : Sprite
    {
        Rectangle mFrameRect;
        float iTimer;
        int iFrames;
        int iDelay;

        // Has this done a single pass?
        bool bDidLoop;
        public bool DidLoop
        {
            get { return bDidLoop; }
            set { bDidLoop = value; }
        }

        public SheetedSprite(Texture2D texture, int frames, int delay)
        {
            // load texture
            mTexture = texture;
            mFrameRect = new Rectangle(0, 0, texture.Width / frames, texture.Height);
            mRotationCenter = new Vector2(mFrameRect.Width / 2, mFrameRect.Height / 2);
            fScale = 1.0f;
            iFrames = frames;
            iTimer = 0;
            iDelay = delay;
            mBoundingBox = new Rectangle(0, 0, mFrameRect.Width, mFrameRect.Height);
        }

        public void Update(GameTime gameTime)
        {
            iTimer += CUtil.GameMilliseconds;
            if (iTimer > iDelay)
            {
                NextFrame();
                iTimer = iTimer - iDelay;
            }
        }

        private void NextFrame()
        {
            mFrameRect.X += mFrameRect.Width;

            // Loop around
            if (mFrameRect.X >= mTexture.Width)
            {
                mFrameRect.X = 0;
                bDidLoop = true;
            }
        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(mTexture,
                Position,
                mFrameRect,
                Color.White,
                fRotation,
                mRotationCenter,
                fScale,
                SpriteEffects.None,
                0);
        }
    }
}
