using System;

namespace NeoIsisJob.Models
{
    public class UserClassModel
    {
        private int _userId;
        private int _classId;
        private DateTime _enrollmentDate;

        public int UserId { get => _userId; set => _userId = value; }
        public int ClassId { get => _classId; set => _classId = value; }
        public DateTime EnrollmentDate { get => _enrollmentDate; set => _enrollmentDate = value; }

        private UserClassModel() { }

        public UserClassModel(int userId, int classId, DateTime enrollmentDate)
        {
            UserId = userId;
            ClassId = classId;
            EnrollmentDate = enrollmentDate;
        }
    }
}
