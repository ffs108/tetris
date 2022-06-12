namespace Tetris
{
    /*
    keeps track of game events such as scores, when the gamegrid needs to be manipulated
    calculates when and where a dropped piece can result and when and if holds occur.
    */
    public class GameState
    {
        private Block cur;
        public Block Current
        {
            get => cur;
            private set 
            { 
                cur = value; 
                cur.reset();
                for (int i = 0; i < 2; i++)
                {
                    Current.move(1, 0);
                    if (!BlockFits())
                    {
                        Current.move(-1, 0);
                    }
                }
            }
        }


        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool gameOver { get; private set; }
        public int score { get; private set; }
        public Block held { get; private set; }
        public bool canHold { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            Current = BlockQueue.getAndUpdate();
            canHold = true;
        }

        private bool BlockFits()
        {
            foreach (Position coor in Current.tilePositions())
            {
                if (!GameGrid.isEmpty(coor.Row, coor.Col))
                {
                    return false;
                }
            }
            return true;
        }

        public void hold()
        {
            if (!canHold)
            {
                return;
            }
            if(held == null)
            {
                held = Current;
                Current = BlockQueue.getAndUpdate();
            }
            else
            {
                Block temp = Current;
                Current = held;
                held = temp;
            }
            canHold = false;
        }

        public void rotateBlockRight()
        {
            Current.rotateRight();
            if (!BlockFits())
            {
                Current.rotateLeft();
            }
        }
        public void rotateBlockLeft()
        {
            Current.rotateLeft();
            if (!BlockFits())
            {
                Current.rotateRight();
            }
        }

        public void moveLeft()
        {
            Current.move(0, -1);
            if (!BlockFits())
            {
                Current.move(0, 1);
            }
        }
        public void moveRight()
        {
            Current.move(0, 1);
            if (!BlockFits())
            {
                Current.move(0, -1);
            }
        }

        private bool isGameOver()
        {
            return !(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1));
        }

        private void placeBlock()
        {
            foreach (Position coor in Current.tilePositions())
            {
                GameGrid[coor.Row, coor.Col] = Current.Id;
            }
            score += 10 * GameGrid.clearFullRows();
            if (isGameOver())
            {
                gameOver = true;
            }
            else
            {
                Current = BlockQueue.getAndUpdate();
                canHold = true;
            }
        }

        public void moveBlockDown()
        {
            Current.move(1, 0);
            if (!BlockFits())
            {
                Current.move(-1, 0);
                placeBlock();
            }
        }

        private int tileDropDistance(Position pos)
        {
            int drop = 0;
            while(GameGrid.isEmpty(pos.Row + drop + 1, pos.Col))
            {
                drop++;
            }
            return drop;
        }

        public int blockDropDistance()
        {
            int drop = GameGrid.Rows;
            foreach (Position coor in Current.tilePositions())
            {
                drop = System.Math.Min(drop, tileDropDistance(coor));
            }
            return drop;
        }

        public void drop()
        {
            Current.move(blockDropDistance(), 0);
            placeBlock();
        }
    }
}