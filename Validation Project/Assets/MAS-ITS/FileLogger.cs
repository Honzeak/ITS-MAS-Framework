using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

// Logger for subsequent data analysis
public class FileLogger : MonoBehaviour
{
    private Regex logMatchRegex = new Regex(@"timetofinish", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private Regex timeMatchRegex = new Regex(@"- (?<time>.+)", RegexOptions.Compiled);
    private StreamWriter writer;
    public void OnEnable()
    {
        Debug.LogWarning("------ ------ Registered log callback----- ---------");
        Application.logMessageReceived += LogToFile;
        writer = new(Path.Join("TimeLogs","timeLog.txt"), false);
        writer.WriteLine("TravelTime");
        writer.AutoFlush = true;
    }

    public void OnDisable()
    {
        Application.logMessageReceived -= LogToFile;
        writer.Dispose();
    }

    public void LogToFile(string condition, string stackTrace, LogType type)
    {
        if (!logMatchRegex.IsMatch(condition)) return;
        var result = timeMatchRegex.Match(condition);
        if (!result.Success) return;
        var capTime = result.Groups["time"].Value;
        writer.WriteLine(capTime);
    }
}
