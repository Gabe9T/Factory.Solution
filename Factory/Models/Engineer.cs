using System.Collections.Generic;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public List<MachineEngineer> JoinEntities { get; set; }
    }
}