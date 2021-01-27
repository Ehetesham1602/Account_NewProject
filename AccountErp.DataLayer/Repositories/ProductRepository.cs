using AccountErp.Dtos;
using AccountErp.Dtos.Item;
using AccountErp.Dtos.Product;
using AccountErp.Entities;
using AccountErp.Infrastructure.Repositories;
using AccountErp.Models.Product;
using AccountErp.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace AccountErp.DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(Product entity)
        {
            await _dataContext.AddAsync(entity);
        }

        public void Edit(Product entity)
        {
            _dataContext.Update(entity);
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _dataContext.Product.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAsync(List<int> itemIds)
        {
            return await _dataContext.Product.Include(x => x.SalesTax).Where(x => itemIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<ProductDetailDto> GetDetailAsync(int id)
        {
            return await (from s in _dataContext.Product
                          where s.Id == id
                          select new ProductDetailDto
                          {
                              Id = s.Id,
                              Name = s.Name,
                              BuyingPrice = s.BuyingPrice,
                              SellingPrice = s.SellingPrice,
                              InitialStock = s.InitialStock,
                              Description = s.Description,
                              IsTaxable = s.IsTaxable,
                              TaxCode = s.SalesTax.Code,
                              Status = s.Status,
                              BankAccountId = s.BankAccountId,
                              ProductCategoryId = s.ProductCategoryId,
                              CayegoryName = s.Category.Name
                          })
                          .AsNoTracking()
                          .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductDetailDto>> GetAllAsync(Constants.RecordStatus? status = null)
        {
            return await (from s in _dataContext.Product
                          join c in _dataContext.SalesTaxes
                          on s.SalesTaxId equals c.Id
                         into groupjoin_Sales
                          from c in groupjoin_Sales.DefaultIfEmpty()
                          where status == null
                            ? s.Status != Constants.RecordStatus.Deleted
                            : s.Status == status.Value
                          orderby s.Name
                          select new ProductDetailDto
                          {
                              Id = s.Id,
                              Name = s.Name,
                              BuyingPrice = s.BuyingPrice,
                              SellingPrice = s.SellingPrice,
                              InitialStock = s.InitialStock,
                              Description = s.Description,
                              IsTaxable = s.IsTaxable,
                              TaxCode = s.SalesTax.Code,
                              TaxPercentage = s.SalesTax.TaxPercentage,
                              Status = s.Status,
                              SalesTaxId = s.SalesTaxId,
                              BankAccountId = s.BankAccountId,
                              TaxBankAccountId = c.BankAccountId,
                              ProductCategoryId = s.ProductCategoryId,
                              CayegoryName = s.Category.Name
                          })
                          .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<ProductDetailForEditDto> GetForEditAsync(int id)
        {
            return await (from s in _dataContext.Product
                          where s.Id == id
                          select new ProductDetailForEditDto
                          {
                              Id = s.Id,
                              Name = s.Name,
                              Description = s.Description,
                              IsTaxable = s.IsTaxable ? "1" : "0",
                              SalesTaxId = s.SalesTaxId,
                              BuyingPrice = s.BuyingPrice,
                              SellingPrice = s.SellingPrice,
                              InitialStock = s.InitialStock,
                              BankAccountId = s.BankAccountId,
                              ProductCategoryId = s.ProductCategoryId,
                              CayegoryName = s.Category.Name
                          })
                         .AsNoTracking()
                         .SingleOrDefaultAsync();
        }

        public async Task<JqDataTableResponse<ProductListItemDto>> GetPagedResultAsync(ProductJqDataTableRequestModel model)
        {
            if (model.Length == 0)
            {
                model.Length = Constants.DefaultPageSize;
            }

            var filterKey = model.Search.Value;

            var linqStmt = (from s in _dataContext.Product
                            where s.Status != Constants.RecordStatus.Deleted
                                && (model.FilterKey == null
                                || EF.Functions.Like(s.Name, "%" + model.FilterKey + "%")
                                || EF.Functions.Like(s.Description, "%" + model.FilterKey + "%"))
                            select new ProductListItemDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                BuyingPrice = s.BuyingPrice,
                                SellingPrice = s.SellingPrice,
                                InitialStock = s.InitialStock,
                                Description = s.Description ?? "",
                                Status = s.Status,
                                TaxCode = s.SalesTax.Code,
                                TaxPercentage = s.SalesTax.TaxPercentage,
                                BankAccountId = s.BankAccountId,
                                ProductCategoryId = s.ProductCategoryId,
                                CayegoryName = s.Category.Name,
                                CreatedOn=s.CreatedOn
                                
                                
                            })
                            .AsNoTracking();

            var sortExpresstion = model.GetSortExpression();

            var pagedResult = new JqDataTableResponse<ProductListItemDto>
            {
                RecordsTotal = await _dataContext.Product.CountAsync(x => x.Status != Constants.RecordStatus.Deleted),
                RecordsFiltered = await linqStmt.CountAsync(),
                Data = await linqStmt.OrderBy(sortExpresstion).Skip(model.Start).Take(model.Length).ToListAsync()
            };
            return pagedResult;
        }

        public async Task<JqDataTableResponse<ProductListItemDto>> GetInventoryPagedResultAsync(ProductInventoryJqDataTableRequestModel model)
        {
            if (model.Length == 0)
            {
                model.Length = Constants.DefaultPageSize;
            }

            var filterKey = model.Search.Value;

            var linqStmt = (from s in _dataContext.Product
                            where s.Status != Constants.RecordStatus.Deleted
                                && (model.FilterKey == null
                                || EF.Functions.Like(s.Name, "%" + model.FilterKey + "%")
                                || EF.Functions.Like(s.Description, "%" + model.FilterKey + "%"))
                                && (s.CreatedOn>=model.StartDate && s.CreatedOn<=model.EndDate)
                            select new ProductListItemDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                BuyingPrice = s.BuyingPrice,
                                SellingPrice = s.SellingPrice,
                                InitialStock = s.InitialStock,
                                Description = s.Description ?? "",
                                Status = s.Status,
                                TaxCode = s.SalesTax.Code,
                                TaxPercentage = s.SalesTax.TaxPercentage,
                                BankAccountId = s.BankAccountId,
                                ProductCategoryId = s.ProductCategoryId,
                                CayegoryName = s.Category.Name,
                                CreatedOn = s.CreatedOn


                            })
                            .AsNoTracking();

            var sortExpresstion = model.GetSortExpression();

            var pagedResult = new JqDataTableResponse<ProductListItemDto>
            {
                RecordsTotal = await _dataContext.Product.CountAsync(x => x.Status != Constants.RecordStatus.Deleted),
                RecordsFiltered = await linqStmt.CountAsync(),
                Data = await linqStmt.OrderBy(sortExpresstion).Skip(model.Start).Take(model.Length).ToListAsync()
            };
            return pagedResult;
        }


        public async Task<IEnumerable<SelectListItemDto>> GetSelectItemsAsync()
        {
            return await _dataContext.Product
                .AsNoTracking()
                .Where(x => x.Status == Constants.RecordStatus.Active)
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItemDto
                {
                    KeyInt = x.Id,
                    Value = x.Name
                }).ToListAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var vendor = await _dataContext.Items.FindAsync(id);

            if (vendor.Status == Constants.RecordStatus.Active)
            {
                vendor.Status = Constants.RecordStatus.Inactive;
            }
            else if (vendor.Status == Constants.RecordStatus.Inactive)
            {
                vendor.Status = Constants.RecordStatus.Active;
            }

            _dataContext.Items.Update(vendor);
        }

        public bool checkItemAvailable(int id)
        {
            var invoice_ids = _dataContext.InvoiceServices.Where(x => x.ServiceId == id).Select(x => x.InvoiceId).ToList();
            var bill_ids = _dataContext.BillItems.Where(x => x.ItemId == id).Select(x => x.BillId).ToList();
            var quot_ids = _dataContext.QuotationServices.Where(x => x.ServiceId == id).Select(x => x.QuotationId).ToList();

            string msg = string.Empty;
            bool isvalid = true;

            foreach (int invoiceid in invoice_ids)
            {
                var invoice = _dataContext.Invoices.Where(x => x.Id == invoiceid && x.Status != Constants.InvoiceStatus.Deleted).FirstOrDefault();
                if (invoice != null)
                {
                    isvalid = false;

                }
            }

            foreach (int billid in bill_ids)
            {
                var invoice = _dataContext.Bills.Where(x => x.Id == billid && x.Status != Constants.BillStatus.Deleted).FirstOrDefault();
                if (invoice != null)
                {
                    isvalid = false;

                }
            }
            foreach (int quot_id in quot_ids)
            {
                var invoice = _dataContext.Quotations.Where(x => x.Id == quot_id && x.Status != Constants.InvoiceStatus.Deleted).FirstOrDefault();
                if (invoice != null)
                {
                    isvalid = false;

                }
            }
            return isvalid;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dataContext.Product.FindAsync(id);
            item.Status = Constants.RecordStatus.Deleted;
            _dataContext.Product.Update(item);

        }


    }
}
