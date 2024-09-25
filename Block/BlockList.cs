using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MasterGame.Block
{ 
    public class BlockList
    {

        private static BlockList instance;

        List<Sprite> blockList;
        int currentBlock;
        int firstBlock = 0;
        int lastBlock;

        public static BlockList Instance
        {
            get
            {
                if( instance == null)
                {
                    instance = new BlockList();
                }
                return instance; 
            }
        }

        public void setBlockList(List<Sprite> myBlocks)
        {
            blockList = myBlocks;
            currentBlock = firstBlock;
            lastBlock = myBlocks.Count - 1;
        }

        // if we aren't at the end of the list, increment. Otherwise, go to start. 
        public void viewNext()
        {
            if (currentBlock < lastBlock)
            {
                currentBlock++;
            }
            else
            {
                currentBlock = firstBlock;

            }
        }

        // if we aren't at the beginning of the list, decrement. Otherwise, got to end. 
        public void viewPrevious()
        {
            if (currentBlock > firstBlock)
            {
                currentBlock--;
            }
            else
            {
                currentBlock = lastBlock;
            }
        }

        // Draws the current block. 
        public void Draw(Vector2 location)
        {
            if (currentBlock >= 0 && currentBlock < blockList.Count)
            {
                blockList[currentBlock].Draw(location);
            }
        }

        public void Update()
        {
            // do nothing for most tile, animates waterfall tile.
            blockList[currentBlock].Update();

        }

    }

}
