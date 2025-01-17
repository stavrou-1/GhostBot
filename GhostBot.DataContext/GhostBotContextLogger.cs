using static System.Environment;

namespace GhostBot.EntityModels {
    public class GhostBotContextLogger
    {
        public static void WriteLine(string message)
        {
            string path = Path.Combine(GetFolderPath(
                SpecialFolder.DesktopDirectory), "GhostBot.txt");
            StreamWriter textFile = File.AppendText(path);
            textFile.WriteLine(message);
            textFile.Close();
        }
    }
}
