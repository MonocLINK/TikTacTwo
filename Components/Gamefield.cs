public class Gamefield
{
    public int Size { get; private set; }

    public List<List<string>> Board { get; set; }

    public Gamefield()
    {
        Size = GetSize();
        Board = new List<List<string>>();
        GenerateBoard();
    }

    private int GetSize()
    {
        // input
        Console.Write("\nHow big will the board be? > ");
        string? inp = Console.ReadLine();

        // convert to int, at least 3x3
        if (inp != null && inp.Any(char.IsDigit))
        {
            int inpInt = Int32.Parse(inp);

            // between min and max size
            if (inpInt >= 3 && inpInt <= GameController.MAX_SIZE)
                return Int32.Parse(inp);
        }

        // invalid input
        Error.InputError();
        return GetSize();
    }

    private void GenerateBoard()
    {
        // add size number of rows
        for (int i = 0; i < Size + 1; i++)
        {
            List<string> row = new List<string>();

            for (int j = 0; j < Size + 1; j++)
            {
                // creates row of " _ "
                row.Add(" _ ");

                // add row to board if i on last iteration
                if (j == Size - 1)
                {
                    Board.Add(row);
                }
            }
        }

        // set number rows
        for (int i = 0; i <= Size; i++)
        {
            Board[0][i] = " " + i + " ";
        }

        // set number cols
        for (int i = 0; i <= Size; i++)
        {
            Board[i][0] = " " + i + " ";
        }

        // top corner blank
        Board[0][0] = "   ";
    }

    public void ShowBoard()
    {
        foreach (var row in Board)
        {
            foreach (var spot in row)
            {
                Console.Write(spot);
            }
            Console.WriteLine();
        }
    }

    public Player? PlaceMove(Player player, int[] move)
    {
        // place move
        Board[move[1]][move[0]] = " " + player.Piece.Symbol + " ";

        // check if move wins game
        if (IsGameWon(player.Piece.Symbol))
            return player;

        // no player won
        return null;
    }

    private bool IsGameWon(char symbol)
    {
        int numInRow = 0;
        int numInCol = 0;
        int numInDiag = 0;

        // check rows
        for (int i = 1; i <= Size; i++)
        {
            for (int j = 1; j <= Size; j++)
            {
                // break out of this row if a symbol wasn't found
                if (!Board[i][j].Contains(symbol))
                    break;
                else
                    numInRow++;
            }
            // player wins if there is "Size" in a row
            if (numInRow == Size)
                return true;

            // reset for next row
            numInRow = 0;
        }

        // check cols
        for (int i = 1; i <= Size; i++)
        {
            for (int j = 1; j <= Size; j++)
            {
                // break out of this row if a symbol wasn't found
                if (!Board[j][i].Contains(symbol))
                    break;
                else
                    numInCol++;
            }
            // player wins if there is "Size" in a row
            if (numInCol == Size)
                return true;

            // reset for next row
            numInCol = 0;
        }

        // check TL -> BR diag
        for (int i = 1; i <= Size; i++)
        {
            // break out of this row if a symbol wasn't found
            if (!Board[i][i].Contains(symbol))
            {
                break;
            }
            else
                numInDiag++;

            // player wins if there is "Size" in a row
            if (numInDiag == Size)
                return true;
        }
        // reset for next diag
        numInDiag = 0;

        // check BR -> TL diag
        for (int i = Size; i >= 0; i--)
        {
            // break out of this row if a symbol wasn't found
            if (!Board[i][i].Contains(symbol))
                break;
            else
                numInDiag++;

            // player wins if there is "Size" in a row
            if (numInDiag == Size)
                return true;
        }
        // reset for next diag
        numInDiag = 0;

        // no winner
        return false;
    }
}
