namespace Common.Maze;

public class Maze
{
    private readonly List<Room> _rooms = new();

    public void AddRoom(Room room) => _rooms.Add(room);

    public Room? RoomNo(int roomNumber) => _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
}