using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tests.TestModels
{
    public class Model2
    {
        public int PublicInt { get; set; }
        public string PublicStr { get; set; }
        public string ValueOfAllFilledInProperties { get; set; }
        public Model3 InternalModel { get; set; }
        public string PublicOnlyReadStr { get; private set; }
        public string PublicOnlyWriteStr { private get; set; }
        public IEnumerable<Model3> InternalModelArray { get; set; }

        public string GetPublicOnlyWriteStr()
        {
            return PublicOnlyWriteStr;
        }
    }
}
