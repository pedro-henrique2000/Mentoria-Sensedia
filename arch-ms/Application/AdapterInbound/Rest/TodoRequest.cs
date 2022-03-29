namespace Application.AdapterInbound.Rest
{
    public class TodoRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime Deadline { get; set; }

    }
}
