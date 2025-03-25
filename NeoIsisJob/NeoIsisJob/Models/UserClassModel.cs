using System;

namespace NeoIsisJob.Models
{
    public class UserClassModel
    {
        public int UserId { get; private set; }
        public int ClassId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }

        private UserClassModel() { }

        public UserClassModel(int userId, int classId, DateTime enrollmentDate)
        {
            UserId = userId;
            ClassId = classId;
            EnrollmentDate = enrollmentDate;
        }
    }
}
