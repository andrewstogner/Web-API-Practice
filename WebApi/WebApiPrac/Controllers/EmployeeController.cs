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
using System.Web;

namespace WebApiPrac.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            /*Stored procedure:
            CREATE PROCEDURE GetEmployee
            AS
            BEGIN
            Select EmployeeId, EmployeeName, Department, DateOfJoining, PhotoFileName from dbo.Employee
            END*/

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand("GetEmployee", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            /*Stored procedure:
            CREATE PROCEDURE PostEmployee (@EmployeeName VARCHAR(500), @Department VARCHAR(500), @DateOfJoining DATE, @PhotoFileName VARCHAR(500))
            AS
            BEGIN
            Insert into dbo.Employee Values (@EmployeeName, @Department, @DateOfJoining, @PhotoFileName)
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("PostEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeName", emp.EmployeeName));
                    cmd.Parameters.Add(new SqlParameter("@Department", emp.Department));
                    cmd.Parameters.Add(new SqlParameter("@DateOfJoining", emp.DateofJoining));
                    cmd.Parameters.Add(new SqlParameter("@PhotoFileName", emp.PhotoFileName));
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

        public string Put(Employee emp)
        {
            /*Stored procedure:
            CREATE PROCEDURE PutEmployee (@EmployeeId INT, @EmployeeName VARCHAR(500), @Department VARCHAR(500), @DateOfJoining DATE, @PhotoFileName VARCHAR(500))
            AS
            BEGIN
            Update dbo.Employee 
            Set EmployeeName = @EmployeeName, Department = @Department, DateOfJoining = @DateOfJoining, PhotoFileName = @PhotoFileName
            Where EmployeeId = @EmployeeId
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("PutEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", emp.EmployeeId));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeName", emp.EmployeeName));
                    cmd.Parameters.Add(new SqlParameter("@Department", emp.Department));
                    cmd.Parameters.Add(new SqlParameter("@DateOfJoining", emp.DateofJoining));
                    cmd.Parameters.Add(new SqlParameter("@PhotoFileName", emp.PhotoFileName));
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
            CREATE PROCEDURE DelEmployee @EmployeeId INT
            AS
            BEGIN
            Delete From dbo.Employee
            Where EmployeeId = @EmployeeId
            END*/

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand("DelEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", id));
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

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            /*Stored procedure:
            CREATE PROCEDURE GetAllDepartmentNames
            AS
            BEGIN
            Select DepartmentName from dbo.Department
            END*/

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand("GetAllDepartmentNames", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var filePosted = httpRequest.Files[0];
                string filename = filePosted.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                filePosted.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
