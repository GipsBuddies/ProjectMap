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
        public int characterType { get; set; }
        public int castColor { get; set; }
        public bool hasCastOnLeftArm { get; set; }
        public bool hasCastOnRightArm { get; set; }
        public bool hasCastOnLeftLeg { get; set; }
        public bool hasCastOnRightLeg { get; set; }
    }
}
