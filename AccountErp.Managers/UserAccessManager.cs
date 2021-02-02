using AccountErp.Dtos.UserAccess;
using AccountErp.Entities;
using AccountErp.Factories;
using AccountErp.Infrastructure.DataLayer;
using AccountErp.Infrastructure.Managers;
using AccountErp.Infrastructure.Repositories;
using AccountErp.Models.UserAccess;
using AccountErp.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountErp.Managers
{
    public class UserAccessManager : IUserAccessMAnager
    {
        private readonly IUserAccessRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _userId;

        public UserAccessManager(IHttpContextAccessor contextAccessor,
          IUserAccessRepository repository,
          IUnitOfWork unitOfWork)
        {
            _userId = contextAccessor.HttpContext.User.GetUserId();

            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddUserScreenAccessAsync(List<ScreenAccessModel> model)
        {
            await _repository.DeleteAsyncUserScreenAccess(model[0].UserRoleId);
            await _unitOfWork.SaveChangesAsync();
            List<UserScreenAccess> item = new List<UserScreenAccess>();
            UserScreenAccessFactory.CreateUserScreenAccess(model, item);
            await _repository.AddUserScreenAccessAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ScreenAccessDto>> GetUserScreenAccessById(int id)
        {
            return await _repository.GetAsyncUserScreenAccess(id);
        }
        public async Task<List<ScreendetailDto>> GetAllScreenDetail()
        {
            return await _repository.GetAllScreenDetail();
        }
    }
}
