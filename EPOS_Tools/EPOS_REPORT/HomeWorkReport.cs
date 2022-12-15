using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS_Tools.EPOS_REPORT
{
    internal class HomeWorkReport
    {
        public string caption { get; set; }
        public string baseCaption { get; set; }
        public List<List<Cell>> cells { get; set; }

    }

    class Cell
    {
        public string value { get; set; }
        public string type { get; set; }
        public int rowSpan { get; set; }
        public int colSpan { get; set; }
        public bool isHTML { get; set; }
        public int inputType { get; set; }
        public bool allowEdit { get; set; }
    }
}
