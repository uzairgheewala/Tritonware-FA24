using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();


    public void SetFlag(string flagName, bool value)
    {
        flags[flagName] = value;
    }

    public bool IsFlagSet(string flagName)
    {
        if (flags.ContainsKey(flagName))
        {
            return flags[flagName];
        }
        return false;
    }

    void OnGUI()
    {
        string flagStatus = "Flags:\n";
        foreach (var flag in flags)
        {
            flagStatus += flag.Key + ": " + flag.Value + "\n";
        }
        GUI.Label(new Rect(10, 10, 300, 500), flagStatus);
    }
}