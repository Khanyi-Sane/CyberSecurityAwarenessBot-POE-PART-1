public class AsciiArt
{
    // Returns the logo as text, because a GUI has no Console to print to.
    public string GetLogo()
    {
        // @"..." keeps backslashes exactly as typed.
        string[] lines =
        {
            @"   _____      _                _____ ",
            @"  / ____|    | |              / ____|",
            @" | |    _   _| |__   ___ _ __ | (___  ___  ___",
            @" | |   | | | | '_ \ / _ \ '__| \___ \/ _ \/ __|",
            @" | |___| |_| | |_) |  __/ |    ____) |  __/ (__",
            @"  \_____\__, |_.__/ \___|_|   |_____/ \___|\___|",
            @"         __/ |",
            @"        |___/      A W A R E N E S S   B O T",
            "",
            @"            [ * ]  STAY SAFE ONLINE  [ * ]"
        };

        // Join puts a line break between each line.
        return string.Join(Environment.NewLine, lines);
    }
}