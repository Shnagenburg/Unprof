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
        Boxer mBoxer;
        BadGuy mBadGuy;
        public BadGuy BadGuy
        {
            get { return mBadGuy; }
            set { mBadGuy = value; }
        }
        BadGuyManager mBadGuyManager;
        public BadGuyManager BadGuyManager
        {
            get { return mBadGuyManager; }
            set { mBadGuyManager = value; }
        }
        
        Terrain mTerrain;
        public Terrain Terrain
        {
            get { return mTerrain; }
            set { mTerrain = value; }
        }

        VisualEffectManager mVisualEffectManager;
        public VisualEffectManager VisualEffectManager
        {
            get { return mVisualEffectManager; }
            set { mVisualEffectManager = value; }
        }
            

        /// <summary>
        /// Create a new game screen. Should be done every time there is a new game.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="contentManager"></param>
        public GameScreen(EventHandler theScreenEvent): base(theScreenEvent)
        {
            mBoxer = new Boxer(CUtil.ResourcePool);
            mBadGuyManager = new BadGuyManager(3000);
            mTerrain = new Terrain(CUtil.ResourcePool.Terrain1);
            mVisualEffectManager = new VisualEffectManager();
        }


        /// <summary>
        /// Update the GameScreen's elements.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            mBoxer.Update(gameTime, keyState, prevState);
            mBadGuyManager.Update(gameTime);
            mTerrain.Update(gameTime);
            mVisualEffectManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the GameScreen's elements.
        /// </summary>
        /// <param name="theBatch"></param>
        public override void Draw(SpriteBatch theBatch)
        {
            mTerrain.Draw(theBatch);
            mBadGuyManager.Draw(theBatch);
            mBoxer.Draw(theBatch);
            mVisualEffectManager.Draw(theBatch);
            base.Draw(theBatch);
        }


    }
}
