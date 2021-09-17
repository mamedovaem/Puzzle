using SFML.Graphics;
using System;

namespace Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("= Rules =\n\nYou can move through tiles with arrow keys.");
            Console.WriteLine("\nYou can select or disselect the tile with enter.");
            Console.WriteLine("\nYou can choose only the red, orange and yellow tiles and go only to the black spaces.");
            Console.WriteLine("\nYou should align the tiles according to colors above the playing field.");

            RenderWindow window = new(new(450, 400), "SFML.NET");
            window.SetFramerateLimit(60);

            Game game = new Game(window);
            game.Run();

            if (game.CheckForWin())
                Console.WriteLine("\n\n*** Congratulations! ***");

        }
    }
}
