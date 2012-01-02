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

namespace Unprof
{
    /// <summary>
    /// The options screen lets you configure the game.
    /// </summary>
    class OptionsScreen : Screen
    {

        /// <summary>
        /// Create a new options menu.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="graphics"></param>
        public OptionsScreen(EventHandler theScreenEvent)
            : base(theScreenEvent)
        {
        }

        /// <summary>
        /// Load the content for the options menu.
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
        }


        /// <summary>
        /// Update the option screen's elements.
        /// </summary>
        /// <param name="theTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
            base.Update(theTime);
        }

        /// <summary>
        /// Draw the option screen's elements.
        /// </summary>
        /// <param name="theBatch"></param>
        public override void Draw(SpriteBatch theBatch)
        {
            base.Draw(theBatch);
        }

    }
}
