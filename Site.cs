namespace Arbiter;

public class Site
{
    public string Path = null;
    public List<Uri> Bindings = new List<Uri>();
    public List<string> Rewriters = new List<string>();
    public List<string> DefaultDocs = new List<string>();
    public Dictionary<string, string> Parameters = new Dictionary<string, string>();
    public IProcessor? Processor;
}