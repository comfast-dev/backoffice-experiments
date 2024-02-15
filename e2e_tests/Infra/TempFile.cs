namespace e2e_tests.Infra;

public class TempFile {
    private readonly string path;

    public TempFile(string name) {
        path = Path.Combine(Path.GetTempPath(), name);
    }

    public void save(string content) {
        using(StreamWriter sw = new StreamWriter(path)) {
            sw.Write(content);
        }
    }
    
    public string read() {
        using(StreamReader sr = new StreamReader(path)) {
            return sr.ReadToEnd();
        }
    }
}