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
    class VisualEffectManager
    {
        List<Explosion> mExplosions;
        public List<Explosion> Explosion
        {
            get { return mExplosions; }
            set { mExplosions = value; }
        }
        
        float fTimer;
        int iDelay;

        public VisualEffectManager()
        {
            mExplosions = new List<Explosion>();
        }

        public void Update(GameTime gameTime)
        {

            foreach (Explosion explos in mExplosions)
            {
                explos.Update(gameTime);
            }

            // Clean up
            for (int i = mExplosions.Count - 1; i >= 0; i--)
            {
                if (mExplosions[i].IsMarkedForDeletion)
                    mExplosions.RemoveAt(i);
            }
        }

        public void AddExplosion(Vector2 origin, Vector2 velocity)
        {
            Explosion explos = new Explosion(CUtil.ResourcePool.Explosion1, CUtil.ResourcePool.Explosion1, origin, velocity);
            mExplosions.Add(explos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Explosion explos in mExplosions)
            {
                explos.Draw(spriteBatch);
            }

        }


    }
}
