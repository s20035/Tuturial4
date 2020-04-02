using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using APBDTUT3.DAL;
using APBDTUT3.Models;

namespace APBDTUT3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
       
        [HttpGet]
        public IActionResult GetStudent(String orderby) {

            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s20035;Integrated Security=True")) {
                using (var command = new SqlCommand()) {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester from Student s "+
                                           "join Enrollment e on e.IdEnrollment = s.IdEnrollment "+
                                           "join Studies st on st.IdStudy = e.IdStudy ;";

                    sqlConnection.Open();
                    var response = command.ExecuteReader();

                    while (response.Read()) {
                        var st = new Student();
                        st.Firstname = response["FirstName"].ToString();
                        st.LastName = response["LastName"].ToString();
                        st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        st.Studies = response["Studies"].ToString();
                        st.Semester = int.Parse(response["Semester"].ToString());


                            students.Add(st);
                    }
                }
        }

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult Getsemester(int id) {
            string answer = String.Empty;
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s20035;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select e.Semester from Student s " +
                                           "join Enrollment e on e.IdEnrollment = s.IdEnrollment "+
                                           "where s.IndexNumber like 's" + id +"'"; 
                                          

                    sqlConnection.Open();
                    var response = command.ExecuteReader();

                     answer = response.Read().ToString();
                }
            }

            return Ok("Semester of this student is :" + answer);
        }

    }
}