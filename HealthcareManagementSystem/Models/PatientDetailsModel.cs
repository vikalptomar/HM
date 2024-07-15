namespace HealthcareManagementSystem.Models
{
    public class PatientDetailsModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateRegistered { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string Specializations { get; set; }

    }
}
