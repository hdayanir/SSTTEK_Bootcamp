namespace WebApplication1
{
    public class EmailListDetails
    {
        public int EmailListDetailId { get; set; }
        public string? FromAdress { get; set; }
        public string? ToAdress { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string SentTime { get; set; }
    }
}