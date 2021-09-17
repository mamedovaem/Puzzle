using SFML.Graphics;
using SFML.System;

namespace Puzzle
{
    public enum CellType
    {
        Tile,
        BlockedCell,
        EmptyCell
    }

    public class Cell : RectangleShape

    {
        public static int CellSize = 50;
        public static int OffsetRow = 150;
        public static int OffsetCol = 100;

        public static Color Orange = new Color(255, 127, 0);
        public static Color Gray = new Color(127, 127, 127);

        private static int SelectedOutline = 3;
        private static int SimpleOutline = 1;
        public int CurrentOutline;

        public CellType Type;
        public bool IsSelected;

        public Cell(CellType type = CellType.EmptyCell, int row = 0, int col = 0)
        {
            Type = type;
            CurrentOutline = SimpleOutline;
            IsSelected = false;

            Size = new Vector2f(CellSize, CellSize);
            Position = new Vector2f(col * CellSize + OffsetCol, row * CellSize + OffsetRow);


        }

        public void SelectCell()
        {
            if (Type == CellType.Tile)
                if (!IsSelected)
                {
                    CurrentOutline = SelectedOutline;
                    IsSelected = true;
                }
                else
                {
                    CurrentOutline = SimpleOutline;
                    IsSelected = false;
                }
        }
    }
}


