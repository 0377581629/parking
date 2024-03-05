using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using DPS.Park.Application.Shared.Dto.Student;
using DPS.Park.Application.Shared.Dto.Student.StudentCard;
using DPS.Park.Application.Shared.Interface.Student;
using DPS.Park.Core.Student;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;
using Zero;
using Zero.Authorization;

namespace DPS.Park.Application.Services.Student
{
    [AbpAuthorize(ParkPermissions.Student)]
    public class StudentAppService : ZeroAppServiceBase, IStudentAppService
    {
        private readonly IRepository<Core.Student.Student> _studentRepository;
        private readonly IRepository<StudentCard> _studentCardRepository;

        public StudentAppService(IRepository<Core.Student.Student> studentRepository,
            IRepository<StudentCard> studentCardRepository)
        {
            _studentRepository = studentRepository;
            _studentCardRepository = studentCardRepository;
        }

        private class QueryInput
        {
            public GetAllStudentInput Input { get; set; }

            public int? Id { get; set; }
        }

        private IQueryable<StudentDto> StudentQuery(QueryInput queryInput)
        {
            var input = queryInput.Input;
            var id = queryInput.Id;

            var query = from obj in _studentRepository.GetAll()
                    .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                    .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                        o => o.Code.Contains(input.Filter) || o.Name.Contains(input.Filter))
                    .WhereIf(id.HasValue, o => o.Id == id.Value)
                select new StudentDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,
                    Code = obj.Code,
                    Name = obj.Name,
                    Avatar = obj.Avatar,
                    Email = obj.Email,
                    PhoneNumber = obj.PhoneNumber,
                    Gender = obj.Gender,
                    DoB = obj.DoB,
                    IsActive = obj.IsActive,
                    
                    UserId = obj.UserId,
                    UserName = obj.User.UserName,
                    UserEmail = obj.User.EmailAddress
                };
            return query;
        }

        private IQueryable<StudentCardDto> StudentCardQuery(int studentId)
        {
            var query = from obj in _studentCardRepository.GetAll()
                    .Where(o => o.TenantId == AbpSession.TenantId && o.StudentId == studentId)
                select new StudentCardDto
                {
                    TenantId = obj.TenantId,
                    Id = obj.Id,

                    StudentId = obj.StudentId,
                    StudentCode = obj.Student.Code,
                    StudentName = obj.Student.Name,
                    StudentEmail = obj.Student.Email,
                    StudentPhoneNumber = obj.Student.PhoneNumber,

                    CardId = obj.CardId,
                    CardCode = obj.Card.Code,
                    CardNumber = obj.Card.CardNumber,

                    Note = obj.Note
                };
            return query;
        }

        public async Task<PagedResultDto<GetStudentForViewDto>> GetAll(GetAllStudentInput input)
        {
            var queryInput = new QueryInput()
            {
                Input = input
            };
            var objQuery = StudentQuery(queryInput);

            var pagedAndFilteredStudents = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

            var objs = from o in pagedAndFilteredStudents
                select new GetStudentForViewDto()
                {
                    Student = ObjectMapper.Map<StudentDto>(o)
                };

            var totalCount = await objQuery.CountAsync();
            var res = await objs.ToListAsync();

            return new PagedResultDto<GetStudentForViewDto>(
                totalCount,
                res
            );
        }

        public async Task<GetStudentForEditOutput> GetStudentForEdit(EntityDto input)
        {
            var queryInput = new QueryInput()
            {
                Id = input.Id
            };

            var query = StudentQuery(queryInput);
            var detailQuery = StudentCardQuery(input.Id);

            var student = await query.FirstOrDefaultAsync();

            var output = new GetStudentForEditOutput()
            {
                Student = ObjectMapper.Map<CreateOrEditStudentDto>(student)
            };
            output.Student.StudentDetails = await detailQuery.ToListAsync();

            return output;
        }

        private async Task ValidateDataInput(CreateOrEditStudentDto input)
        {
            var res = await _studentRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId &&
                            o.Code == input.Code)
                .WhereIf(input.Id.HasValue, o => o.Id != input.Id)
                .FirstOrDefaultAsync();
            if (res != null)
                throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
        }

        public async Task CreateOrEdit(CreateOrEditStudentDto input)
        {
            input.TenantId = AbpSession.TenantId;
            input.StudentDetails ??= new List<StudentCardDto>();

            await ValidateDataInput(input);
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(ParkPermissions.Student_Create)]
        protected virtual async Task Create(CreateOrEditStudentDto input)
        {
            EntityFrameworkManager.ContextFactory = _ => _studentRepository.GetDbContext();
            var obj = ObjectMapper.Map<Core.Student.Student>(input);
            await _studentRepository.InsertAndGetIdAsync(obj);

            var studentDetails = ObjectMapper.Map<List<StudentCard>>(input.StudentDetails);
            if (studentDetails.Any())
            {
                foreach (var detail in studentDetails)
                {
                    detail.TenantId = AbpSession.TenantId;
                    detail.StudentId = obj.Id;
                }

                await _studentRepository.GetDbContext().BulkSynchronizeAsync(studentDetails,
                    options => { options.ColumnSynchronizeDeleteKeySubsetExpression = detail => detail.StudentId; });
            }
            else
            {
                await _studentCardRepository.DeleteAsync(o => o.StudentId == obj.Id);
            }
        }

        [AbpAuthorize(ParkPermissions.Student_Edit)]
        protected virtual async Task Update(CreateOrEditStudentDto input)
        {
            EntityFrameworkManager.ContextFactory = _ => _studentRepository.GetDbContext();
            var obj = await _studentRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            ObjectMapper.Map(input, obj);

            var studentDetails = ObjectMapper.Map<List<StudentCard>>(input.StudentDetails);
            if (studentDetails.Any())
            {
                foreach (var detail in studentDetails)
                {
                    detail.TenantId = AbpSession.TenantId;
                    detail.StudentId = obj.Id;
                }

                await _studentRepository.GetDbContext().BulkSynchronizeAsync(studentDetails,
                    options => { options.ColumnSynchronizeDeleteKeySubsetExpression = detail => detail.StudentId; });
            }
            else
            {
                await _studentCardRepository.DeleteAsync(o => o.StudentId == obj.Id);
            }
        }

        [AbpAuthorize(ParkPermissions.Student_Delete)]
        public async Task Delete(EntityDto input)
        {
            var obj = await _studentRepository.FirstOrDefaultAsync(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("NotFound"));
            await _studentRepository.DeleteAsync(input.Id);
            await _studentCardRepository.DeleteAsync(o => o.StudentId == input.Id);
        }
    }
}