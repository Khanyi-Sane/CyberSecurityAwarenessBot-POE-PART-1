// Chatbot.cs
// Controls the main conversation and connects the other chatbot classes.
// Handles the user's name, menu, conversation loop, and chatbot responses.

using System;
using System.Threading;

public class Chatbot
{
    // Creates the objects needed by the chatbot.
    private readonly UserProfile _userProfile = new UserProfile();
    private readonly ResponseHandler _responseHandler = new ResponseHandler();
    private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();
    private readonly AsciiArt _asciiArt = new AsciiArt();

    // Starts the chatbot conversation.
    public void Start()
    {
        // Play the voice greeting while the chatbot continues starting up.
        _voiceGreeting.PlayGreeting();

        // Display the ASCII logo.
        _asciiArt.DisplayLogo();

        Console.WriteLine();

        TypeLine("Bot > Hello! Welcome to the Cybersecurity Awareness Bot.", ConsoleColor.Cyan);
        TypeLine("Bot > Before we begin, what is your name?", ConsoleColor.Cyan);

        // Store the user's name in their profile.
        _userProfile.Name = ReadName();

        TypeLine($"Bot > Nice to meet you, {_userProfile.Name}!", ConsoleColor.Green);

        // Display the available cybersecurity topics as a numbered list.
        TypeLine("Bot > You can ask me about:", ConsoleColor.Cyan);
        TypeLine("      1. Password Safety", ConsoleColor.Cyan);
        TypeLine("      2. Phishing", ConsoleColor.Cyan);
        TypeLine("      3. Safe Browsing", ConsoleColor.Cyan);
        TypeLine("      4. My Purpose", ConsoleColor.Cyan);

        TypeLine("Bot > Type 'exit' or 'quit' when you want to end the conversation.", ConsoleColor.DarkYellow);

        // Start the main conversation loop.
        RunConversationLoop();
    }

    // Gets the user's name and makes sure it is not left blank.
    private string ReadName()
    {
        string name = "";

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write($"{Environment.NewLine}You > ");
            name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                TypeLine("Bot > Please enter your name.", ConsoleColor.DarkYellow);
            }
        }

        return name.Trim();
    }

    // Keeps the conversation running until the user chooses to exit.
    private void RunConversationLoop()
    {
        while (true)
        {
            Console.Write($"{Environment.NewLine}{_userProfile.Name} > ");
            string input = Console.ReadLine() ?? "";
            string trimmedInput = input.Trim();

            // Allow the user to end the conversation.
            if (trimmedInput.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                trimmedInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                TypeLine("Bot > Goodbye! Stay safe online.", ConsoleColor.Cyan);
                break;
            }

            // Handle blank input.
            if (string.IsNullOrWhiteSpace(trimmedInput))
            {
                TypeLine("Bot > Please type a question in order for me to assist you.", ConsoleColor.DarkYellow);
                continue;
            }

            // Send the user's question to the ResponseHandler.
            string response = _responseHandler.GetResponse(trimmedInput, _userProfile.Name);

            // Display the chatbot response using the typing effect.
            TypeLine($"Bot > {response}", ConsoleColor.Cyan);
        }
    }

    // Displays chatbot messages with a short typing effect.
    private void TypeLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        foreach (char character in message)
        {
            Console.Write(character);
            Thread.Sleep(20);
        }

        Console.WriteLine();
        Console.ResetColor();
    }
}
