namespace DesignPatterns.Behavioral.TemplateMethod;

public static class RealLifeExample
{
    public static void Run() {
        FileOpener fileOpener = new TextfileOpener();
        fileOpener.ProcessChange("file.txt", "Hello World");
    }
}


abstract class FileOpener
{
    public void ProcessChange(string filePath, string message)
    {
        if(CanOpen(filePath))
        {
            ReadFile(filePath);
            WriteToFile(filePath, message);
            CloseFile(filePath);
        }
    }

    // Can't override
    protected bool CanOpen(string filePath) {
        return true;
    }

    // Must override
    protected abstract void ReadFile(string filePath);

    // Optional to be overriden (i.e. Hook)
    protected virtual void WriteToFile(string filePath, string message) {
        Console.WriteLine("FileOpener.WriteToFile()");
    }

    // Can't override
    protected bool CloseFile(string filePath) {
        return true;
    }
}

class TextfileOpener : FileOpener {
    protected override void ReadFile(string filePath) {
        Console.WriteLine("TextfileOpener.ReadFile()");
    }

    protected override void WriteToFile(string filePath, string message) {
        Console.WriteLine("TextfileOpener.WriteToFile()");
    }
}