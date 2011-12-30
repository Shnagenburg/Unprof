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
    class ResourcePool
    {
        public Texture2D BoxerIdle;
        public Texture2D BoxerJabbing;
        public Texture2D BoxerDuckAndCover;

        public Texture2D RocketIdle;
        public Texture2D RocketDying;

        public Texture2D Explosion1;

        public void LoadContentForGame(ContentManager content)
        {
            BoxerIdle = content.Load<Texture2D>("idle1");
            BoxerJabbing = content.Load<Texture2D>("jab1");
            BoxerDuckAndCover = content.Load<Texture2D>("dnc");

            RocketIdle = content.Load<Texture2D>("rocket1");
            RocketDying = content.Load<Texture2D>("rocket1");

            Explosion1 = content.Load<Texture2D>("explos1");
        }


    }
}
