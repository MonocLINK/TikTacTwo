using System.Text.RegularExpressions;

public class GameController
{
    private static List<Player> players = new List<Player>();
    public static int MAX_SIZE = 10;
    public static void Play()
    {
        // Setup variables
        bool isGamePlaying = true;
        int currentPlayer = 0;
        Player? winner = null;

        // Generate Players
        AddPlayers(GetPlayers());

        // Generate Gamefield
        Gamefield gamefield = new Gamefield();

        // Start game loop
        while (isGamePlaying)
        {
            // display board
            gamefield.ShowBoard();

            // move for current player
            int[] move = players[currentPlayer].GetMoveLocation(gamefield);

            // place move on board and see if anyone won
            winner = gamefield.PlaceMove(players[currentPlayer], move);

            // check if player has won
            if (winner != null)
            {
                gamefield.ShowBoard();
                PlayerWon(winner);
                isGamePlaying = false;
            }

            // next player
            if (currentPlayer < players.Count - 1)
                currentPlayer++;
            else
                currentPlayer = 0;
        }
    }

    private static int GetPlayers()
    {
        // input
        Console.Write("How many will play? > ");
        string? inp = Console.ReadLine();

        // convert to int, at least 2 players
        if (inp != null)
        {
            // remove all non num
            inp = Regex.Replace(inp, "[^0-9]", "");

            // must contian digits
            if (inp.Any(char.IsDigit) && Int32.Parse(inp) >= 2)
            {
                return Int32.Parse(inp);
            }
        }

        Error.InputError();
        return GetPlayers();
    }

    private static void AddPlayers(int numPlayers)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            Console.WriteLine($"\nPlayer {i + 1}");
            players.Add(new Player());
        }
    }

    private static void PlayerWon(Player winner)
    {
        Console.WriteLine($"\nCongrats {winner.Name}!");
        Console.WriteLine("You won!");
    }
}