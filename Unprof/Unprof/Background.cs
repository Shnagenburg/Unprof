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
        Sprite mSprite;

        public Background(ResourcePool pool)
        {
            mSprite = new Sprite(pool.Background1);
            mSprite.Position = new Vector2( mSprite.Boundingbox.Width / 2, mSprite.Boundingbox.Height / 2);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch theBatch)
        {
            mSprite.Draw(theBatch);
        }

    }
}
