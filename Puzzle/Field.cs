
namespace Puzzle
{
    class Field
    {
        public const int mapSize = 5;
        public Cell[,] map;

        public Cell leftColumn;
        public Cell centerColumn;
        public Cell rightColumn;

        public CellType leftColor;
        public CellType centerColor;
        public CellType rightColor;

        public Field()
        {
            map = new Cell[mapSize, mapSize];
            initMap();
            leftColor = CellType.yellowChip;
            centerColor = CellType.orangeChip;
            rightColor = CellType.redChip;

            leftColumn = new Cell(leftColor, -2, 0);
            centerColumn = new Cell(centerColor, -2, 2);
            rightColumn = new Cell(rightColor, -2, 4);


        }

        void initMap()
        {
            //
            string charMap = "O B Y B O " + // R - red; O - orange; Y - yellow; B - block; E - empty; 
                             "R E Y E O " +
                             "Y B R B R " +
                             "O E O E Y " +
                             "R B R B Y ";

            parseMap(charMap);
        }
        void parseMap(string charMap)
        {
            charMap = charMap.Replace(" ", "");

            for (int i = 0; i < mapSize; i++)
                for (int j = 0; j < mapSize; j++)
                {
                    switch (charMap[i * mapSize + j])
                    {
                        case 'R':
                            map[i, j] = new Cell(CellType.redChip, i, j);
                            break;
                        case 'O':
                            map[i, j] = new Cell(CellType.orangeChip, i, j);
                            break;
                        case 'Y':
                            map[i, j] = new Cell(CellType.yellowChip, i, j);
                            break;
                        case 'B':
                            map[i, j] = new Cell(CellType.blockedCell, i, j);
                            break;
                        case 'E':
                            map[i, j] = new Cell(CellType.emptyCell, i, j);
                            break;

                    }

                }
        }



    }
}
