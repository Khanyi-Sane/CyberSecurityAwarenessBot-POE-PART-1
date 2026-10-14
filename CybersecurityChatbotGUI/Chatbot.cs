// A delegate holds a METHOD: "any method that takes a string and returns nothing".
public delegate void BotMessageHandler(string message);

public class Chatbot
{
    private readonly UserProfile _userProfile = new UserProfile();
    private readonly ResponseHandler _responseHandler = new ResponseHandler();
    private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();
    private readonly AsciiArt _asciiArt = new AsciiArt();

    // The form assigns its display method to this.
    public BotMessageHandler? MessageHandler;

    // Read-only property that hands the logo text to the form.
    public string Logo => _asciiArt.GetLogo();

    // The bot "speaks" through the delegate and never touches the form directly.
    private void Say(string message)
    {
        MessageHandler?.Invoke(message);
    }

    public void Start()
    {
        _voiceGreeting.PlayGreeting();
        Say("Hello! Welcome to the Cybersecurity Awareness Bot.");
        Say("Before we begin, what is your name?");
    }

    // The form calls this each time the user sends a message.
    public void ProcessInput(string input)
    {
        string trimmedInput = input.Trim();

        if (string.IsNullOrWhiteSpace(trimmedInput))
        {
            Say("Please type a question in order for me to assist you.");
            return;
        }

        // No name stored yet, so this first message is the name.
        if (_userProfile.Name == "")
        {
            _userProfile.Name = trimmedInput;
            Say($"Nice to meet you, {_userProfile.Name}!");
            Say("You can ask me about:\n  1. Password Safety\n  2. Phishing\n  3. Safe Browsing\n  4. My Purpose");
            return;
        }

        // Reuse the Part 1 responses for now.
        Say(_responseHandler.GetResponse(trimmedInput, _userProfile.Name));
    }
}