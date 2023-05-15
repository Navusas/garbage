namespace Common.Maze;

public abstract class MapSite
{
     /// <summary>
     /// If you enter a room, then your location changes. If you try to enter a door,
     /// then one of the two things happen:
     ///   - If the door is open, then you go into the next room.
     ///   - If the door is closed, then you hurt your nose.
     /// </summary>
     public abstract void Enter();
}