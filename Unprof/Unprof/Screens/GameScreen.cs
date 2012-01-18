/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Unprof
{
    /// <summary>
    /// The game screen is the screen where all of the gameplay happens.
    /// </summary>
    class GameScreen : Screen
    {
        Level mCurrentLevel;
            

        /// <summary>
        /// Create a new game screen. Should be done every time there is a new game.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="contentManager"></param>
        public GameScreen(EventHandler theScreenEvent): base(theScreenEvent)
        {
            mCurrentLevel = new Level();
            CUtil.CurrentLevel = mCurrentLevel;
        }


        /// <summary>
        /// Update the GameScreen's elements.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            mCurrentLevel.Update(gameTime, keyState, prevState);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the GameScreen's elements.
        /// </summary>
        /// <param name="theBatch"></param>
        public override void Draw(SpriteBatch theBatch)
        {
            mCurrentLevel.Draw(theBatch);
        }


    }
}
