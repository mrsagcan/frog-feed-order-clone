using System;

public class Actions
{
    public static Action OnFrogClicked;
    public static Action OnTongueReachedObstacle;
    public static Action OnTongueCollect;
    public static Action OnBerryFound;
    public static Action<Cell> OnCellDisable;
    public static Action OnLevelSuccesful;
    public static Action OnLevelFailed;
    public static Action<int> OnMovesLeftChanged;
}
