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
    /// <summary>
    /// Handles level progression
    /// </summary>
    class LevelManager
    {
        Level mCurrentLevel;
        List<Level> mLevels;
        public LevelManager()
        {

        }

        public void Update(GameTime gameTime)
        {
            //mCurrentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //mCurrentLevel.Draw(spriteBatch);
        }
    }
}
