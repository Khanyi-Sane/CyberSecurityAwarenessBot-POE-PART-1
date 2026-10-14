namespace CybersecurityChatbotGUI
{
    // The moods the bot can recognise.
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated
    }

    public class SentimentDetector
    {
        private readonly Random _random = new Random();

        // mood -> words that suggest it
        private readonly Dictionary<Sentiment, List<string>> _moodWords = new Dictionary<Sentiment, List<string>>
        {
            [Sentiment.Worried] = new List<string> { "worried", "scared", "afraid", "anxious", "nervous", "concerned", "overwhelmed", "terrified" },
            [Sentiment.Curious] = new List<string> { "curious", "wondering", "wonder", "interested", "fascinated" },
            [Sentiment.Frustrated] = new List<string> { "frustrated", "annoyed", "angry", "fed up", "irritated", "sick of" }
        };

        // mood -> several opening lines ({name} is swapped for the user's name)
        private readonly Dictionary<Sentiment, List<string>> _openings = new Dictionary<Sentiment, List<string>>
        {
            [Sentiment.Worried] = new List<string>
        {
            "It's completely understandable to feel that way, {name}. Let me share a tip to help you stay safe.",
            "Don't worry, {name}, you're not alone. A few simple habits make a big difference.",
            "That's a very normal concern, {name}. Let's take it one small step at a time."
        },
            [Sentiment.Curious] = new List<string>
        {
            "Curiosity is one of the best security habits, {name}. Here's something useful.",
            "Asking questions is how you stay ahead of attackers, {name}. Here's a tip."
        },
            [Sentiment.Frustrated] = new List<string>
        {
            "I hear you, {name}. Online safety can feel annoying, but one small change helps a lot.",
            "That's frustrating, {name}, and you're not the only one who feels it. Here's an easy place to start."
        }
        };

        // Returns the mood found in the sentence (Neutral if none).
        public Sentiment Detect(string input)
        {
            string text = input.ToLower();

            foreach (KeyValuePair<Sentiment, List<string>> entry in _moodWords)
            {
                foreach (string word in entry.Value)
                {
                    if (text.Contains(word))
                    {
                        return entry.Key;
                    }
                }
            }

            return Sentiment.Neutral;
        }

        // Returns a random supportive opening line, or "" for Neutral.
        public string GetOpening(Sentiment sentiment, string name)
        {
            if (sentiment == Sentiment.Neutral)
            {
                return "";
            }

            List<string> options = _openings[sentiment];
            return options[_random.Next(options.Count)].Replace("{name}", name);
        }
    }
}