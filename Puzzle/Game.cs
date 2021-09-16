using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Puzzle
{
    class Game
    {
        RenderWindow _window;
        Field field;
        int currRow, currCol;

        public Game()
        {
            _window = new(new(450, 400), "SFML.NET");
            _window.SetFramerateLimit(60);

            field = new Field();

            currRow = 0;
            currCol = 0;
            DrawCurrCell();
        }

        public void Run()
        {
            //  key events

            _window.Closed += (obj, e) => { _window.Close(); };
            _window.KeyPressed +=
                (sender, e) =>
                {

                    var window = (Window)sender;
                    switch (e.Code)
                    {
                        case Keyboard.Key.Escape:
                            window.Close();
                            break;
                        case Keyboard.Key.Enter:

                            field.map[currRow, currCol].SelectCell();
                            break;

                        default:
                            //если клетка не выбрана

                            if (!field.map[currRow, currCol].isSelected)
                                MoveCell(e.Code);
                            else
                                //если выбрана
                                MakeMove(e.Code);
                            break;
                    }

                };

            while (_window.IsOpen && !CheckForWin())
            {
                _window.DispatchEvents();
                _window.Clear();
                DrawMap();

                _window.Display();
            }
        }

        public void DrawMap()
        {
            _window.Draw(field.leftColumn);
            _window.Draw(field.centerColumn);
            _window.Draw(field.rightColumn);

            for (int i = 0; i < Field.mapSize; i++)
                for (int j = 0; j < Field.mapSize; j++)
                    _window.Draw(field.map[i, j]);

            DrawCurrCell();
        }


        public void DrawCurrCell()
        {
            Vector2f cellSize = new Vector2f(Cell.size, Cell.size);
            RectangleShape currCell = new RectangleShape(cellSize);
            currCell.Position = new Vector2f(currCol * Cell.size + Cell.offsetCol, currRow * Cell.size + Cell.offsetRow);
            currCell.OutlineColor = Color.White;
            currCell.FillColor = Color.Transparent;
            currCell.OutlineThickness = field.map[currRow, currCol].currOutline;
            _window.Draw(currCell);
        }
        public void MoveCell(Keyboard.Key key)
        {


            switch (key)
            {
                case Keyboard.Key.Up:
                    if (currRow != 0)
                    {
                        currRow--;

                    }
                    break;

                case Keyboard.Key.Down:
                    if (currRow != Field.mapSize - 1)
                    {
                        currRow++;

                    }
                    break;

                case Keyboard.Key.Left:
                    if (currCol != 0)
                    {
                        currCol--;

                    }
                    break;

                case Keyboard.Key.Right:
                    if (currCol != Field.mapSize - 1)
                    {
                        currCol++;

                    }
                    break;

            }

            DrawCurrCell();
        }

        public bool CheckPossibleMove(int row, int col)
        {
            if (field.map[row, col].type == CellType.emptyCell &&
                (Math.Abs(row - currRow) + Math.Abs(col - currCol) < 2))
                return true;

            return false;
        }

        public void MakeMove(Keyboard.Key key)
        {
            int newRow = currRow;
            int newCol = currCol;

            switch (key)
            {
                case Keyboard.Key.Up:
                    if (currRow != 0 && CheckPossibleMove(newRow - 1, newCol))
                    {
                        newRow--;

                    }
                    break;

                case Keyboard.Key.Down:
                    if (currRow != Field.mapSize - 1 && CheckPossibleMove(newRow + 1, newCol))
                    {
                        newRow++;

                    }
                    break;

                case Keyboard.Key.Left:
                    if (currCol != 0 && CheckPossibleMove(newRow, newCol - 1))
                    {
                        newCol--;

                    }
                    break;

                case Keyboard.Key.Right:
                    if (currCol != Field.mapSize - 1 && CheckPossibleMove(newRow, newCol + 1))
                    {
                        newCol++;

                    }

                    break;

            }

            Cell temp = field.map[currRow, currCol];
            Vector2f temp_pos = field.map[newRow, newCol].Position;

            field.map[currRow, currCol] = field.map[newRow, newCol];
            field.map[currRow, currCol].Position = temp.Position;

            field.map[newRow, newCol] = temp;
            field.map[newRow, newCol].Position = temp_pos;

            currRow = newRow;
            currCol = newCol;

            DrawMap();


        }



        public bool CheckForWin()
        {
            bool hasWon = true;

            for (int i = 0; i < Field.mapSize; i++)
            {
                if (field.map[i, 0].type == field.leftColor &&
                   field.map[i, 2].type == field.centerColor &&
                   field.map[i, 4].type == field.rightColor)
                    continue;
                else
                {
                    hasWon = false;
                    break;
                }
            }
            return hasWon;
        }

    }
}
