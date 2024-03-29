using System.Collections.Generic;

namespace Factory.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public List<MachineEngineer> JoinEntities { get; set; }
    }
}