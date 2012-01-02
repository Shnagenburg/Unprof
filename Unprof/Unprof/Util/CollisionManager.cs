using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unprof
{
    class CollisionManager
    {
        static public void CheckJabAgainstBadGuys(Boxer boxer)
        {
            foreach (BadGuy badguy in CUtil.CurrentGame.BadGuyManager.BadGuys)
            {
                //if (RectA.X1 < RectB.X2 && 
                //    RectA.X2 > RectB.X1 &&
                //    RectA.Y1 < RectB.Y2 && 
                //    RectA.Y2 > RectB.Y1)
                if (
                    boxer.Position.X - boxer.BoundingBox.Width / 2 < badguy.Position.X + badguy.BoundingBox.Width / 2 &&
                    boxer.Position.X + boxer.BoundingBox.Width / 2 > badguy.Position.X - badguy.BoundingBox.Width / 2 &&

                    boxer.Position.Y - boxer.BoundingBox.Height / 2 < badguy.Position.Y + badguy.BoundingBox.Height / 2 &&
                    boxer.Position.Y + boxer.BoundingBox.Height / 2 > badguy.Position.Y - badguy.BoundingBox.Height / 2)                    
                {
                    badguy.Die();
                }
            }
        }

        static public void CheckBoxerAgainstRockets(Boxer boxer)
        {
            foreach (Projectile proj in CUtil.CurrentGame.BadGuyManager.Projectiles)
            {
                if (
                    boxer.Position.X - boxer.BoundingBox.Width / 2 < proj.Position.X + proj.BoundingBox.Width / 2 &&
                    boxer.Position.X + boxer.BoundingBox.Width / 2 > proj.Position.X - proj.BoundingBox.Width / 2 &&

                    boxer.Position.Y - boxer.BoundingBox.Height / 2 < proj.Position.Y + proj.BoundingBox.Height / 2 &&
                    boxer.Position.Y + boxer.BoundingBox.Height / 2 > proj.Position.Y - proj.BoundingBox.Height / 2     
                )
                {
                    if (boxer.CurrentState == Boxer.State.DuckAndCovering)
                    {
                        proj.Die();
                    }
                    else
                    {
                        proj.Explode();
                    }
                }
            }
        }
    }
}
