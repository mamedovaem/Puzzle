using SFML.Graphics;

namespace Puzzle
{
    class Field
    {
        public const int MapSize = 5;
        public Cell[,] Map;

        public Cell LeftColumn;
        public Cell CenterColumn;
        public Cell RightColumn;

        public Field()
        {
            Map = new Cell[MapSize, MapSize];
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

            ParseMap(charMap);
        }

        void ParseMap(string charMap)
        {
            charMap = charMap.Replace(" ", "");

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    switch (charMap[i * MapSize + j])
                    {
                        case 'R':
                            Map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Color.Red
                            };
                            break;
                        case 'O':
                            Map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Cell.Orange
                            };
                            break;
                        case 'Y':
                            Map[i, j] = new Cell(CellType.Tile, i, j)
                            {
                                FillColor = Color.Yellow
                            };
                            break;
                        case 'B':
                            Map[i, j] = new Cell(CellType.BlockedCell, i, j)
                            {
                                FillColor = Cell.Gray
                            };
                            break;
                        case 'E':
                            Map[i, j] = new Cell(CellType.EmptyCell, i, j)
                            {
                                FillColor = Color.Black
                            };
                            break;

                    }
                }
            }
        }
    }
}
