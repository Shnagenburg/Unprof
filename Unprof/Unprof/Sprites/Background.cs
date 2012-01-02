using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Unprof
{
    class Background
    {
        Sprite [] mSprites;
        Sprite mSprite;

        public Background(ResourcePool pool)
        {
            mSprite = new Sprite(pool.Background1);
            mSprite.Position = new Vector2( mSprite.Boundingbox.Width / 2, mSprite.Boundingbox.Height / 2);

            mSprites = new Sprite[2];
            Sprite s1 = new Sprite(pool.Background1);
            s1.Position = new Vector2(mSprite.Boundingbox.Width / 2, mSprite.Boundingbox.Height / 2);
            Sprite s2 = new Sprite(pool.Background1);
            s2.Position = new Vector2( (mSprite.Boundingbox.Width * 3) / 2, mSprite.Boundingbox.Height / 2);

            mSprites[0] = s1;
            mSprites[1] = s2;



        }

        public void Update(GameTime gameTime)
        {
            // Check to see if we are 100% off the camera, if so move it forward
            foreach (Sprite spr in mSprites)
            {
                if ( Math.Abs(CUtil.Camera.XOffset / 2) - ((spr.Boundingbox.Width / 2) + spr.Position.X)
                    > 0)
                {
                    spr.Position.X += spr.Boundingbox.Width * 2;
                }
            }
        }

        public void Draw(SpriteBatch theBatch)
        {
            foreach (Sprite spr in mSprites)
            {
                spr.Draw(theBatch);
            }
        }

    }
}
