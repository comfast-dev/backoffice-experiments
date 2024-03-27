using System.Diagnostics;
using UiTests.Lib.Comfast;

namespace UiTests.Lib;

public class WaitUtils {
    public static void WaitFor(Func<bool> action, string? description = null, int? timeoutMs = null) {
        var timeout = timeoutMs ?? DriverConfig.TimeoutMs;
        var end = DateTime.Now.AddMilliseconds(timeoutMs ?? DriverConfig.TimeoutMs);
        while (DateTime.Now  < end) {
            try {
                bool res = action.Invoke();
                if (res) return;
            } catch (Exception e) { }
            Thread.Sleep(300);
        }

        int timeoutSec = timeout / 1000;
        string? descr = description ?? action.ToString();
        throw new Exception($"Wait failed after {timeoutSec}s, for: {descr}");
    }
}