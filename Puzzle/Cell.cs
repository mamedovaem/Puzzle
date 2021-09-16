using SFML.Graphics;
using SFML.System;

namespace Puzzle
{
    public enum CellType { redChip, orangeChip, yellowChip, blockedCell, emptyCell }

    public class Cell : RectangleShape

    {
        public static int size = 50;
        public static int offsetRow = 150;
        public static int offsetCol = 100;

        static Color Orange = new Color(255, 127, 0);
        static Color Gray = new Color(127, 127, 127);

        public static int selectedOutline = 3;
        public static int simpleOutline = 1;
        public int currOutline;

        public CellType type;
        public bool isSelected;

        public Cell(CellType type = CellType.emptyCell, int row = 0, int col = 0)
        {
            this.type = type;
            currOutline = simpleOutline;
            isSelected = false;

            Size = new Vector2f(size, size);
            Position = new Vector2f(col * size + offsetCol, row * size + offsetRow);

            switch (type)
            {
                case CellType.redChip:
                    this.FillColor = Color.Red;
                    break;
                case CellType.orangeChip:
                    this.FillColor = Orange;
                    break;
                case CellType.yellowChip:
                    this.FillColor = Color.Yellow;
                    break;
                case CellType.blockedCell:
                    this.FillColor = Gray;
                    break;
                case CellType.emptyCell:
                    this.FillColor = Color.Black;
                    break;

            }
        }

        public void SelectCell()
        {

            switch (type)
            {
                case CellType.redChip:
                case CellType.orangeChip:
                case CellType.yellowChip:

                    if (!isSelected)
                    {
                        currOutline = selectedOutline;
                        isSelected = true;
                    }
                    else
                    {
                        currOutline = simpleOutline;
                        isSelected = false;
                    }
                    break;
            }


        }
    }
}

