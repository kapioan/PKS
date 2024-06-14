namespace CorporatePractice1.Data.Subtypes
{
    public class PlanItem
    {
        public long StudentID { get; set; } = 0;
        public long SubjectID { get; set; } = 0;
        public byte Grade { get; set; } = 0;

        public PlanItem() { }
        public PlanItem(long student_id, long subject_id, byte grade)
        {
            StudentID = student_id;
            SubjectID = subject_id;
            Grade = grade;
        }
    }
}