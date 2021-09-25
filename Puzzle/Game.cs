using SFML.Graphics;
using SFML.Window;

namespace Puzzle
{
    class Game
    {
        private RenderWindow _window;
        private Field _field;
        private int _currentRow;
        private int _currentCol;
        public bool IsFinished;

        public Game(RenderWindow window)
        {
            _window = window;
            _field = new Field();

            _currentRow = 0;
            _currentCol = 0;

            IsFinished = false;

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
                            _field.SelectCell(_currentRow, _currentCol);
                            break;
                        default:
                            if (!_field.GetCell(_currentRow, _currentCol).IsSelected)
                                MoveSelection(e.Code);
                            else
                                MoveTile(e.Code);
                            break;
                    }

                };

        }

        public void Run()
        {
            while (_window.IsOpen && !IsFinished)
            {
                _window.DispatchEvents();
                _window.Clear();

                CheckForWin();
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
                    _window.Draw(_field.GetCell(i, j));

            DrawSelection();
        }


        public void DrawSelection()
        {
            RectangleShape selection = new Cell(CellType.EmptyCell, _currentRow, _currentCol)
            {
                OutlineColor = Color.White,
                FillColor = Color.Transparent,
                OutlineThickness = _field.GetCell(_currentRow, _currentCol).CurrentOutline
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

        public void MoveTile(Keyboard.Key key)
        {
            int newRow = _currentRow;
            int newCol = _currentCol;

            switch (key)
            {
                case Keyboard.Key.Up:
                    if (_currentRow != 0 &&
                        _field.GetCell(newRow - 1, newCol).Type == CellType.EmptyCell)
                    {
                        newRow--;

                    }
                    break;

                case Keyboard.Key.Down:
                    if (_currentRow != Field.MapSize - 1 &&
                        _field.GetCell(newRow + 1, newCol).Type == CellType.EmptyCell)
                    {
                        newRow++;

                    }
                    break;

                case Keyboard.Key.Left:
                    if (_currentCol != 0 &&
                        _field.GetCell(newRow, newCol - 1).Type == CellType.EmptyCell)
                    {
                        newCol--;

                    }
                    break;

                case Keyboard.Key.Right:
                    if (_currentCol != Field.MapSize - 1 &&
                        _field.GetCell(newRow, newCol + 1).Type == CellType.EmptyCell)
                    {
                        newCol++;

                    }

                    break;
            }

            _field.UpdateMap(_currentRow, _currentCol, newRow, newCol);

            _currentRow = newRow;
            _currentCol = newCol;
        }

        public void CheckForWin()
        {
            for (int i = 0; i < Field.MapSize; i++)
            {
                if (_field.GetCell(i, 0).FillColor != _field.LeftColumn.FillColor ||
                   _field.GetCell(i, 2).FillColor != _field.CenterColumn.FillColor ||
                   _field.GetCell(i, 4).FillColor != _field.RightColumn.FillColor)
                {
                    return;
                }
            }

            IsFinished = true;
        }

    }
}
