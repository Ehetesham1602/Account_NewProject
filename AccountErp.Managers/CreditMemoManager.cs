using AccountErp.Dtos.CreditMemo;
using AccountErp.Factories;
using AccountErp.Infrastructure.DataLayer;
using AccountErp.Infrastructure.Managers;
using AccountErp.Infrastructure.Repositories;
using AccountErp.Models.CreditMemo;
using AccountErp.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountErp.Managers
{
    public class CreditMemoManager : ICreditMemoManager
    {
        private readonly ICreditMemoRepository _creaditmemoRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _userId;
     

        public CreditMemoManager(IHttpContextAccessor contextAccessor,
            ICreditMemoRepository creaditmemoRepository,
            IUnitOfWork unitOfWork)
        {
            _userId = contextAccessor.HttpContext.User.GetUserId();

            _creaditmemoRepository = creaditmemoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CreditMemoAddModel model)
        {
            var count = await _creaditmemoRepository.getCount();

            await _creaditmemoRepository.AddAsync(CreditMemoFactory.Create(model, _userId, count));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<JqDataTableResponse<CreditMemoListItemDto>> GetPagedResultAsync(CreditMemoJqDataTableRequestModel model)
        {
            return await _creaditmemoRepository.GetPagedResultAsync(model);
        }


        public async Task<CreditMemoDetailDto> GetDetailAsync(int id)
        {
            return await _creaditmemoRepository.GetDetailAsync(id);
        }

        public async Task EditAsync(CreditMemoEditModel model)
        {
            var creaditmemo = await _creaditmemoRepository.GetAsync(model.Id);
            CreditMemoFactory.Edit(model, creaditmemo, _userId);
            _creaditmemoRepository.Edit(creaditmemo);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    }