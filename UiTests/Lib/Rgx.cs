using System.Text.RegularExpressions;

namespace UiTests.Lib;

public static class Rgx {
    public static string RgxReplace(this string str, string pattern, string replacement) {
        return Regex.Replace(str, pattern, replacement);
    }
    
    public static string TrimLength(this string str, int length) {
        return str.Length <= length ? str : str.Substring(0, length);
    }
}