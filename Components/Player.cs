using System.Text.RegularExpressions;

public class Player
{
    public string Name { get; private set; }
    public Piece Piece { get; private set; }

    public Player()
    {
        Name = GetPlayerName();
        Piece = new Piece();
    }

    public string GetPlayerName()
    {
        // input name
        Console.Write("Enter name: ");
        string? name = Console.ReadLine();

        // proper name input
        if (name != null && name.Length >= 1)
            return name;

        // invalid input
        Error.InputError();
        return GetPlayerName();
    }

    public int[] GetMoveLocation(Gamefield gamefield)
    {
        // input move
        Console.Write($"\n{Name}, Enter move x,y: ");
        string? loc = Console.ReadLine();

        // proper location input
        if (loc != null)
        {
            // must only be numbers
            loc = Regex.Replace(loc, "[^0-9]", "");

            int[] locInt = ConvertLocToIntArr(loc);
            if (IsProperLocation(locInt, gamefield))
                return locInt;
        }

        // invalid input
        Error.InputError();
        return GetMoveLocation(gamefield);
    }
    private int[] ConvertLocToIntArr(string loc)
    {
        // convert loc to int
        int[] locInt = new int[2];
        for (int i = 0; i < loc.Length; i++)
            locInt[i] = loc[i] - '0';

        return locInt;
    }

    private bool IsProperLocation(int[] loc, Gamefield gamefield)
    {
        // make sure only two numbers
        if (loc.Count() != 2)
            return false;

        // make sure location is within range
        foreach (var place in loc)
        {
            if (place > gamefield.Size)
                return false;
        }

        // make sure location on board is empty
        if (!gamefield.Board[loc[1]][loc[0]].Contains('_'))
            return false;

        // passed all tests
        return true;

    }
}