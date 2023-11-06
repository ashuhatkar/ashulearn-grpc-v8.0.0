namespace GrpcGreeter.Models
{
    /// <summary>
    /// Represents student
    /// </summary>
    public partial class Student
    {
        /// <summary>
        /// Gets or sets the student identifier
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or set the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the school
        /// </summary>
        public string School { get; set; }
    }
}