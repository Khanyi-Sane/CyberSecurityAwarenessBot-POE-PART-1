using System.Media;

public class VoiceGreeting
{
    private readonly string _relativePath = Path.Combine("Audio", "greeting.wav");

    // Kept as a field so the player stays alive while the sound plays.
    private SoundPlayer? _player;

    public void PlayGreeting()
    {
        try
        {
            // BaseDirectory = the folder the .exe runs from, so no personal paths.
            string fullPath = Path.Combine(AppContext.BaseDirectory, _relativePath);

            if (!File.Exists(fullPath))
            {
                return; // missing file: skip quietly instead of crashing
            }

            _player = new SoundPlayer(fullPath);
            _player.Play(); // runs in the background so the window doesn't freeze
        }
        catch (Exception)
        {
            // An audio problem must never stop the chatbot opening.
        }
    }
}