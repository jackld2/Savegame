using SQLite4Unity3d;

public class User {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Username { get; set; }
    public float TimePlayed { get; set; }
    public string SaveJSON { get; set; }
    

    public override string ToString()
    {
        return string.Format("User: {0}\nTime Played: {1}", Username, TimePlayedToString(TimePlayed));
    }

    //display a float as time (00h:00m:00s)
    private static string TimePlayedToString(float time)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        return  string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
    }
}
