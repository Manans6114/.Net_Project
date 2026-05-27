using System;
using System.Collections.Generic;
using System.Text;

namespace First_Appli.Common.utilities
{
    public static class AppConstants
    {
        public const string GetAllEmployees = "usp_tbl_Employee_GetAll";
        public const string GetEmployeeById = "usp_tbl_Employee_GetById";
        public const string InsertEmployee = "usp_tbl_Employee_Insert";
        public const string UpdateEmployee = "usp_tbl_Employee_Update";
        public const string DeleteEmployee = "usp_tbl_Employee_Delete";

        public const string Id = "@Id";
        public const string Name = "@Name";
        public const string Department = "@Department";
    }
}