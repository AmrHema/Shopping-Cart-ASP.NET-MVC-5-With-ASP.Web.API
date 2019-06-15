using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.DataTables
{
    public class DataTableResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object[] data { get; set; }
        public string error { get; set; }
    }
}