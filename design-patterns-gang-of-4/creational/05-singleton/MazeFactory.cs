namespace DesignPatterns.Creational.Singleton;

#region Very simple Singleton

/// <summary>
/// Very simple Singleton pattern implementation. This class
/// is responsible for creating and managing it's own instance.
/// It only has 1 type, and no sub-types.
/// </summary>
public class MazeFactory
{
    private static MazeFactory? _instance;

    protected MazeFactory()
    {
        
    }
    
    public static MazeFactory GetInstance()
    {
        // The book way
        // if (_instance == null)
        // {
        //     _instance = new MazeFactory();
        // }
        //
        // return _instance;
        
        // In CSharp nowadays we can do this
        return _instance ??= new MazeFactory();
    }
}

#endregion

#region Singleton Suggested
public class BombedMazeFactory : SuggestedMazeFactory {}
public class EnchantedMazeFactory : SuggestedMazeFactory {}

/// <summary>
/// This is more complicated Singleton, since it has "children", or classes which
/// inherits from the same base class.
///
/// The type is determined by the environment variable "MazeFactoryType"
/// </summary>
public class SuggestedMazeFactory
{
    private static SuggestedMazeFactory? _instance;

    protected SuggestedMazeFactory()
    {
        
    }
    
    public static SuggestedMazeFactory GetInstance()
    {
        if (_instance != null) return _instance;

        var factoryType = Environment.GetEnvironmentVariable("MazeFactoryType");

        _instance = factoryType switch
        {
            "bombed" => new BombedMazeFactory(),
            "enchanted" => new EnchantedMazeFactory(),
            _ => new SuggestedMazeFactory()
        };

        return _instance;
    }
}
#endregion