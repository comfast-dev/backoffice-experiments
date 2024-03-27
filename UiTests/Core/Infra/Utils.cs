using System.Reflection;

namespace UiTests.Lib.Infra;

public static class Utils
{
    public static T readField<T>(object target, string fieldPath) {
        var curr = target;
        foreach (var name in fieldPath.Split('.')) curr = readOneField(curr, name);

        return (T)curr;
    }

    private static object readOneField(object target, string name) {
        var prop = target.GetType().GetProperty(name);
        if (prop != null) return prop.GetValue(target);

        var privateField = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (privateField != null) return privateField.GetValue(target);

        throw new Exception("Oh no");
    }
}