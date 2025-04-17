using System;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.ViewModels
{
    public class ClassesViewModel
    {
        private readonly IClassService classService;

        public ClassesViewModel(IClassService classService)
        {
            this.classService = classService;
        }

        public string ConfirmRegistration(int userId, int classId, DateTime date)
        {
            return classService.ConfirmRegistration(userId, classId, date);
        }
    }
} 