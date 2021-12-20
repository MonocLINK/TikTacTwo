public class Piece
{
    public char Symbol { get; private set; }
    private static List<char> SymbolList = new List<char>();

    public Piece()
    {
        Symbol = GetSymbol();
    }

    private char GetSymbol()
    {
        Console.Write("Enter symbol: ");
        string? symbol = Console.ReadLine();

        // symbol has value, only 1 value, and isn't already used
        if (symbol != null && symbol.Length == 1 && !SymbolList.Contains(symbol[0]))
        {
            // return first char of symbol and add it to the list
            SymbolList.Add(symbol[0]);
            return symbol[0];
        }

        // invalid input
        Error.InputError();
        return GetSymbol();
    }
}