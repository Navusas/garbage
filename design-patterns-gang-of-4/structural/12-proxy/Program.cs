// In the below example, Proxy adds a 'laxy loader'
// However, other use cases could be to add a security layer, or a logging layer (similar to Decorator)

var image = new ImageProxy("test_10mb.jpg");
image.Display();
image.Display();

interface IImage
{
    void Display();
}

class Realimage : IImage
{
    private readonly string _fileName;

    public Realimage(string fileName)
    {
        _fileName = fileName;
        Console.WriteLine("Load from disk");
    }
    public void Display()
    {
        Console.WriteLine($"Displaying {_fileName}");
    }
}

class ImageProxy : IImage
{
    private readonly string _fileName;
    private Realimage? _realimage;

    public ImageProxy(string fileName)
    {
        _fileName = fileName;
    }
    public void Display()
    {
        if (_realimage == null)
        {
            _realimage = new Realimage(_fileName);
        }
        _realimage.Display();
    }
}