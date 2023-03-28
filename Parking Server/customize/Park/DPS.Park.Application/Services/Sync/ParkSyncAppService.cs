using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using DPS.Park.Application.Shared.Dto.Message;
using DPS.Park.Application.Shared.Dto.Sync;
using DPS.Park.Application.Shared.Interface.Common;
using DPS.Park.Application.Shared.Interface.Sync;
using DPS.Park.Core.Message;
using DPS.Park.Core.Student;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Z.EntityFramework.Extensions;
using Zero;
using Zero.Configuration;

namespace DPS.Park.Application.Services.Sync
{
    [AbpAuthorize]
    public class ParkSyncAppService : ZeroAppServiceBase, IParkSyncAppService
    {
        #region Constructor

        private readonly IParkAppService _parkAppService;
        private readonly IRepository<Core.History.History> _historyRepository;
        private readonly ISettingManager _settingManager;
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Core.Student.Student> _studentRepository;
        private readonly IRepository<StudentCard> _studentCardRepository;
        private readonly IRepository<Core.Card.Card> _cardRepository;

        public ParkSyncAppService(
            IParkAppService parkAppService,
            IRepository<Core.History.History> historyRepository,
            ISettingManager settingManager,
            IRepository<Message> messageRepository, IRepository<Core.Student.Student> studentRepository,
            IRepository<StudentCard> studentCardRepository, IRepository<Core.Card.Card> cardRepository)
        {
            _parkAppService = parkAppService;
            _historyRepository = historyRepository;
            _settingManager = settingManager;
            _messageRepository = messageRepository;
            _studentRepository = studentRepository;
            _studentCardRepository = studentCardRepository;
            _cardRepository = cardRepository;
        }

        #endregion

        public async Task SendInfo(SyncDto input)
        {
            // Client sent to Sv
            if (input is {Details: { }} && input.Details.Any())
            {
                using (CurrentUnitOfWork.SetTenantId(input.TenantId))
                {
                    EntityFrameworkManager.ContextFactory = _ => _historyRepository.GetDbContext();
                    var todayHistories = await _historyRepository.GetAllListAsync(o =>
                        EF.Functions.DateDiffDay(o.InTime, DateTime.Today) == 0);
                    if (todayHistories != null && todayHistories.Any())
                    {
                        await _historyRepository.GetDbContext().BulkDeleteAsync(todayHistories);
                    }

                    var newDetails = ObjectMapper.Map<List<Core.History.History>>(input.Details);
                    foreach (var newDetail in newDetails)
                    {
                        newDetail.TenantId = input.TenantId;
                        newDetail.CreationTime = DateTime.Now;
                    }

                    await _historyRepository.GetDbContext().BulkInsertAsync(newDetails);
                }
            }
        }

        public async Task<List<GetStudentActiveInfoSyncDto>> GetStudentActiveInfo()
        {
            // Sv to Client
            var res = new List<GetStudentActiveInfoSyncDto>();

            var students = await _studentRepository
                .GetAllListAsync(o => !o.IsDeleted && o.IsActive && o.TenantId == AbpSession.TenantId);
            var studentCards = await _studentCardRepository.GetAllListAsync();
            var cards = await _cardRepository.GetAllListAsync(o =>
                !o.IsDeleted && o.IsActive && o.TenantId == AbpSession.TenantId);

            foreach (var student in students)
            {
                var cardIdsOfStudent =
                    studentCards.Where(o => o.StudentId == student.Id).Select(o => o.CardId).ToList();
                var cardNumbersOfStudent =
                    cards.Where(o => cardIdsOfStudent.Contains(o.Id)).Select(o => o.CardNumber).ToList();
                var cardNumberOfStudentToSync = string.Join(";", cardNumbersOfStudent);
                
                var defaultAvatarPath = GlobalConfig.DefaultAvatarUrlBackEnd;
                var defaultAvatarBytes = await File.ReadAllBytesAsync(defaultAvatarPath);
                var defaultAvatarBase64 = Convert.ToBase64String(defaultAvatarBytes);
                
                res.Add(new GetStudentActiveInfoSyncDto()
                {
                    Id = student.Id,
                    Code = student.Code,
                    Name = student.Name,
                    AvatarBase64 = defaultAvatarBase64,
                    Male = student.Gender ? 1 : 0,
                    DoBStr = $"{student.DoB.Day}/{student.DoB.Month}/{student.DoB.Year}",
                    CardNumber = cardNumberOfStudentToSync
                });
            }

            return res;
        }

        public async Task<MessageInfoDto> SendMessageInfo()
        {
            var decreasePercentStr = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                .DecreasePercent);
            var decreasePercent = double.Parse(decreasePercentStr);

            var historyOutToday = await _historyRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                .Where(o => EF.Functions.DateDiffDay(o.OutTime, DateTime.Today) == 0)
                .Select(o => o.Price)
                .ToListAsync();

            var turnOverToday = (1 - decreasePercent / 100) * historyOutToday.Sum();

            //Add new message to DB
            var message = new CreateOrEditMessageDto()
            {
                Content = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year +
                          " - VNUA - Bãi xe - Doanh thu " + turnOverToday
            };
            var obj = ObjectMapper.Map<Message>(message);
            obj.TenantId = AbpSession.TenantId;
            await _messageRepository.InsertAndGetIdAsync(obj);

            var res = new MessageInfoDto()
            {
                PhoneToSendMessage = await _settingManager.GetSettingValueAsync(AppSettings.ParkSettings
                    .PhoneToSendMessage),
                MessageContent = obj.Content
            };
            return res;
        }
    }
}