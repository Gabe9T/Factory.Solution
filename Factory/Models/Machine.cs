using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public ICollection<EngineerMachine> EngineerMachines { get; set; }
    }
}