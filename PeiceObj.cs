using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RushHourTester
{
    internal class PeiceObj
    {    
        private Texture2D texture;
        private Vector2 gridPos;
        private Vector2[] occupiedSpaceOffset = new Vector2[3];
        private Vector2 absPos;
        private int length;

        public PeiceObj(Vector2 gridPos, int length)
        {
            this.gridPos = gridPos;
            this.length = length;
            switch (length)
            {
                case 1:
                    occupiedSpaceOffset[0] = Vector2.Zero;
                    occupiedSpaceOffset[1] = new Vector2(0, 1);
                    occupiedSpaceOffset[2] = new Vector2(-100, -100);
                    break;
                case 2:
                    occupiedSpaceOffset[0] = Vector2.Zero;
                    occupiedSpaceOffset[1] = new Vector2(1, 0);
                    occupiedSpaceOffset[2] = new Vector2(-100, -100);
                    break;
                case 3:
                    occupiedSpaceOffset[0] = Vector2.Zero;
                    occupiedSpaceOffset[1] = new Vector2(0, 1);
                    occupiedSpaceOffset[2] = new Vector2(0, 2);
                    break;
                case 4:
                    occupiedSpaceOffset[0] = Vector2.Zero;
                    occupiedSpaceOffset[1] = new Vector2(1, 0);
                    occupiedSpaceOffset[2] = new Vector2(2, 0);
                    break;
            }
        }
        public void LoadContent(Texture2D texture) 
        {
            this.texture = texture;
        }
        public int GetLength() 
        {
            return length;
        }
        public Vector2[] GetOccupiedSpace() 
        {
            Vector2[] returnValue = new Vector2[]
            {
                gridPos + occupiedSpaceOffset[0],
                gridPos + occupiedSpaceOffset[1],
                gridPos + occupiedSpaceOffset[2]
            };
            return returnValue;
        }
        public void ChangePosition(Vector2 change) 
        {
            gridPos += change;
        }
        public void draw(SpriteBatch spriteBatch) 
        {
            absPos = Vector2.Multiply(gridPos, 116) + new Vector2(50, 170);
            spriteBatch.Draw(texture, absPos, Color.White);
        }
    }
}
