// A delegate holds a METHOD: "any method that takes a string and returns nothing".
using CybersecurityChatbotGUI;

public delegate void BotMessageHandler(string message);

public class Chatbot
{
    private readonly UserProfile _userProfile = new UserProfile();
    private readonly ResponseHandler _responseHandler = new ResponseHandler();
    private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();
    private readonly AsciiArt _asciiArt = new AsciiArt();
    private readonly KeywordResponder _keywordResponder = new KeywordResponder();
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
            // No name stored yet, so this first message is the name.
            if (_userProfile.Name == "")
            {
                _userProfile.Name = trimmedInput;
                Say($"Nice to meet you, {_userProfile.Name}!");
                Say("You can ask me about:\n  1. Password Safety\n  2. Phishing\n  3. Safe Browsing\n  4. My Purpose");
                return;
            }

        // 1. Did the user mention a topic we know? Check this FIRST so
        //    "help me with my password" gives password advice, not the menu.
        string topic = _keywordResponder.DetectTopic(trimmedInput);

        if (topic != "")
        {
            _userProfile.CurrentTopic = topic; // remember it for follow-ups
            Say(_keywordResponder.GetRandomResponse(topic));
            return;
        }

        // 2. No new topic. Is it a follow-up like "tell me more"?
        if (_keywordResponder.IsFollowUp(trimmedInput))
        {
            if (_userProfile.CurrentTopic != "")
            {
                // Continue the topic we were already discussing.
                Say(_keywordResponder.GetRandomResponse(_userProfile.CurrentTopic));
            }
            else
            {
                Say("Happy to go deeper! Ask me about passwords, phishing, scams, privacy, malware or safe browsing first.");
            }
            return;
        }

        // 3. Otherwise use the Part 1 responses (menu, "how are you", etc).
        Say(_responseHandler.GetResponse(trimmedInput, _userProfile.Name));
    }   // <- the closing brace of ProcessInput, already there
}       // <- the closing brace of the class, already there