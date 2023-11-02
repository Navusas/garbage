namespace CsharpNerds;

public interface IFeature
{
    string Title {get; }
    string? LinkToSource {get;}

    void DemonstrateBefore();

    void DemonstrateAfter();
}