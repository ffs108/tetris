using System.Collections.Generic;

namespace Tetris
{
    /*
        Parent class for every single tetrimino piece, has every method each class would need.
        it can handle movemnent once a particular instances of a child is create since it is
        all dependent on the position object and its place within the 2d array that is gamegrid
    */
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int Id { get; }
        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Col);
        }

        public IEnumerable<Position> tilePositions()
        {
            foreach (Position coor in Tiles[rotationState])
            {
                yield return new Position(coor.Row + offset.Row, coor.Col + offset.Col);
            }
        }

        public void rotateRight()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        public void rotateLeft()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState -= 1;
            }
        }

        public void move(int rows, int cols)
        {
            offset.Row += rows;
            offset.Col += cols;
        }

        public void reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Col = StartOffset.Col;
        }
    }
}