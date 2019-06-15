using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class ModelC
    {
        public List<ModelA> ListA { get; set; }
        public List<ModelB> ListB { get; set; }
        public int Age { get; set; }
    }
}