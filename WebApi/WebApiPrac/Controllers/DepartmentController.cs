using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiPrac.Models;

namespace WebApiPrac.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            /*Stored procedure:
            CREATE PROCEDURE GetDepartment
            AS
            BEGIN
            Select DepartmentId, DepartmentName from dbo.Department
            END*/

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand("GetDepartment", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dep)
        {
            /*Stored procedure:
            CREATE PROCEDURE PostDepartment @DepartmentName VARCHAR(500)
            AS
            BEGIN
            Insert into dbo.Department Values (@DepartmentName)
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("PostDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DepartmentName", dep.DepartmentName));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return "Add Successful";
            }
            catch (Exception)
            {
                return "Add Failed";
            }
        }

        public string Put(Department dep)
        {
            /*Stored procedure:
            CREATE PROCEDURE PutDepartment (@DepartmentId INT, @DepartmentName VARCHAR(500))
            AS
            BEGIN
            Update dbo.Department Set DepartmentName = @DepartmentName
            Where DepartmentId = @DepartmentId
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("PutDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", dep.DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentName", dep.DepartmentName));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return "Update Successful";
            }
            catch (Exception)
            {
                return "Update Failed";
            }
        }

        public string Delete(int id)
        {

            /*Stored procedure:
            CREATE PROCEDURE DelDepartment @DepartmentId INT
            AS
            BEGIN
            Delete From dbo.Department
            Where DepartmentId = @DepartmentId
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("DelDepartment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", id));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return "Delete Successful";
            }
            catch (Exception)
            {
                return "Delete Failed";
            }
        }
    }
}
