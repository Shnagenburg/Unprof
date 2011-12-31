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
    class CUtil
    {
        static public GraphicsDevice GraphicsDevice;
        static public GameScreen CurrentGame;
        static public ResourcePool ResourcePool;
        static public float GameRate;
        static public GameTime GameTime;
        static public Camera Camera;
        static public float CameraScrollSpeed = 0.04f;

        /// <summary>
        /// The time since the last update of the game loop, with the game rate applied
        /// </summary>
        static public float GameMilliseconds
        {
            get { return GameTime.ElapsedGameTime.Milliseconds * GameRate; }
        }
    }
}
