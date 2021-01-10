﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketManagementSystem.database
{
	class MarketDebt : Debt	
	{
		
		[Key]
		public int barcode { get; set; }

		[Required]
		public virtual Product Product { get; set; }

		public static List<MarketDebt> getMDebtnonPayed() 
		{ //toplam market borcu

			List<MarketDebt> debtInfo;

			using (MngContext context = new MngContext())
			{

				debtInfo = context.MarketDebts.SqlQuery("Select amount from Products where payed=false").ToList();

			}

			return debtInfo;

		}

		public static MarketDebt setMDebt(int barcode, float debt_amount,
																			DateTime debt_date, Product prd )
		{
			using (MngContext context = new MngContext())
			{

				MarketDebt m = new MarketDebt
				{
					barcode = barcode,
					debt_amount = debt_amount,
					debt_date = debt_date,
					payed = false, //ürün stoğa eklendiğinde borç oluşur daima
					Product = prd,
				};

				context.MarketDebts.Add(m);
				context.SaveChanges();

				return m;

			}

		}

		public static float totalMDebt()
		{
			List<MarketDebt> marketDebts = getMDebtnonPayed();

			float totalDebt = 0;
			
			foreach (MarketDebt debt in marketDebts)
			{
				//zaten getMDebtnonPayed() metodunda ödenmemiş borçlar alındığı için 
				//borcun ödenip ödenmediği kontrolü yapılmaz.
				totalDebt += debt.debt_amount;

			}

			return totalDebt;

		}

	}

}
