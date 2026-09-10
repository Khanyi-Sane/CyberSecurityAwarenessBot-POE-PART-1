// AsciiArt.cs
// Displays a cybersecurity-themed ASCII logo when the bot starts.

using System;

public class AsciiArt
{
    // Displays the chatbot logo in the console.
    public void DisplayLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(@"   _____      _                _____ ");
        Console.WriteLine(@"  / ____|    | |              / ____|");
        Console.WriteLine(@" | |    _   _| |__   ___ _ __ | (___  ___  ___");
        Console.WriteLine(@" | |   | | | | '_ \ / _ \ '__| \___ \/ _ \/ __|");
        Console.WriteLine(@" | |___| |_| | |_) |  __/ |    ____) |  __/ (__");
        Console.WriteLine(@"  \_____\__, |_.__/ \___|_|   |_____/ \___|\___|");
        Console.WriteLine(@"         __/ |");
        Console.WriteLine(@"        |___/      A W A R E N E S S   B O T");
        Console.WriteLine();

        Console.WriteLine(@"            [ * ]  STAY SAFE ONLINE  [ * ]");

        Console.ResetColor();
    }
}