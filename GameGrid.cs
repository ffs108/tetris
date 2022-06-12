using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tetris
{
    /*
        This is the bulk of references that are found else where, the 2D array that is responsible for 
        "holding" pieces. The actual implementation is that piece objects, tied to Position objects, dictate
        what "cells" of the 2d array they occupy. This also checks whether a row is filled, is empty, moves
        rows down after a clear and actually clears them as well.
    */
    public class GameGrid
    {
        private int[,] grid;
        public int Rows { get; }
        public int Cols { get; }
        public int this[int row, int col]
        {
            get => grid[row, col];
            set => grid[row, col] = value;
        }

        public GameGrid(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            grid = new int[rows, cols];
        }

        public bool isInside(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Cols;
        }

        public bool isEmpty(int row, int col)
        {
            return isInside(row, col) && grid[row, col] == 0;
        }

        public bool isRowFull(int row)
        {
            for (int col = 0; col < Cols; col++)
            {
                if (grid[row, col] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isRowEmpty(int row)
        {
            for (int col = 0; col < Cols; col++)
            {
                if (grid[row, col] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void clearRow(int row)
        {
            for (int col = 0; col < Cols; col++)
            {
                grid[row, col] = 0;
            }
        }
        private void moveRow(int row, int numR)
        {
            for (int col = 0; col < Cols; col++)
            {
                grid[row + numR, col] = grid[row, col];
            }
        }
        public int clearFullRows()
        {
            int cleared = 0;
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (isRowFull(row))
                {
                    clearRow(row);
                    cleared += 1;
                }
                else if (cleared > 0)
                {
                    moveRow(row, cleared);
                }
            }
            return cleared;
        }
    }
}