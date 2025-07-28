using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientProgress.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<PatientImage> Images { get; set; }
    }
}
