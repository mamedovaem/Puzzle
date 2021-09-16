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
            
            Game game = new Game();
            game.Run();
            
            if(game.CheckForWin())
                Console.WriteLine("\n\n*** Congratulations! ***");

        }
    }
}
