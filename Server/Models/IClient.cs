namespace Server.Models
{
    public interface IClient
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
    }
}
