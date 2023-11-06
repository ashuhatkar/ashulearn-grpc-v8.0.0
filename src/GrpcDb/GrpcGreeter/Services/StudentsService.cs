using Grpc.Core;
using GrpcGreeter.Data;
using GrpcGreeter.Protos;

namespace GrpcGreeter.Services
{
    /// <summary>
    /// Represents a grpc student service
    /// </summary>
    public partial class StudentsService : RemoteStudent.RemoteStudentBase
    {
        #region Fields

        private readonly ILogger<StudentsService> _logger;
        private readonly SchoolDbContext _schoolContext;

        #endregion

        #region Ctor

        public StudentsService(ILogger<StudentsService> logger,
            SchoolDbContext schoolContext)
        {
            _logger = logger;
            _schoolContext = schoolContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets students info
        /// </summary>
        /// <param name="request">Student request</param>
        /// <param name="context">Server call context</param>
        /// <returns>Student model</returns>
        public override async Task<StudentModel> GetStudentInfo(StudentLookupModel request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call StudentsService.GetStudentInfo for student id {id}", request.StudentId);

            if (request.StudentId <= 0)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be > 0 (received {request.StudentId})");
                return null;
            }

            var student = await _schoolContext.Students.FindAsync(request.StudentId);

            _logger.LogInformation("Sending Student response");

            if (student != null)
            {
                return new StudentModel()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    School = student.School
                };
            }

            context.Status = new Status(StatusCode.NotFound, $"Student with id {request.StudentId} do not exist");
            return null;
        }

        #endregion
    }
}