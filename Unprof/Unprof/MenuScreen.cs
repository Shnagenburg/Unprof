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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Unprof
{
    /// <summary>
    /// The menu screen is the main menu players see when starting the game.
    /// </summary>
    class MenuScreen : Screen
    {
        /// <summary>
        /// Create a new main menu screen.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        public MenuScreen(EventHandler theScreenEvent): base(theScreenEvent)
        {

        }

        /// <summary>
        /// Load the main menu screen.
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
        }


        /// <summary>
        /// Update the main menu's elements
        /// </summary>
        /// <param name="theTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
       
            base.Update(theTime);
        }

        /// <summary>
        /// Draw the main menu's elements.
        /// </summary>
        public override void Draw(SpriteBatch theBatch)
        {
            base.Draw(theBatch);
        }

        public void SelectPlayEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Play, new EventArgs());
            return;
        }

        public void SelectOptionsEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Options, new EventArgs());
            return;
        }

        public void SelectQuitEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Quit, new EventArgs());
            return;
        }

        public void SelectHighScoreEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.HighScore, new EventArgs());
            return;
        }

    }
}
