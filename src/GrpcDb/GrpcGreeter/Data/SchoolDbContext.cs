/*--****************************************************************************
  --* Project Name    : IssueAPI
  --* Reference       : GrpcGreeter.Models, Microsoft.EntityFrameworkCore
  --* Description     : Represents DbContext
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         10/11/23  CR-XXXXX Original
  --****************************************************************************/
using GrpcGreeter.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcGreeter.Data
{
    /// <summary>
    /// Represents a DbContext
    /// </summary>
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(GetStudents());
        }

        public DbSet<Student> Students { get; set; }

        private static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>() {
                new Student() {
                    StudentId = 1,
                    FirstName = "Ann",
                    LastName = "Fox",
                    School = "Nursing"
                },
                new Student() {
                    StudentId = 2,
                    FirstName = "Sam",
                    LastName = "Deo",
                    School = "Mining"
                },
                new Student() {
                    StudentId = 3,
                    FirstName = "Sue",
                    LastName = "Cox",
                    School = "Business"
                },
                new Student() {
                    StudentId = 4,
                    FirstName = "Tim",
                    LastName = "Lee",
                    School = "Computing"
                },
                new Student() {
                    StudentId = 5,
                    FirstName = "Jan",
                    LastName = "Ray",
                    School = "Nursing"
                },
                new Student() {
                    StudentId = 6,
                    FirstName = "Tia",
                    LastName = "Lint",
                    School = "Software"
                },
                new Student() {
                    StudentId = 7,
                    FirstName = "Carl",
                    LastName = "Los",
                    School = "Schooling"
                },
                new Student() {
                    StudentId = 8,
                    FirstName = "Unic",
                    LastName = "Les",
                    School = "Mothering"
                },
                new Student() {
                    StudentId = 9,
                    FirstName = "Ingz",
                    LastName = "Chang",
                    School = "Banking"
                },
                new Student() {
                    StudentId = 10,
                    FirstName = "Teea",
                    LastName = "Santos",
                    School = "Colleging"
                },
                new Student() {
                    StudentId = 11,
                    FirstName = "Test",
                    LastName = "Test",
                    School = "Testing"
                }
            };
            return students;
        }
    }
}