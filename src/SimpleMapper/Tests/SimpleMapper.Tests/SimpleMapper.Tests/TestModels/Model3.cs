using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tests.TestModels
{
    public class Model3
    {
        public int PublicInt { get; set; }
        public string PublicStr { get; set; }
        public Model2 InternalModel { get; set; }
    }
}
