namespace UiTests.Lib.Infra;

public class TempFile
{
    private readonly string path;

    public TempFile(string name) {
        path = Path.Combine(Path.GetTempPath(), name);
    }

    public void save(string content) {
        using (var sw = new StreamWriter(path)) {
            sw.Write(content);
        }
    }

    public string read() {
        using (var sr = new StreamReader(path)) {
            return sr.ReadToEnd();
        }
    }
}