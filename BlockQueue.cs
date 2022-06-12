using System;

namespace Tetris
{
    /*
    This is just a randomized queue(not in the data structure sense) of Block objects that will
    be used to generate whats up next piece wise
    */
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(), new JBlock(), new LBlock(),
            new OBlock(), new SBlock(), new TBlock(),
            new ZBlock()
        };

        private readonly Random rand = new Random();
        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = randomBlock();
        }
        private Block randomBlock()
        {
            return blocks[rand.Next(blocks.Length)];
        }

        public Block getAndUpdate()
        {
            Block option = NextBlock;
            do
            {
                NextBlock = randomBlock();
            }
            while (option.Id == NextBlock.Id);
            return option;
        }
    }
}