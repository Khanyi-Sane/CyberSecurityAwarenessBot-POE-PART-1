// ResponseHandler.cs
// Receives the user's input and identifies what cybersecurity topic they are asking about.
// Uses keyword matching and menu numbers to provide a relevant response.

using System;

public class ResponseHandler
{
    // Processes the user's question and returns a suitable response.
    public string GetResponse(string userInput, string userName)
    {
        string input = userInput.ToLower().Trim();

        // Responds when the user asks how the bot is doing.
        if (input.Contains("how are you"))
        {
            return "I'm running smoothly, thanks for asking! I stay busy keeping an eye " +
                   "out for cyber threats. How can I help you stay safe today?";
        }

        // Option 4 or questions about the bot's purpose.
        else if (input == "4" || input.Contains("purpose") ||
                 input.Contains("who are you") || input.Contains("what are you"))
        {
            return "I'm the Cybersecurity Awareness Assistant. My purpose is to help " +
                   "everyday users like you understand common online threats - like " +
                   "phishing and weak passwords - and to give practical tips to stay safe.";
        }

        // Responds when the user asks what topics are available.
        else if (input.Contains("what can i ask") ||
                 input.Contains("help") ||
                 input.Contains("topics"))
        {
            return "You can choose one of the following options:\n" +
                   "  1. Password Safety\n" +
                   "  2. Phishing\n" +
                   "  3. Safe Browsing\n" +
                   "  4. My Purpose\n" +
                   "Just type the number or ask a question, " + userName + ".";
        }

        // Option 1 or questions about password safety.
        else if (input == "1" || input.Contains("password"))
        {
            return "Password safety tip: Use a unique, strong password for every account - " +
                   "at least 12 characters, mixing upper and lower case letters, numbers and " +
                   "symbols. Avoid using personal details like your name or birthday, and " +
                   "consider using a password manager so you never have to reuse passwords.";
        }

        // Option 2 or questions about phishing.
        else if (input == "2" || input.Contains("phishing"))
        {
            return "Phishing tip: Phishing emails or messages try to trick you into giving " +
                   "away personal information by pretending to be a trusted source, like your " +
                   "bank. Always check the sender's email address carefully, avoid clicking " +
                   "suspicious links, and never share passwords or sensitive information.";
        }

        // Option 3 or questions about safe browsing.
        else if (input == "3" ||
                 input.Contains("browsing") ||
                 input.Contains("browser") ||
                 input.Contains("website") ||
                 input.Contains("link"))
        {
            return "Safe browsing tip: Stick to websites that start with 'https://' " +
                   "(the padlock icon shows the connection is encrypted), avoid downloading " +
                   "files from unfamiliar sites, and keep your browser and antivirus software " +
                   "up to date.";
        }

        // Responds when the user says thank you.
        else if (input.Contains("thank"))
        {
            return "You're welcome, " + userName + "! Stay safe out there.";
        }

        // Default response when the input is not recognised.
        else
        {
            return "I didn't quite understand that. Could you rephrase? You can choose " +
                   "1 for Password Safety, 2 for Phishing, 3 for Safe Browsing, or " +
                   "4 for My Purpose.";
        }
    }
}