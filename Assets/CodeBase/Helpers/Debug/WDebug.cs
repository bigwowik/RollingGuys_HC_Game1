using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Helpers.Debug
{
    public static class WDebug
    {
        public static DebugMessageType[] MessageTypes;

        static WDebug()
        {
            MessageTypes = new []
            {
                new DebugMessageType(WType.Infrastructure, Color.magenta),
                new DebugMessageType(WType.Logic, Color.blue),
                new DebugMessageType(WType.Services, Color.green),
                new DebugMessageType(WType.UI, Color.yellow),
                new DebugMessageType(WType.GameStates, Color.cyan),
                new DebugMessageType(WType.Data, new Color(210,105,30)),
            };
        }

        public static void Log(WType type, string message)
        {
            var debugMessageType = MessageTypes.Single(z => z.Type == type);
            var newString = "";
            newString += $"<color=#";
            newString += ColorUtility.ToHtmlStringRGBA(debugMessageType.Color);
            newString += ">";
            newString += debugMessageType.Type.ToString();
            newString += ": ";
            newString += "</color>";
            newString += message;
            
            UnityEngine.Debug.Log(newString);
        }
        public static void Log( string message, WType type)
        {
            Log(type, message);
        }
        
    }
    
    
    public class DebugMessageType
    {
        public WType Type;
        public Color Color;

        public DebugMessageType(WType type, Color color)
        {
            Type = type;
            Color = color;
        }
    }

    public enum WType
    {
        Infrastructure,
        Logic,
        Services,
        UI,
        GameStates,
        Data
    }
}