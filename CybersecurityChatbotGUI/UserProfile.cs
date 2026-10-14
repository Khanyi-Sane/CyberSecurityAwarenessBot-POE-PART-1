// UserProfile.cs
// Stores basic information about the person chatting to the bot.
// The Name property allows the chatbot to save and use the user's name.

public class UserProfile
{
    // Stores the user's name.
    public string Name { get; set; } = "";
    public string CurrentTopic { get; set; } = "";   // what we're talking about right now
    public string FavouriteTopic { get; set; } = ""; // used later for memory
}