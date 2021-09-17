using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Puzzle
{
    class Game
    {
        private RenderWindow _window;
        private Field _field;
        private int _currentRow;
        private int _currentCol;

        public Game(RenderWindow window)
        {
            _window = window;
            _field = new Field();

            _currentRow = 0;
            _currentCol = 0;

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
                            _field.Map[_currentRow, _currentCol].SelectCell();
                            break;
                        default:
                            if (!_field.Map[_currentRow, _currentCol].IsSelected)
                                MoveSelection(e.Code);
                            else
                                MoveTile(e.Code);
                            break;
                    }

                };

        }

        public void Run()
        {
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
            _window.Draw(_field.LeftColumn);
            _window.Draw(_field.CenterColumn);
            _window.Draw(_field.RightColumn);

            for (int i = 0; i < Field.MapSize; i++)
                for (int j = 0; j < Field.MapSize; j++)
                    _window.Draw(_field.Map[i, j]);

            DrawSelection();
        }


        public void DrawSelection()
        {
            RectangleShape selection = new Cell(CellType.EmptyCell, _currentRow, _currentCol)
            {
                OutlineColor = Color.White,
                FillColor = Color.Transparent,
                OutlineThickness = _field.Map[_currentRow, _currentCol].CurrentOutline
            };

            _window.Draw(selection);
        }
        public void MoveSelection(Keyboard.Key key)
        {
            switch (key)
            {
                case Keyboard.Key.Up:
                    if (_currentRow != 0)
                    {
                        _currentRow--;

                    }
                    break;

                case Keyboard.Key.Down:
                    if (_currentRow != Field.MapSize - 1)
                    {
                        _currentRow++;

                    }
                    break;

                case Keyboard.Key.Left:
                    if (_currentCol != 0)
                    {
                        _currentCol--;

                    }
                    break;

                case Keyboard.Key.Right:
                    if (_currentCol != Field.MapSize - 1)
                    {
                        _currentCol++;

                    }
                    break;

            }
        }

        public bool CheckPossibleMove(int row, int col)
        {
            return (_field.Map[row, col].Type == CellType.EmptyCell);
        }

        public void MoveTile(Keyboard.Key key)
        {
            int newRow = _currentRow;
            int newCol = _currentCol;

            switch (key)
            {
                case Keyboard.Key.Up:
                    if (_currentRow != 0 && CheckPossibleMove(newRow - 1, newCol))
                    {
                        newRow--;

                    }
                    break;

                case Keyboard.Key.Down:
                    if (_currentRow != Field.MapSize - 1 && CheckPossibleMove(newRow + 1, newCol))
                    {
                        newRow++;

                    }
                    break;

                case Keyboard.Key.Left:
                    if (_currentCol != 0 && CheckPossibleMove(newRow, newCol - 1))
                    {
                        newCol--;

                    }
                    break;

                case Keyboard.Key.Right:
                    if (_currentCol != Field.MapSize - 1 && CheckPossibleMove(newRow, newCol + 1))
                    {
                        newCol++;

                    }

                    break;
            }

            Cell temp = _field.Map[_currentRow, _currentCol];
            Vector2f temp_pos = _field.Map[newRow, newCol].Position;

            _field.Map[_currentRow, _currentCol] = _field.Map[newRow, newCol];
            _field.Map[_currentRow, _currentCol].Position = temp.Position;

            _field.Map[newRow, newCol] = temp;
            _field.Map[newRow, newCol].Position = temp_pos;

            _currentRow = newRow;
            _currentCol = newCol;

        }

        public bool CheckForWin()
        {
            for (int i = 0; i < Field.MapSize; i++)
            {
                if (_field.Map[i, 0].FillColor != _field.LeftColumn.FillColor ||
                   _field.Map[i, 2].FillColor != _field.CenterColumn.FillColor ||
                   _field.Map[i, 4].FillColor != _field.RightColumn.FillColor)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
