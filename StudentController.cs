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
            
            if(StudentDataSimulation.StudentList.Count==0)
            {
                return NotFound("No students found.");
            }
            var averageGrade = StudentDataSimulation.StudentList.Average(student => student.Grade);
            return Ok(averageGrade);
        }


        [HttpGet("{id}",Name = "GetStudentByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if(id<1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            var student = StudentDataSimulation.StudentList.FirstOrDefault(s => s.Id == id)
                ;
            if(student==null)
            {
                return NotFound($"student with ID {id} not found. ");

            }
            return Ok(student);
        }

        [HttpPost(Name = "Add Student")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> AddStudent(Student newStudent)

        {
            if(newStudent==null||string.IsNullOrEmpty(newStudent.Name)||newStudent.Age<0||newStudent.Grade<0)
            {
                return BadRequest("invalid student data");
            }
            newStudent.Id = StudentDataSimulation.StudentList.Count > 0 ? StudentDataSimulation.StudentList.Max(S=>S.Id) + 1 : 1;
            StudentDataSimulation.StudentList.Add(newStudent);
            return CreatedAtRoute("GetStudentByID", new { id = newStudent.Id }, newStudent);
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not eccepted ID {id}");
            }
            var student = StudentDataSimulation.StudentList.FirstOrDefault(s => s.Id == id);
            if(student ==null)
            {
                return NotFound($"student with id {id} not found.");

            }
            StudentDataSimulation.StudentList.Remove(student);
            return Ok($"Student with ID {id} has been removed ");
        }
        [HttpPut("{id}", Name = "EditStudent")]
        public ActionResult EditStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            var student = StudentDataSimulation.StudentList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"student with id {id} not found.");
            }
            student.Name = "omer";
            student.Age = 25;
            student.Grade = 85;
            return Ok(student);
        }
    }
}
