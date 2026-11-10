# PROG6221 Programming 2A - POE Part 2

## Student Details

- **Name:** Sanele Khanyisile
- **Student number:** ST10498540
- **Module:** PROG6221 Programming 2A
- **Lecturer:** Ms A Phewa
- **Assessment:** POE Part 2

## Project Overview

This project is a Cybersecurity Awareness Chatbot. In Part 1 it was a C# .NET 8 console application (`CyberSecurityAwarenessBot`). In Part 2 it has been rebuilt as a Windows Forms GUI application (`CybersecurityChatbotGUI`) that keeps the Part 1 features and adds keyword recognition, random responses, follow-ups, memory and sentiment detection.

This repository contains both the Part 1 console app and the Part 2 GUI app. **Part 2 is the `CybersecurityChatbotGUI` project.**

## Part 2 Features

- Voice greeting and ASCII branding from Part 1, running inside the GUI
- Keyword recognition across six cybersecurity topics
- Random responses (several tips per topic, avoiding immediate repeats)
- Follow-up handling (e.g. "tell me more") that continues the current topic
- Memory of the user's name, favourite topic and last topic discussed
- Sentiment detection (worried, curious, frustrated) with an adapted opening line
- Safe handling of blank and unrecognised input
- Part 1 menu options (1-4) and responses kept as a fallback

## GUI Overview

<img width="1920" height="1080" alt="Chatbot GUI" src="https://github.com/user-attachments/assets/ac5efe78-aaf9-4eb3-a3dc-7bfecd28adce" />

| Control | Purpose |
|---------|---------|
| ASCII banner (`rtbBanner`) | Shows the bot logo at the top |
| Chat history (`rtbChatHistory`) | Shows the conversation; bot and user messages are colour-coded |
| Input box (`txtUserInput`) | Where the user types; pressing Enter also sends |
| Send button (`btnSend`) | Sends the message to the bot |

## Cybersecurity Topics Supported

Password safety, phishing, scams, privacy, malware and safe browsing. Example keywords include "password", "phishing", "scam", "privacy", "virus" and "website". The Part 1 menu (1 Password Safety, 2 Phishing, 3 Safe Browsing, 4 My Purpose) also still works.

## Memory and Sentiment Features

**Memory**
- The first message is stored as the user's name.
- Saying something like "I'm interested in privacy" stores a favourite topic.
- Asking "what do you remember" recalls the name, favourite topic and last topic.
- Later replies are personalised using the stored interest.

**Sentiment**
- Detects worried, curious and frustrated wording (e.g. "I am worried about scams").
- Adds a supportive opening line, then gives a relevant tip.
- If no topic is named, it uses the current topic or picks one at random.

## How to Run the Project

**Requirements:** Windows, Visual Studio 2022 or later, .NET 8 SDK.

1. Clone the repository:
git clone https://github.com/Khanyi-Sane/CyberSecurityAwarenessBot-POE-PART-1.git


2. Open CybersecurityAwarenessBot in Visual Studio.
3. **Open the Part 2 project `CybersecurityChatbotGUI`.** If the solution contains both Part 1 and Part 2, right-click `CybersecurityChatbotGUI` in Solution Explorer and choose **Set as Startup Project**. Part 1 (`CyberSecurityAwarenessBot`) is the console version and is not the one to run for Part 2.
4. **Audio file:** the WAV greeting file is located in [folder path, e.g. `CybersecurityChatbotGUI/Assets/`]. It is copied to the output folder when you build, so no manual steps are needed.
5. Press **F5** to build and run.

**Features to test (in this order):**
1. Enter your name when asked (listen for the voice greeting at start-up).
2. Type `passwords`, `phishing`, `malware` (keyword recognition).
3. Repeat a topic to see a different tip (random responses).
4. Type `tell me more` (follow-up).
5. Type `I'm interested in privacy`, then `what do you remember` (memory).
6. Type `I am worried about scams` (sentiment).
7. Send a blank message, then `asdfgh` (safe handling of unknown input).

## Project Structure

```
CyberSecurityAwarenessBot-POE-PART-1/
├── README.md
├── [Solution].sln
├── .github/workflows/[ci-file].yml      CI workflow
├── CybersecurityChatbotGUI/              Part 2 (Windows Forms)
│   ├── ChatbotForm.cs                    GUI: displays messages, passes input on
│   ├── Chatbot.cs                        Main logic and the BotMessageHandler delegate
│   ├── KeywordResponder.cs               Topic detection, follow-ups, random replies (Dictionary/List)
│   ├── SentimentDetector.cs              Mood detection and supportive openings
│   ├── ResponseHandler.cs                Part 1 logic, kept as fallback
│   ├── UserProfile.cs                    Stores name, favourite topic, current topic
│   ├── VoiceGreeting.cs                  Plays the WAV greeting
│   ├── AsciiArt.cs                       Banner/logo text
│   └── [Assets folder]/                  WAV file
└── [Part 1 folder]/                      CyberSecurityAwarenessBot (console)
```

Key techniques: `Dictionary<string, List<string>>` for keywords and responses, `enum Sentiment`, and the delegate `BotMessageHandler`, which lets the bot display messages in the form without referencing it directly.

## GitHub / CI Information

The repository uses GitHub Actions. The workflow builds `CybersecurityChatbotGUI` on `windows-latest` on every push.

- [Repository](https://github.com/Khanyi-Sane/CyberSecurityAwarenessBot-POE-PART-1)
- [Actions (CI runs)](https://github.com/Khanyi-Sane/CyberSecurityAwarenessBot-POE-PART-1/actions)

## Tags and Releases

Part 2 was released in three tagged versions:

- `v2.0.0`: GUI foundation with Part 1 features integrated
- `v2.1.0`: Keyword recognition, random responses and follow-ups
- `v2.2.0`: Memory, sentiment detection and final release

[View all releases](https://github.com/Khanyi-Sane/CyberSecurityAwarenessBot-POE-PART-1/releases)

## Video Presentation Link

https://youtu.be/-8WY5uqB1_0 
