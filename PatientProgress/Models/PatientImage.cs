using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientProgress.Models
{
    public class PatientImage
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }

        public Patient Patient { get; set; }
    }
}
