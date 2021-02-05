using AccountErp.Dtos.CreditMemo;
using AccountErp.Entities;
using AccountErp.Models.CreditMemo;
using AccountErp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountErp.Infrastructure.Repositories
{
  public  interface ICreditMemoRepository
    {
        Task AddAsync(CreditMemo entity);
        Task<int> getCount();
        void Edit(CreditMemo entity);

        Task<JqDataTableResponse<CreditMemoListItemDto>> GetPagedResultAsync(CreditMemoJqDataTableRequestModel model);
        Task<CreditMemoDetailDto> GetDetailAsync(int id);


    }
}
