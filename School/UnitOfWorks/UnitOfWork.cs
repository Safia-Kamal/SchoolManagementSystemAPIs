using School.Models;
using School.Repository;

namespace School.UnitOfWorks
{
    
    public class UnitOfWork
    {
        SchoolDbContext context;

        public StudentRepo studentRepo;
        public ParentRepo parentRepo;
        public TeacherRepo teacherRepo;
        public ClassRepo classRepo;
        public SubjectRepo subjectRepo;
        public ClassSubjectsRepo classSubjectsRepo;
        public GradeRepo gradeRepo;
        public AttendanceRepo attendanceRepo;
        public TimetableRepo timetableRepo;
        public TeacherClassSubjectRepo teacherClassSubjectRepo;


        public UnitOfWork(SchoolDbContext context) { 
            this.context = context;
        }

        public StudentRepo StudentRepo { get {
                if (studentRepo == null) 
                    studentRepo = new StudentRepo(context);
                return studentRepo;
            } 
        }

        public ParentRepo ParentRepo
        {
            get
            {
                if (parentRepo == null)
                    parentRepo = new ParentRepo(context);
                return parentRepo;
            }
        }

        public TeacherRepo TeacherRepo
        {
            get
            {
                if (teacherRepo == null)
                    teacherRepo = new TeacherRepo(context);
                return teacherRepo;
            }
        }

        public ClassRepo ClassRepo
        {
            get
            {
                if (classRepo == null)
                    classRepo = new ClassRepo(context);
                return classRepo;
            }
        }

        public SubjectRepo SubjectRepo
        {
            get
            {
                if (subjectRepo == null)
                    subjectRepo = new SubjectRepo(context);
                return subjectRepo;
            }
        }

        public TeacherClassSubjectRepo TeacherClassSubjectRepo
        {
            get
            {
                if (teacherClassSubjectRepo == null)
                    teacherClassSubjectRepo = new TeacherClassSubjectRepo(context);
                return teacherClassSubjectRepo;
            }
        }
        public ClassSubjectsRepo ClassSubjectsRepo
        {
            get
            {
                if (classSubjectsRepo == null)
                    classSubjectsRepo = new ClassSubjectsRepo(context);
                return classSubjectsRepo;
            }
        }



        public GradeRepo GradeRepo
        {
            get
            {
                if (gradeRepo == null)
                    gradeRepo = new GradeRepo(context);
                return gradeRepo;
            }
        }

        public AttendanceRepo AttendanceRepo
        {
            get
            {
                if (attendanceRepo == null)
                    attendanceRepo = new AttendanceRepo(context);
                return attendanceRepo;
            }
        }

        public TimetableRepo TimetableRepo
        {
            get
            {
                if (timetableRepo == null)
                    timetableRepo = new TimetableRepo(context);
                return timetableRepo;
            }
        }

        public void save()
        {
            context.SaveChanges();
        }
    }
}
