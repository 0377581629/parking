using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using DPS.Park.Core.Student;
using DPS.Reporting.Application.Shared.Dto.Student;
using DPS.Reporting.Application.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;
using Card = DPS.Park.Core.Card.Card;

namespace DPS.Reporting.Application.Services
{
    [AbpAuthorize(ReportPermissions.StudentReports)]
    public class StudentReportingAppService : ZeroAppServiceBase, IStudentReportingAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Student> _studentRepository;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<StudentCard> _studentCardRepository;
        private readonly IRepository<Card> _cardRepository;

        public StudentReportingAppService(IAbpSession abpSession, IRepository<Student> studentRepository,
            ISettingManager settingManager, IRepository<StudentCard> studentCardRepository,
            IRepository<Card> cardRepository)
        {
            _abpSession = abpSession;
            _studentRepository = studentRepository;
            _settingManager = settingManager;
            _studentCardRepository = studentCardRepository;
            _cardRepository = cardRepository;
        }

        [AbpAuthorize(ReportPermissions.StudentRenewCardReport)]
        public async Task<List<StudentRenewCardReportingOutput>> StudentRenewCardReport(
            StudentRenewCardReportingInput input)
        {
            var res = new List<StudentRenewCardReportingOutput>();
            var cards = await _cardRepository.GetAll()
                .Where(o =>
                    o.TenantId == _abpSession.TenantId &&
                    o.CreationTime >= input.StartDate &&
                    o.CreationTime <= input.EndDate)
                .WhereIf(input != null && !string.IsNullOrEmpty(input.Filter), o => o.CardNumber.Contains(input.Filter))
                .WhereIf(input is {CardTypeId: { }}, o => o.CardTypeId == input.CardTypeId)
                .WhereIf(input is {VehicleTypeId: { }}, o => o.VehicleTypeId == input.VehicleTypeId)
                .Include(o => o.CardType)
                .Include(o => o.VehicleType)
                .ToListAsync();
            var students = await _studentRepository.GetAll().Where(o => o.TenantId == _abpSession.TenantId)
                .ToListAsync();
            var cardStudents = await _studentCardRepository.GetAll().Where(o => o.TenantId == _abpSession.TenantId)
                .ToListAsync();

            foreach (var card in cards)
            {
                var cardStudent = cardStudents.FirstOrDefault(o => o.CardId == card.Id);
                var student = students.FirstOrDefault(o => cardStudent != null && o.Id == cardStudent.CardId);
                res.Add(new StudentRenewCardReportingOutput()
                {
                    StudentCode = student != null ? student.Code : "",
                    StudentName = student != null ? student.Name : "",
                    CardNumber = card.CardNumber,
                    CardType = card.CardType != null ? card.CardType.Name : "",
                    VehicleType = card.VehicleType != null ? card.VehicleType.Name : "",
                    RenewCardTime = $"{card.CreationTime.Day}/{card.CreationTime.Month}/{card.CreationTime.Year}"
                });
            }

            return res;
        }
    }
}