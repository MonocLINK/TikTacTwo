public class Error
{
    public static bool isProperInput(Type inputType, Type properType)
    {
        if (inputType == properType)
            return true;

        return false;
    }

    public static void InputError() { System.Console.WriteLine("\nInvalid Input\n"); }
}
