using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day01
{
    public class Elv
    {
        public List<long> Load { get; set; } = new List<long>();
        public long TotalLoad { get { return Load.Sum(); } }
    }
}
