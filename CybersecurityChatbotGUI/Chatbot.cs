using CybersecurityChatbotGUI;

// A delegate holds a METHOD: "any method that takes a string and returns nothing".
public delegate void BotMessageHandler(string message);

public class Chatbot
{
    private readonly UserProfile _userProfile = new UserProfile();
    private readonly ResponseHandler _responseHandler = new ResponseHandler();
    private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();
    private readonly AsciiArt _asciiArt = new AsciiArt();
    private readonly KeywordResponder _keywordResponder = new KeywordResponder();
    private readonly SentimentDetector _sentimentDetector = new SentimentDetector();

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
        Say("Hello! Welcome to the Cybersecurity Awareness Bot (v2).");
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

        // Does the user want to know what the bot remembers?
        if (IsMemoryQuestion(trimmedInput))
        {
            ShowMemory();
            return;
        }

        string topic = _keywordResponder.DetectTopic(trimmedInput);
        Sentiment sentiment = _sentimentDetector.Detect(trimmedInput);
        bool isFollowUp = _keywordResponder.IsFollowUp(trimmedInput);

        // Follow-up with no new topic: continue the current one.
        if (topic == "" && isFollowUp)
        {
            if (_userProfile.CurrentTopic == "")
            {
                Say("Happy to go deeper! Ask me about passwords, phishing, scams, privacy, malware or safe browsing first.");
                return;
            }

            topic = _userProfile.CurrentTopic;
        }

        // A feeling but no topic: use the current topic, or pick one,
        // so the user always gets a tip without asking again.
        if (topic == "" && sentiment != Sentiment.Neutral)
        {
            topic = _userProfile.CurrentTopic != ""
                ? _userProfile.CurrentTopic
                : _keywordResponder.GetRandomTopic();
        }

        // Nothing recognised: fall back to the Part 1 responses.
        if (topic == "")
        {
            Say(_responseHandler.GetResponse(trimmedInput, _userProfile.Name));
            return;
        }

        _userProfile.CurrentTopic = topic;

        // Build ONE reply: [mood opening] + [memory line] + [the tip].
        string reply = _keywordResponder.GetRandomResponse(topic);

        bool remembered = TryRememberInterest(trimmedInput, topic);
        if (remembered)
        {
            reply = $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online. " + reply;
        }

        string opening = _sentimentDetector.GetOpening(sentiment, _userProfile.Name);
        if (opening != "")
        {
            reply = opening + " " + reply;
        }

        Say(reply);

        // Use the remembered interest to personalise later replies.
        if (!remembered && _userProfile.FavouriteTopic != "")
        {
            if (_userProfile.FavouriteTopic == topic)
            {
                Say($"As someone interested in {topic}, {_userProfile.Name}, this one is especially worth acting on.");
            }
            else
            {
                Say($"Since you're interested in {_userProfile.FavouriteTopic}, {_userProfile.Name}, ask me for a {_userProfile.FavouriteTopic} tip any time.");
            }
        }
    }

    // Looks for phrases like "I'm interested in privacy" and stores the topic.
    private bool TryRememberInterest(string input, string topic)
    {
        string text = input.ToLower();
        string[] interestPhrases = { "interested in", "my favourite", "my favorite", "i like", "i love", "i care about" };

        foreach (string phrase in interestPhrases)
        {
            if (text.Contains(phrase))
            {
                _userProfile.FavouriteTopic = topic;
                return true;
            }
        }

        return false;
    }

    // True when the user asks what the bot knows about them.
    private bool IsMemoryQuestion(string input)
    {
        string text = input.ToLower();

        return text.Contains("what do you remember")
            || text.Contains("do you remember")
            || text.Contains("what do you know about me")
            || text.Contains("what is my name")
            || text.Contains("what's my name")
            || text.Contains("what am i interested in");
    }

    // Recalls everything stored in the user's profile.
    private void ShowMemory()
    {
        Say($"Your name is {_userProfile.Name}.");

        if (_userProfile.FavouriteTopic != "")
        {
            Say($"You told me you're interested in {_userProfile.FavouriteTopic}.");
        }
        else
        {
            Say("You haven't told me a favourite topic yet. Try saying 'I'm interested in privacy'.");
        }

        if (_userProfile.CurrentTopic != "")
        {
            Say($"We were last talking about {_userProfile.CurrentTopic}.");
        }
    }
}