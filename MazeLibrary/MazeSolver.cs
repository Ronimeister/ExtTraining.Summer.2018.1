using System;

namespace MazeLibrary
{
    public class MazeSolver
    {
        #region Properties
        private int[,] _map;
        private int _startX;
        private int _startY;
        private int _mapWidth;
        private int _mapHeight;
        private int _finishX;
        private int _finishY;
        private bool _isFinished;
        #endregion

        #region Public API
        /// <summary>
        /// Constructor for <see cref="MazeSolver"/> class
        /// </summary>
        /// <param name="mazeModel"><see cref="int[,]"/> map</param>
        /// <param name="startX">Start position X-coord.</param>
        /// <param name="startY">Start position Y-coord.</param>
        /// <exception cref="ArgumentException">Throw when <paramref name="startX"/> or <paramref name="startY"/> is less than 0 or when start position is equal to -1</exception>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="mazeModel"/> is equal to null or one of indexes <paramref name="startX"/> or <paramref name="startY"/> is out of range</exception>
        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            if (mazeModel is null)
            {
                throw new ArgumentNullException($"{nameof(mazeModel)} can't be equal to null!");
            }

            if (startX < 0 || startY < 0)
            {
                throw new ArgumentException($"Index can't be less the 0!");
            }

            if (startX > mazeModel.GetLength(0) || startY > mazeModel.GetLength(1))
            {
                throw new ArgumentOutOfRangeException($"Index is out of range!");
            }

            if (mazeModel[startX, startY] == -1)
            {
                throw new ArgumentException($"This position of {nameof(mazeModel)} can't be equal to -1!");
            }

            _map = mazeModel;
            _startX = startX;
            _startY = startY;
            _mapHeight = mazeModel.GetLength(0);
            _mapWidth = mazeModel.GetLength(1);
        }

        /// <summary>
        /// Method that return current state of map
        /// </summary>
        /// <returns>Current state of map</returns>
        public int[,] MazeWithPass() => _map;

        /// <summary>
        /// Method that finds the way from start point to the end point
        /// </summary>
        /// <exception cref="ArgumentException">Throw when <see cref="_map"/> hasn't got any exit points</exception>
        public void PassMaze()
        {
            if (!IsWithFinish())
            {
                throw new ArgumentException($"{nameof(_map)} hasn't got exit! Check the input data.");
            }

            (int X,int Y) = FindFinish(_map);
            _finishX = X;
            _finishY = Y;
            CompleteTheMap(1, _startX, _startY, 0);
        }
        #endregion

        #region Private API
        private bool IsWithFinish() => (CountFinishes(_map) == 2) ? true : false;

        private int CountFinishes(int[,] map)
        {
            int result = 0;

            for (int i = 0; i < _mapHeight; i++)
            {
                for (int j = 0; j < _mapWidth; j++)
                {
                    if (i == 0 || i == (_mapHeight - 1) || j == 0 || j == (_mapWidth - 1))
                    {
                        if (_map[i, j] == 0)
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        private (int, int) FindFinish(int[,] _map)
        {
            for (int i = 0; i < _mapHeight; i++)
            {
                for (int j = 0; j < _mapWidth; j++)
                {
                    if (i == 0 || i == (_mapHeight - 1) || j == 0 || j == (_mapWidth - 1))
                    {
                        if (_map[i, j] == 0)
                        {
                            if(i != _startX && j != _startY)
                            {
                                return (i, j);
                            }

                            if (i == _startX && j != _startY)
                            {
                                return (i, j);
                            }

                            if(j == _startY && i != _startX)
                            {
                                return (i, j);
                            }
                        }
                    }
                }
            }

            throw new ArgumentException($"{nameof(_map)} hasn't got any finish!");
        }

        private void CompleteTheMap(int prev, int x, int y, int step)
        {
            int incY = y + 1;
            int incX = x + 1;
            int decY = y - 1;
            int decX = x - 1;

            if (!_isFinished &&_map[x, y] == 0 && x < _mapHeight && y < _mapWidth)
            {
                step++;
                _map[x, y] = step;

                if (x == _finishX && y == _finishY)
                {
                    _isFinished = true;
                }

                if (incX < _mapHeight && _map[incX, y] == 0)
                {
                    CompleteTheMap(_map[x, y], incX, y, step);
                }

                if(incY < _mapWidth && _map[x, incY] == 0)
                {
                    CompleteTheMap(_map[x, y], x, incY, step);
                }

                if (decX >= 0 && _map[decX, y] == 0)
                {
                    CompleteTheMap(_map[x, y], decX, y, step);
                }

                if (decY >= 0 && _map[x, decY] == 0)
                {
                    CompleteTheMap(_map[x, y], x, decY, step);
                }

                if (!_isFinished)
                {
                    _map[x, y] = 0;
                }
            }
        }
        #endregion
    }
}