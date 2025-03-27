namespace ProjectMap.WebApi.Models
{
    public class ChoiceRouteModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Path { get; set; }
        public bool Begining { get; set; }
        public bool Middel { get; set; }
        public bool Finish { get; set; }
        public string NamePatient { get; set; }
        public DateTime BirthDate { get; set; }
        public string NameDoctor { get; set; }
    }
}
