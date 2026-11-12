# PROG6221 Cybersecurity Awareness Chatbot - Final POE

## Student Information
**Name:** Sanele Khanyisile Mhlanga
**Student Number:** ST10498540
**Module:** PROG6221 Programming 2A

## Project Overview
This application is a GUI-based Cybersecurity Awareness Assistant (C#, .NET 8, Windows Forms)
that combines the functionality developed in Parts 1, 2 and 3.

## Final Features
### Part 1
- Voice greeting
- Cybersecurity identity / ASCII element
- Basic cybersecurity interaction
- Input validation

### Part 2
- GUI interaction
- Keyword recognition
- Random responses
- Conversation flow
- Memory and recall
- Sentiment detection

### Part 3
- MySQL Task Assistant
- Optional reminders
- Complete and delete task actions
- Cybersecurity quiz (12 questions, multiple choice and true/false)
- NLP simulation using string/phrase detection
- Activity Log

## Project Structure
| Class | Purpose |
|---|---|
| ChatbotForm | Main window with tabs: Chat, Tasks, Cyber Quiz, Activity Log |
| CyberTask | Model for a single task |
| TaskRepository | Add, read, complete and delete tasks in MySQL |
| DatabaseConfig | Holds the MySQL connection string |
| QuizQuestion / QuizManager | Quiz questions, scoring and feedback |
| NlpProcessor | Detects what the user wants from their typed phrase |
| ActivityLogger | Records and returns recent actions |

## Database Setup
1. Install and start MySQL Server.
2. Run `database_setup.sql` in MySQL Workbench. It creates the
   `CyberSecurityBotDB` database and the `cyber_tasks` table.
3. Open `DatabaseConfig.cs`.
4. Replace `YOUR_PASSWORD` with your local MySQL password.
5. Open the solution in Visual Studio, then build and run the application.

## How to Use
- Chat normally about cybersecurity in the Chat tab.
- Use the Tasks tab to create and manage cybersecurity tasks.
- Use the Cyber Quiz tab to start the quiz.
- Use the Activity Log tab to view recent actions.

Example commands:
- Add a task to review privacy settings.
- Remind me to update my password tomorrow.
- Start the cyber quiz.
- Show my tasks.
- What have you done for me?

## Releases
- v1.0-part1
- v2.0-part2
- v3.0-final

## Video Presentation
[Watch the demonstration on YouTube](https://youtu.be/_-KFP7VVYwI)
