using UnityEngine;
using System.Text.RegularExpressions;

public static class StringDefine
{
    /// <summary>
    /// 문장에서 첫 단어를 분리해낸다.
    /// </summary>
    /// <param name="text">분리할 문장</param>
    /// <param name="word">분리해낸 단어. 없으면 빈 문자열</param>
    /// <param name="remain">나머지 문장. 없으면 빈 문자열</param>
    public static void SplitFirstWord(string text, out string word, out string remain)
    {
        int sepIdx = text.IndexOf(' ');
        if (sepIdx < 0)
        {
            word = text;
            remain = string.Empty;
        }
        else
        {
            word = text.Substring(0, sepIdx);
            remain = text.Substring(sepIdx + 1, text.Length - sepIdx - 1);
        }
    }

    public static Color ParseColor(string text)
    {
        string pattern = Pattern._colorIntGroup + "$";
        Match match = Regex.Match(text, pattern);
        if (!match.Success)
        {
            return Color.black;
        }
        else
        {
            byte r;
            byte.TryParse(match.Groups[1].Value, out r);
            byte g;
            byte.TryParse(match.Groups[2].Value, out g);
            byte b;
            byte.TryParse(match.Groups[3].Value, out b);
            Color32 color = new Color32(r, g, b, 255);
            return color;
        }
    }

    public static Vector3 ParseVector3(string text)
    {
        string pattern = Pattern._vector3InnerGroup + "$";
        Match match = Regex.Match(text, pattern);
        if (!match.Success)
        {
            return Vector3.zero;
        }
        else
        {
            float x = ParseFloat(match.Groups[1].Value);
            float y = ParseFloat(match.Groups[2].Value);
            float z = ParseFloat(match.Groups[3].Value);
            return new Vector3(x, y, z);
        }
    }

    public static Vector3 ParseVector2(string text)
    {
        string pattern = Pattern._vector2InnerGroup + "$";
        Match match = Regex.Match(text, pattern);
        if (!match.Success)
        {
            return Vector2.zero;
        }
        else
        {
            float x = ParseFloat(match.Groups[1].Value);
            float y = ParseFloat(match.Groups[2].Value);
            return new Vector2(x, y);
        }
    }

    public static float ParseFloat(string text, float defaultValue = 0.0f)
    {
        float val;
        if (float.TryParse(text, out val))
        {
            return val;
        }
        else
        {
            return defaultValue;
        }
    }

    public static bool ParseBoolean(string text, bool defaultValue = false)
    {
        bool val;
        if (bool.TryParse(text, out val))
        {
            return val;
        }
        else
        {
            return defaultValue;
        }
    }

    public static string ReplaceEscape(string text)
    {
        text = text.Replace("\\n", "\n");
        return text;
    }

    public static string GetMatchGroupSting(Match match)
    {
        string text = "";
        for (int i = 0; i < match.Groups.Count; ++i)
        {
            text += ("[" + i.ToString() + "] " + match.Groups[i].Value + "\n");
        }
        return text;
    }

    public static class Pattern
    {
        public const string _wordGroup = @"([^\s]+)";
        public const string _int = @"(?:[-+]?\d+)";
        public const string _intGroup = @"([-+]?\d+)";
        public const string _float = @"(?:[-+]?\d+(?:.\d+)?)";
        public const string _floatGroup = @"([-+]?\d+(?:.\d+)?)";
        public const string _colorGroup = @"([(]" + _int + "," + _int + "," + _int + "[)])";
        public const string _colorIntGroup = @"(?:[(]" + _intGroup + "," + _intGroup + "," + _intGroup + "[)])";
        public const string _booleanGroup = @"(true|false)";
        public const string _stringGroup = @"(.+)";
        public const string _emptyString = @"^$";
        public const string _anyString = @".*";
        public const string _anyStringGroup = @"(.*)";
        public const string _vector3Group = @"([(]" + _float + "," + _float + "," + _float + "[)])";
        public const string _vector3InnerGroup = @"(?:[(]" + _floatGroup + "," + _floatGroup + "," + _floatGroup + "[)])";
        public const string _vector2Group = @"([(]" + _float + "," + _float + "[)])";
        public const string _vector2InnerGroup = @"(?:[(]" + _floatGroup + "," + _floatGroup + "[)])";
    }
}