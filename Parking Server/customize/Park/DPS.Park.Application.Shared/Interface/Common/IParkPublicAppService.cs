using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DPS.Park.Application.Shared.Dto.Common;
using DPS.Park.Application.Shared.Dto.Contact.UserContact;
using DPS.Park.Application.Shared.Dto.Order;
using DPS.Park.Application.Shared.Dto.Student;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Park.Application.Shared.Interface.Common
{
    public interface IParkPublicAppService: IApplicationService
    {
        #region User
        Task<PagedResultDto<GetOrderForViewDto>> GetMyOrders(ParkPublicInput input);

        #endregion

        #region Student

        Task<StudentDto> GetStudentByUserId(ParkPublicInput input);

        #endregion

        #region Order

        Task<int> CreateOrder(CreateOrEditOrderDto input);

        #endregion

        #region Checkout vnpay

        Task<string> UrlPayment(int typePayment, int orderId);


        #endregion
        
        #region Contact

        Task GetUserContact(CreateOrEditUserContactDto input);

        #endregion
    }
}