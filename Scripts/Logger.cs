using UnityEngine;

using System.IO;

public class Logger : ScriptableObject
{
    private static Logger logger;

    private string path;

    public static void Init()
    {
        if (logger == null)
        {
            logger = new Logger();
            logger.path = Application.persistentDataPath + "/log.txt";
            Debug.Log("Starting new session at " + System.DateTime.Now + ". Log file saved to " + logger.path);
            logger.WriteToFile("Starting new session");
        }
    }
    public static void WriteString(string text)
    {
        if (logger == null)
        {
            Init();
        }

        logger.WriteToFile(text);
    }

    private void WriteToFile(string text)
    {
        StreamWriter writer = new StreamWriter(logger.path, true);
        writer.WriteLine("[" + System.DateTime.Now + "] " + text);
        writer.Close();
    }
}
