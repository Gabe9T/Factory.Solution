using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<EngineerMachine> EngineerMachines { get; set; }
    }
}