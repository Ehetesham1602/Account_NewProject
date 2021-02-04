﻿using AccountErp.Entities;
using AccountErp.Models.CreditMemo;
using AccountErp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace AccountErp.Factories
{
    public class CreditMemoFactory
    {

        public static CreditMemo Create(CreditMemoAddModel model, string userId, int count)
        {
            var creditmemo = new CreditMemo
            {
                CustomerId = model.CustomerId,
                InvoiceNumber = "INV" + "-" + model.InvoiceDate.ToString("yy") + "-" + (count + 1).ToString("000"),
                Tax = model.Tax,
                Discount = model.Discount,
                TotalAmount = model.TotalAmount,
                Remark = model.Remark,
                Status = Constants.InvoiceStatus.Pending,
                CreatedBy = userId ?? "0",
                CreatedOn = Utility.GetDateTime(),
                InvoiceDate = model.InvoiceDate,
                StrInvoiceDate = model.InvoiceDate.ToString("yyyy-MM-dd"),
                DueDate = model.DueDate,
                StrDueDate = model.DueDate.ToString("yyyy-MM-dd"),
                PoSoNumber = model.PoSoNumber,
                SubTotal = model.SubTotal,
                LineAmountSubTotal = model.LineAmountSubTotal,
                InvoiceId = model.InvoiceId,
                //   InvoiceType = model.InvoiceType,
                CreditMemoService = model.CreditMemoService.Select(x => new CreditMemoService
                {
                    Id = Guid.NewGuid(),
                    ServiceId = x.ServiceId,
                    ProductId = x.ProductId,
                    Rate = x.Rate,
                    OldQuantity = x.OldQuantity,
                    NewQuantity = x.NewQuantity,
                    Price = x.Price,
                    TaxId = x.TaxId,
                    TaxPrice = x.TaxPrice,
                    TaxPercentage = x.TaxPercentage,
                    LineAmount = x.LineAmount
                }).ToList()
            };


            return creditmemo;
        }

        public static void Edit(CreditMemoEditModel model, CreditMemo entity, string userId, int count)
        {


            entity.CustomerId = model.CustomerId;
            entity.InvoiceNumber = "INV" + "-" + model.InvoiceDate.ToString("yy") + "-" + (count + 1).ToString("000");
            entity.Tax = model.Tax;
            entity.Discount = model.Discount;
            entity.TotalAmount = model.TotalAmount;
            entity.Remark = model.Remark;
            entity.Status = Constants.InvoiceStatus.Pending;
            entity.CreatedBy = userId ?? "0";
            entity.CreatedOn = Utility.GetDateTime();
            entity.InvoiceDate = model.InvoiceDate;
            entity.StrInvoiceDate = model.InvoiceDate.ToString("yyyy-MM-dd");
            entity.DueDate = model.DueDate;
            entity.StrDueDate = model.DueDate.ToString("yyyy-MM-dd");
            entity.PoSoNumber = model.PoSoNumber;
            entity.SubTotal = model.SubTotal;
            entity.LineAmountSubTotal = model.LineAmountSubTotal;
            entity.InvoiceId = model.InvoiceId;
            //   InvoiceType = model.InvoiceType,
            model.CreditMemoService.Select(x => new CreditMemoService
            {
                Id = Guid.NewGuid(),
                ServiceId = x.ServiceId,
                ProductId = x.ProductId,
                Rate = x.Rate,
                OldQuantity = x.OldQuantity,
                NewQuantity = x.NewQuantity,
                Price = x.Price,
                TaxId = x.TaxId,
                TaxPrice = x.TaxPrice,
                TaxPercentage = x.TaxPercentage,
                LineAmount = x.LineAmount
            }).ToList();








    }
}
}