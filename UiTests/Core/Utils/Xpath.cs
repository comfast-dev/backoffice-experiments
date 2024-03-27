using System.Text;

namespace UiTests.Lib;

public class Xpath {
    /**
     * Escape text for xpath, so it can include ' and " characters
     * how to use:
     * string xpath = $"//a[text()={EscapeXpath("It's \"hard\" text to match")}]"
     */
    public static string EscapeText(string text) {
        var parts = text.Split("'");
        if (parts.Length == 1) return $"'{text}'";
        if (!text.Contains("\"")) return $"\"{text}\"";
        
        var builder = new StringBuilder("concat('");
        for (var i = 0; i < parts.Length; i++) {
            builder.Append(parts[i] + "'");
            if (i < parts.Length - 1) builder.Append(", \"'\", '");
        }
        
        return builder.Append(")").ToString();
    }

    public static string TextIs(string text) {
        return $".={EscapeText(text)}";
    }
    
    public static string Contains(string text) {
        return $"contains(.,{EscapeText(text)})";
    }
}