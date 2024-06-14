namespace CorporatePractice1.Data.Subtypes
{
    public class Student
    {
        public long StudentID { get; set; } = 0;
        public string StudentName { get; set; } = string.Empty;
        public string StudentSurname { get; set; } = string.Empty;
        public string StudentPatronimic { get; set; } = string.Empty;

        public Student() { }
        public Student(long student_id, string name, string surname, string patronimic)
        {
            StudentID = student_id;
            StudentName = name;
            StudentSurname = surname;
            StudentPatronimic = patronimic;
        }
    }
}