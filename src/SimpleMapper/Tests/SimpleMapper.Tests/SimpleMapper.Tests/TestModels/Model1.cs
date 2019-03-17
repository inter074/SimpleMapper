using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapper.Tests.TestModels
{
    public class Model1
    {
        public int PublicInt { get; set; }
        public string PublicStr { get; set; }
        public string PublicOnlyReadStr { get; private set; }
        public string PublicOnlyWriteStr { private get; set; }
        public Model2 InternalModel { get; set; }
        public IEnumerable<Model3> InternalModelArray { get; set; }

        public void SetPublicOnlyReadStr(string str)
        {
            PublicOnlyReadStr = str;
        }

        public string GetPublicOnlyWriteStr(string str)
        {
            return PublicOnlyWriteStr;
        }
    }
}
