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
                if (
                    boxer.Position.X + boxer.BoundingBox.Width > badguy.Position.X && boxer.Position.X < badguy.Position.X + badguy.BoundingBox.Width &&
                    boxer.Position.Y + boxer.BoundingBox.Height > badguy.Position.Y && boxer.Position.Y < badguy.Position.Y + badguy.BoundingBox.Height
                    )
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
                    boxer.Position.X + boxer.BoundingBox.Width > proj.Position.X && boxer.Position.X < proj.Position.X + proj.BoundingBox.Width &&
                    boxer.Position.Y + boxer.BoundingBox.Height / 2 > proj.Position.Y - proj.BoundingBox.Height / 2 && boxer.Position.Y - boxer.BoundingBox.Height / 2 < proj.Position.Y + proj.BoundingBox.Height / 2
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
