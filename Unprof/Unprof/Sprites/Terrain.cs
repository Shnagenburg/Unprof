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
    class Terrain
    {
        const int SCREEN_HEIGHT = 480; 
        const float SCROLL_SPEED = 0.02f;

        // The X and Y values of where the height changes
        Point [] mMasterHeights;
        public Point[] MasterHeights
        {
            get { return mMasterHeights; }
            set { mMasterHeights = value; }
        }

        List<Vector2> mMapping;
        List<Vector2> mCutOffMapping;
        List<Rectangle> mCutOffMappingSources;

        Texture2D mTexture;

        Vector2 OffsetPosition;

        public Terrain(Texture2D texture)
        {
            mTexture = texture;
            mMapping = new List<Vector2>();
            mCutOffMapping = new List<Vector2>();
            mCutOffMappingSources = new List<Rectangle>(); 

            Point p1 = new Point(0, 50);
            Point p2 = new Point(200, 10);
            Point p3 = new Point(300, 50);
            Point p4 = new Point(350, 100);
            Point p5 = new Point(1000, 20);
            Point p6 = new Point(5000, 0);

            mMasterHeights = new Point[6];
            mMasterHeights[0] = p1;
            mMasterHeights[1] = p2;
            mMasterHeights[2] = p3;
            mMasterHeights[3] = p4;
            mMasterHeights[4] = p5;
            mMasterHeights[5] = p6;

            OffsetPosition = Vector2.Zero;

            CalculateTextureMapping();
        }

        // Given our terrain set, where do the textures need to go?
        private void CalculateTextureMapping()
        {
            int textWidth = mTexture.Width;
            int textHeight = mTexture.Height;
            int currentHeight = mMasterHeights[0].Y;
            Point nextHeightChange = mMasterHeights[1];
            int heightMapIterator = 1;

            // How long is this map?
            int xMax = mMasterHeights[mMasterHeights.Length - 1].X;


            int i = 0;
            // Fill the list with vectors
            while (i < xMax)
            {
                // Make a column of vectors
                int y = currentHeight;
                if (y > 0)
                {
                    int numberInColumn = (y / textHeight);

                    if (nextHeightChange.X - i >= textWidth)
                    {
                        for (int a = 0; a < numberInColumn; a++)
                        {                            
                            mMapping.Add(new Vector2(i, SCREEN_HEIGHT -   ((a + 1) * textHeight)   ));
                        }
                        mCutOffMapping.Add(new Vector2(i, SCREEN_HEIGHT - y) );
                        mCutOffMappingSources.Add(
                                new Rectangle(
                                    0,
                                    textHeight - (y - numberInColumn * textHeight),
                                    textWidth,
                                    y - numberInColumn * textHeight)
                            );
                    }
                    else
                    {
                        for (int a = 0; a < numberInColumn; a++)
                        {
                            mCutOffMapping.Add(
                                new Vector2(   i,   SCREEN_HEIGHT - ((a + 1) * textHeight)   )
                                );
                            mCutOffMappingSources.Add(
                                new Rectangle(
                                    0,
                                    0,
                                    nextHeightChange.X - i,
                                    textHeight)
                            );
                        }
                        mCutOffMapping.Add(  
                            new Vector2(i, SCREEN_HEIGHT - y) 
                            );
                        mCutOffMappingSources.Add(
                                new Rectangle(
                                    0,
                                    textHeight - (y - numberInColumn * textHeight),
                                    nextHeightChange.X - i,
                                    y - numberInColumn * textHeight)
                            );
                    }
                }

                // Move to the next Column
                i += textWidth;

                // Check for the current height
                if (i > nextHeightChange.X)
                {
                    i = nextHeightChange.X;
                    currentHeight = nextHeightChange.Y;
                    heightMapIterator++;
                    if (mMasterHeights.Length > heightMapIterator)
                        nextHeightChange = mMasterHeights[heightMapIterator];
                    else
                        break;
                }
            }

        }


        /// <summary>
        /// Given a location X, return the height of the terrain at that location.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetHeightOfX(float x)
        {
            // Find the height that we stand on
            for (int i = 0; i < mMasterHeights.Length - 1; i++)
            {
                if (x > mMasterHeights[i].X && x <= mMasterHeights[i + 1].X)
                {
                    return mMasterHeights[i].Y;
                }
            }

            return mMasterHeights[mMasterHeights.Length - 1].Y;
        }


        public void Update(GameTime gameTime)
        {
            //OffsetPosition.X -= SCROLL_SPEED * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 v in mMapping)
            {                
                spriteBatch.Draw(
                    mTexture,
                    v + OffsetPosition, // this line can stretch the texture
                    Color.White);
            }
            foreach (Vector2 v in mCutOffMapping)
            {
                Rectangle rect = mCutOffMappingSources[mCutOffMapping.IndexOf(v)]; // REVISIT optimize!
                spriteBatch.Draw(
                    mTexture,
                    v + OffsetPosition,
                    rect, // this line can stretch the texture
                    Color.White);
            }
        }

           
    }
}
