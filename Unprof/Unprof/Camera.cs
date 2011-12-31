using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Unprof
{
    class Camera
    {
        float xOffset;
        float yOffset;

        public Matrix TransformationMatrix
        {
            get { return Matrix.CreateTranslation(xOffset, yOffset, 0.0f); }
        }
        public Matrix BackgroundTransformationMatrix
        {
            get { return Matrix.CreateTranslation(xOffset / 2, yOffset / 2, 0.0f); }
        }

        public Camera()
        {
            xOffset = 0;
            yOffset = 0;
        }

        public void Update(GameTime gameTime)
        {
            xOffset -= CUtil.CameraScrollSpeed * CUtil.GameMilliseconds;
        }
    }
}
