using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DataSimulation;

namespace StudentAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("All",Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok (StudentDataSimulation.StudentList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]

        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentDataSimulation.StudentList.Where(s => s.Grade >= 70).ToList();
            return Ok(passedStudents);
        }

        [HttpGet("AverageGrade",Name = "GetAverageGrade")]
        public ActionResult<double> GetAverageGrade()
        {
            StudentDataSimulation.StudentList.Clear();
            if (StudentDataSimulation.StudentList.Count==0)
            {
                return NotFound("No students found.");
            }
            var averageGrade = StudentDataSimulation.StudentList.Average(student => student.Grade);
            return Ok(averageGrade);
        }


    }
}
