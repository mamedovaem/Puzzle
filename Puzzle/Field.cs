using SFML.Graphics;
using SFML.System;

namespace Puzzle
{
    class Field
    {
        public const int MapSize = 5;
        private Cell[,] _map;

        public Cell LeftColumn;
        public Cell CenterColumn;
        public Cell RightColumn;

        public Field()
        {
            _map = new Cell[MapSize, MapSize];
            InitMap();

            LeftColumn = new Cell(CellType.Tile, -2, 0)
            {
                FillColor = Color.Yellow
            };
            CenterColumn = new Cell(CellType.Tile, -2, 2)
            {
                FillColor = Cell.Orange
            };
            RightColumn = new Cell(CellType.Tile, -2, 4)
            {
                FillColor = Color.Red
            };
        }

        void InitMap()
        {

            string charMap = "O B Y B O " + // R - red; O - orange; Y - yellow; B - block; E - empty; 
                             "R E Y E O " +
                             "Y B R B R " +
                             "O E O E Y " +
                             "R B R B Y ";

            charMap = charMap.Replace(" ", "");

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    switch (charMap[i * MapSize + j])
                    {
                        case 'R':
                            _map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Color.Red
                            };
                            break;
                        case 'O':
                            _map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Cell.Orange
                            };
                            break;
                        case 'Y':
                            _map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Color.Yellow
                            };
                            break;
                        case 'B':
                            _map[i, j] = new Cell(CellType.BlockedCell, i, j)
                            {
                                FillColor = Cell.Gray
                            };
                            break;
                        case 'E':
                            _map[i, j] = new Cell(CellType.EmptyCell, i, j)
                            {
                                FillColor = Color.Black
                            };
                            break;

                    }
                }
            }
        }

        public void UpdateMap(int currentRow, int currentCol, int newRow, int newCol)
        {
            Cell temp = _map[currentRow, currentCol];
            Vector2f temp_pos = _map[newRow, newCol].Position;

            _map[currentRow, currentCol] = _map[newRow, newCol];
            _map[currentRow, currentCol].Position = temp.Position;

            _map[newRow, newCol] = temp;
            _map[newRow, newCol].Position = temp_pos;
        }

        public Cell GetCell(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < MapSize && j < MapSize)
                return _map[i, j];
            else
                return _map[0, 0];
        }

        public void SelectCell(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < MapSize && j < MapSize)
                _map[i, j].Select();
            else
                _map[0, 0].Select();
        }
    }
}
