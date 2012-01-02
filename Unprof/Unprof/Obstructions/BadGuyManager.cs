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
        Random rand;

        List<BadGuy> mBadGuys;
        public List<BadGuy> BadGuys
        {
            get { return mBadGuys; }
            set { mBadGuys = value; }
        }
        
        List<Projectile> mProjectiles;
        public List<Projectile> Projectiles
        {
            get { return mProjectiles; }
            set { mProjectiles = value; }
        }

        float fTimer;
        int iDelay;

        public BadGuyManager(int delay)
        {
            rand = new Random();

            mBadGuys = new List<BadGuy>();
            mProjectiles = new List<Projectile>();
            fTimer = delay;
            iDelay = delay;
        }

        public void Update(GameTime gameTime)
        {
            fTimer += CUtil.GameMilliseconds;

            if (fTimer > iDelay)
            {
                fTimer = fTimer - iDelay;
                AddBadGuy();
                AddRocket();
                AddMeteor();
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


            foreach (Projectile proj in mProjectiles)
            {
                proj.Update(gameTime);
            }

            // Clean up
            for (int i = mProjectiles.Count - 1; i >= 0; i--)
            {
                if (mProjectiles[i].IsMarkedForDeletion)
                    mProjectiles.RemoveAt(i);
            }
        }

        public void AddBadGuy()
        {
            Vector2 vec = new Vector2(CUtil.SCREEN_WIDTH - (CUtil.Camera.XOffset), 0);

            BadGuy badguy = new BadGuy(CUtil.ResourcePool.BoxerIdle, CUtil.ResourcePool.BoxerJabbing,
                vec, new Vector2(0.0f, 0));
            badguy.Scale = 0.5f;


            badguy.fPosY = CUtil.SCREEN_HEIGHT - CUtil.CurrentGame.Terrain.GetHeightOfX(vec.X) - (badguy.BoundingBox.Height / 2);

            mBadGuys.Add(badguy);
        }

        public void AddRocket()
        {
            Rocket rocket = new Rocket(CUtil.ResourcePool.RocketIdle, CUtil.ResourcePool.RocketDying,
                new Vector2(CUtil.SCREEN_WIDTH - (CUtil.Camera.XOffset), 300), new Vector2(0.22f, 0));
            mProjectiles.Add(rocket);
        }

        public void AddMeteor()
        {
            float ySpeed = rand.Next(50, 150);
            float xSpeed = rand.Next(0, 100);
            Vector2 velocity = new Vector2(xSpeed / 500, -ySpeed / 500);


            float xPosition = rand.Next(
                (int)( (CUtil.SCREEN_WIDTH / 2) - CUtil.Camera.XOffset),
                (int)(CUtil.SCREEN_WIDTH - CUtil.Camera.XOffset)
                );
            float yPosition = 5;
            Vector2 position = new Vector2(xPosition, yPosition);

            Meteor meteor = new Meteor(CUtil.ResourcePool.MeteorIdle, CUtil.ResourcePool.MeteorIdle,
                position, velocity);

            meteor.Scale = 0.5f;
            mProjectiles.Add(meteor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BadGuy badguy in mBadGuys)
            {
                badguy.Draw(spriteBatch);
            }
            foreach (Projectile proj in mProjectiles)
            {
                proj.Draw(spriteBatch);
            }

        }


    }
}
