using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIs.Models
{
    public class Department
    {
        [Key]
        public string Dept_ID { get; set; }
        public string Dept_Initial { get; set; }
        public string Dept_Name { get; set; }
    }
}
