namespace CorporatePractice1.Data.Subtypes
{
    public class Subject
    {
        public long SubjectID { get; set; } = 0;
        public string SubjectName { get; set; } = string.Empty;
        public ulong LecturesVolume { get; set; } = 0;
        public ulong PracticeVolume { get; set; } = 0;

        public Subject() { }
        public Subject(long subject_id, string name, ulong lectures_volume, ulong practice_volume)
        {
            SubjectID = subject_id;
            SubjectName = name;
            LecturesVolume = lectures_volume;
            PracticeVolume = practice_volume;
        }
    }
}