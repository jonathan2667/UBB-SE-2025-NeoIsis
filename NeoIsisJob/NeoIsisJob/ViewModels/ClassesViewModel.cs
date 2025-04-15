using System;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.ViewModels
{
    public class ClassesViewModel
    {
        private readonly IClassService _classService;

        public ClassesViewModel(IClassService classService)
        {
            _classService = classService;
        }

        public string ConfirmRegistration(int userId, int classId, DateTime date)
        {
            return _classService.ConfirmRegistration(userId, classId, date);
        }
    }
} 