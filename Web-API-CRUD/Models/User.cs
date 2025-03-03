namespace Web_API_CRUD.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Company { get; set; }
        public List<string>? Email { get; set; }
        public string? PersonalPhone { get; set; }
        public string? WorkPhone { get; set; }

        public User(int id, string name, string? company, List<string>? email, string? personalPhone, string? workPhone)
        {
            Id = id;
            Name = name;
            Company = company;
            Email = email;
            PersonalPhone = personalPhone;
            WorkPhone = workPhone;
        }

        public User(string name, string? company, List<string>? email, string? personalPhone, string? workPhone)
        {
            Name = name;
            Company = company;
            Email = email;
            PersonalPhone = personalPhone;
            WorkPhone = workPhone;
        }
    }
}
