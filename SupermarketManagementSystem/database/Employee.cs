﻿using SupermarketManagementSystem.database.sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketManagementSystem.database
{
	class Employee
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int emp_no { get; set; }
		public string username { get; set; }
		public string password { get; set; }

		public virtual ICollection<CashSale> CashSale { get; set; }
		public virtual ICollection<DebitSale> DebithSale { get; set; }

		public static Employee getEmployee(string dusername, string dpassword) {

			Employee employeeInfo;

			using (var context = new MngContext()) {
				
				employeeInfo = context.Employees.SqlQuery("Select emp_no, username, password from Employees where username=@usr and password=@pwd", new SqlParameter("@usr", dusername), new SqlParameter("@pwd", dpassword)).FirstOrDefault<Employee>();

			}

			return employeeInfo;
		}

		public static Employee getEmployeebyUsName(string dusername)
		{

			Employee employeeInfo;

			using (var context = new MngContext())
			{

				employeeInfo = context.Employees.SqlQuery("Select emp_no, username, password from Employees where username=@usr", new SqlParameter("@usr", dusername)).FirstOrDefault<Employee>();
			}

			return employeeInfo;
		}

		public static void setEmployee(string dusername, string dpassword) {


			if (getEmployee(dusername, dpassword) == null) 
			{ //eklenecek çalışanın database de olup olmadığınıa bakılır. 
			
				using (var context = new MngContext())
				{

					Employee e = new Employee
					{
						username = dusername,
						password = dpassword,
					};
					
					context.Employees.Add(e);
					context.SaveChanges();

				}

			}

		}

	}
}
