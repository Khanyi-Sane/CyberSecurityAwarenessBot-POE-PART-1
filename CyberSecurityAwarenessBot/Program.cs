// Program.cs
// Entry point for the Cybersecurity Awareness Bot.
// This file is kept deliberately small: it just creates the Chatbot
// object and starts the conversation. All real logic lives in the
// other classes (Chatbot, VoiceGreeting, AsciiArt, ResponseHandler, UserProfile).

class Program
{
    static void Main(string[] args)
    {
        Chatbot bot = new Chatbot();
        bot.Start();
    }
}