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
    class BadGuyManager
    {
        List<BadGuy> mBadGuys;
        public List<BadGuy> BadGuys
        {
            get { return mBadGuys; }
            set { mBadGuys = value; }
        }
        
        List<Rocket> mRockets;
        public List<Rocket> Rockets
        {
            get { return mRockets; }
            set { mRockets = value; }
        }

        float fTimer;
        int iDelay;

        public BadGuyManager(int delay)
        {
            mBadGuys = new List<BadGuy>();
            mRockets = new List<Rocket>();
            fTimer = 0;
            iDelay = delay;
        }

        public void Update(GameTime gameTime)
        {
            fTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (fTimer > iDelay)
            {
                fTimer = fTimer - iDelay;
                AddBadGuy();
                AddRocket();
            }

            foreach (BadGuy badguy in mBadGuys)
            {
                badguy.Update(gameTime);
            }

            // Clean up
            for (int i = mBadGuys.Count - 1; i >= 0; i--)
            {
                if (mBadGuys[i].IsMarkedForDeletion)
                    mBadGuys.RemoveAt(i);
            }


            foreach (Rocket rocket in mRockets)
            {
                rocket.Update(gameTime);
            }

            // Clean up
            for (int i = mRockets.Count - 1; i >= 0; i--)
            {
                if (mRockets[i].IsMarkedForDeletion)
                    mRockets.RemoveAt(i);
            }
        }

        public void AddBadGuy()
        {
            BadGuy badguy = new BadGuy(CUtil.ResourcePool.BoxerIdle, CUtil.ResourcePool.BoxerJabbing, 
                new Vector2(800, 300), new Vector2(0.5f, 0) );
            mBadGuys.Add(badguy);
        }

        public void AddRocket()
        {
            Rocket rocket = new Rocket(CUtil.ResourcePool.RocketIdle, CUtil.ResourcePool.RocketDying, 
                new Vector2(600, 300), new Vector2(0.5f, 0) );
            mRockets.Add(rocket);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BadGuy badguy in mBadGuys)
            {
                badguy.Draw(spriteBatch);
            }
            foreach (Rocket rocket in mRockets)
            {
                rocket.Draw(spriteBatch);
            }

        }


    }
}
