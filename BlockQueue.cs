using System;

namespace Tetris
{
    public class BlockQueue
    {
        private readonly Blockp[] blocks = new Block[]
        {
            new IBlock(), new JBlock(), new LBlock(),
            new OBlock(), new SBlock(), new TBlock(),
            new ZBlock()
        };

        private readonly Random rand = new Random();
        public Block NextBlock{get; private set;}

        public BlockQueue()
        {
            NextBlock = randomBlock();
        }
        private Block randomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }
        
        public Block getAndUpdate()
        {
            Block option = NextBlock;
            do
            {
                NextBlock = randomBlock();
            }
            while(option.Id == NextBlock.Id);
            return option;
        }
    }
}