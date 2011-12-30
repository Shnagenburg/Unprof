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
            foreach (Rocket rocket in CUtil.CurrentGame.BadGuyManager.Rockets)
            {
                if (
                    boxer.Position.X + boxer.BoundingBox.Width > rocket.Position.X && boxer.Position.X < rocket.Position.X + rocket.BoundingBox.Width &&
                    boxer.Position.Y + boxer.BoundingBox.Height > rocket.Position.Y && boxer.Position.Y < rocket.Position.Y + rocket.BoundingBox.Height
                    )
                {
                    if (boxer.CurrentState == Boxer.State.DuckAndCovering)
                    {
                        rocket.Die();
                    }
                    else
                    {
                        rocket.Explode();
                    }
                }
            }
        }
    }
}
