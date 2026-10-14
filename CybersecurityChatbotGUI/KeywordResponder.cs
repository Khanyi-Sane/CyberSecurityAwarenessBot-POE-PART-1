public class KeywordResponder
{
    // One Random object, reused every time (creating a new one each call is wasteful).
    private readonly Random _random = new Random();

    // Remembers the last reply so "tell me more" doesn't repeat the same tip.
    private string _lastResponse = "";

    // topic -> list of words/phrases that trigger it.
    // A Dictionary lets us look things up by topic name instead of long if-chains.
    private readonly Dictionary<string, List<string>> _keywords = new Dictionary<string, List<string>>
    {
        ["password"] = new List<string> { "password", "passphrase", "passcode" },
        ["phishing"] = new List<string> { "phishing", "phish", "fake email", "suspicious email" },
        ["scam"] = new List<string> { "scam", "fraud", "fake website", "con artist" },
        ["privacy"] = new List<string> { "privacy", "personal data", "personal information", "data protection" },
        ["malware"] = new List<string> { "malware", "virus", "ransomware", "spyware" },
        ["browsing"] = new List<string> { "safe browsing", "browsing", "website", "link", "download" }
    };

    // topic -> several possible replies. The bot picks one at random.
    private readonly Dictionary<string, List<string>> _responses = new Dictionary<string, List<string>>
    {
        ["password"] = new List<string>
        {
            "Use a strong, unique password for every account. Avoid personal details like birthdays or pet names.",
            "Longer is stronger. A passphrase of four or more random words is hard to crack and easy to remember.",
            "Never reuse a password across accounts. If one site is breached, attackers try that password everywhere else.",
            "Consider a trusted password manager so you only have to remember one strong master password."
        },
        ["phishing"] = new List<string>
        {
            "Check the sender's address before trusting an email. Scammers often copy the look of real companies.",
            "Be cautious of emails that create urgency, like 'your account will be closed in 24 hours'.",
            "Don't open unexpected links or attachments. If in doubt, go to the official website yourself.",
            "Never give passwords or banking details through email or SMS. Real banks won't ask for them that way."
        },
        ["scam"] = new List<string>
        {
            "Scammers create urgency or promise easy money. If it sounds too good to be true, it usually is.",
            "Never pay upfront fees to receive a prize, loan or job. That's a classic scam pattern.",
            "Verify unexpected requests by calling the organisation on a number you find yourself, not the one in the message.",
            "Never share OTPs or PINs with anyone, even if they claim to be from your bank."
        },
        ["privacy"] = new List<string>
        {
            "Review the privacy settings on your social media accounts so only people you trust can see your information.",
            "Limit app permissions. Does a torch app really need your contacts and location?",
            "Think before you share. Details like your ID number, address or routine can be used against you.",
            "Use a screen lock and keep your software updated to help protect the data on your devices."
        },
        ["malware"] = new List<string>
        {
            "Only download apps and software from official stores and trusted websites.",
            "Keep your antivirus and operating system updated, because updates fix the weaknesses malware uses.",
            "Be careful with email attachments and USB drives from unknown sources. They're common ways malware spreads.",
            "Back up important files regularly so ransomware can't hold your only copy hostage."
        },
        ["browsing"] = new List<string>
        {
            "Look for https:// and the padlock before entering any personal details on a website.",
            "Hover over a link to see where it really goes before you click.",
            "Avoid doing banking on public Wi-Fi. A mobile connection or VPN is safer.",
            "Keep your browser updated and be wary of pop-ups telling you your device is infected."
        }
    };

    // Phrases that mean "continue the topic we were already on".
    private readonly List<string> _followUps = new List<string>
    {
        "tell me more", "more", "another", "explain", "what else", "continue",
        "don't understand", "do not understand", "confused", "go on"
    };

    // Returns the topic found in the sentence, or "" if there isn't one.
    public string DetectTopic(string input)
    {
        string text = input.ToLower();

        foreach (KeyValuePair<string, List<string>> entry in _keywords)
        {
            foreach (string word in entry.Value)
            {
                if (text.Contains(word))
                {
                    return entry.Key; // the topic name, e.g. "password"
                }
            }
        }

        return "";
    }

    public bool IsFollowUp(string input)
    {
        string text = input.ToLower();

        foreach (string phrase in _followUps)
        {
            if (text.Contains(phrase))
            {
                return true;
            }
        }

        return false;
    }

    // Picks a random reply for the topic, avoiding an immediate repeat.
    public string GetRandomResponse(string topic)
    {
        List<string> options = _responses[topic];
        string choice = options[_random.Next(options.Count)];

        // Re-roll a few times if we got the same one as last time.
        int attempts = 0;
        while (choice == _lastResponse && options.Count > 1 && attempts < 5)
        {
            choice = options[_random.Next(options.Count)];
            attempts++;
        }

        _lastResponse = choice;
        return choice;
    }

    // Picks any topic at random (used when the user shares a feeling but names no topic).
    public string GetRandomTopic()
    {
        List<string> topics = new List<string>(_responses.Keys);
        return topics[_random.Next(topics.Count)];
    }
}