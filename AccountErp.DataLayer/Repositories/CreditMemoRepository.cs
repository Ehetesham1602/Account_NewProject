using AccountErp.Dtos.CreditMemo;
using AccountErp.Entities;
using AccountErp.Infrastructure.Repositories;
using AccountErp.Models.CreditMemo;
using AccountErp.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace AccountErp.DataLayer.Repositories
{
  public  class CreditMemoRepository : ICreditMemoRepository
    {
        private readonly DataContext _dataContext;

        public CreditMemoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(CreditMemo entity)
        {
            await _dataContext.CreditMemo.AddAsync(entity);
        }

        public async Task<int> getCount()
        {
            int count = await _dataContext.CreditMemo.CountAsync();
            return count;
        }

        public void Edit(CreditMemo entity)
        {

            _dataContext.CreditMemo.Update(entity);
        }

        public async Task<JqDataTableResponse<CreditMemoListItemDto>> GetPagedResultAsync(CreditMemoJqDataTableRequestModel model)
        {
            if (model.Length == 0)
            {
                model.Length = Constants.DefaultPageSize;
            }

            var linqstmt = (from i in _dataContext.CreditMemo
                            join c in _dataContext.Customers
                            on i.CustomerId equals c.Id
                            where (model.CustomerId == null
                                    || i.CustomerId == model.CustomerId.Value)
                                && (model.FilterKey == null
                                    || EF.Functions.Like(c.FirstName, "%" + model.FilterKey + "%")
                                     || EF.Functions.Like(c.MiddleName, "%" + model.FilterKey + "%")
                                    || EF.Functions.Like(c.LastName, "%" + model.FilterKey + "%")
                                     || EF.Functions.Like(i.InvoiceNumber, "%" + model.FilterKey + "%"))
                            && i.Status != Constants.InvoiceStatus.Deleted
                            select new CreditMemoListItemDto
                            {
                                Id = i.Id,
                                CustomerId = i.CustomerId,
                                CustomerName = (c.FirstName ?? "") + " " + (c.MiddleName ?? "") + " " + (c.LastName ?? ""),
                                Description = i.Remark,
                                Amount = i.CreditMemoService.Sum(x => x.Rate),
                                Discount = i.Discount,
                                Tax = i.Tax,
                                TotalAmount = i.TotalAmount,
                                CreatedOn = i.CreatedOn,
                                Status = i.Status,
                                InvoiceNumber = i.InvoiceNumber,
                                SubTotal = i.SubTotal,
                                InvoiceId=i.InvoiceId,
                            
                            })
                            .AsNoTracking();

            var sortExpresstion = model.GetSortExpression();

            var pagedResult = new JqDataTableResponse<CreditMemoListItemDto>
            {
                RecordsTotal = await _dataContext.CreditMemo.CountAsync(x => x.Status != Constants.InvoiceStatus.Deleted),
                RecordsFiltered = await linqstmt.CountAsync(),
                Data = await linqstmt.OrderBy(sortExpresstion).Skip(model.Start).Take(model.Length).ToListAsync()
            };

            foreach (var creditMemoListItemDto in pagedResult.Data)
            {
                creditMemoListItemDto.CreatedOn = Utility.GetDateTime(creditMemoListItemDto.CreatedOn, null);
            }

            return pagedResult;
        }
    }
}
