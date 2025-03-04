namespace BlazorApp.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public List<string> Email { get; set; }
        public string PersonalPhone { get; set; }
        public string WorkPhone { get; set; }
    }
}
