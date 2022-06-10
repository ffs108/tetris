namespace Tetris
{
    public class GameState
    {
        private Block cur;
        public Block Current
        {
            get => cur;
            private set {cur = value; cur.reset();}
        }

        public GameGrid GameGrid{get;}
        public BlockQueue BlockQueue{get;}
        public bool gameOver {get; private set;}


        public GameState()
        {
            GameGrid = new GameGrid(22,10);
            BlockQueue = new BlockQueue();
            Current = BlockQueue.getAndUpdate();
        }

        private bool BlockFits()
        {
            foreach(Position coor in Current.tilePositions())
            {
                if(!GameGrid.isEmpty(coor.Row, coor.Col))
                {
                    return false;
                }
            }
            return true;
        }

        public void rotateBlockRight()
        {
            Current.rotateRight();
            if(!BlockFits())
            {
                Current.rotateLeft();
            }
        }
        public void rotateBlockLeft()
        {
            Current.rotateLeft();
            if(!BlockFits())
            {
                Current.rotateRight();
            }
        }

        public void moveLeft()
        {
            Current.move(0,-1);
            if(!BlockFits())
            {
                Current.move(0,1);
            }
        }
        public void moveRight()
        {
            Current.move(0,1);
            if(!BlockFits())
            {
                Current.move(0,-1);
            }
        }        
        
        private bool isGameOver()
        {
            return !(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1));
        }

        private void placeBlock()
        {
            foreach(Position coor in Current.tilePositions())
            {
                GameGrid[coor.Row, coor.Col] = Current.Id;
            }
            GameGrid.clearFullRows();
            if(isGameOver())
            {
                gameOver = true;
            }
            else
            {
                Current = BlockQueue.getAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            Current.move(1,0);
            if(!BlockFits())
            {
                Current.move(-1,0);
                placeBlock();
            }
        }
    }
}