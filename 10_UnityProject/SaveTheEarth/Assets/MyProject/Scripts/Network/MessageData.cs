public class MessageData
{
    public string Name { get; set; }
    public int Score { get; set; }
    public string Date { get; set; }

    public MessageData()
    {
        Name = "";
        Score = 0;
        Date = "";
    }
}