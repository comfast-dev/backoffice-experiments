namespace UiTests.Lib;

public static class Utils {
    public static string LimitString(this string input, int maxLength) {
        return input.Length > maxLength
            ? input.Substring(0, maxLength) + "..."
            : input;
    }
}