using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class Count
    {
        private int common;
        private int successed;

        public Count(int common, int successed)
        {
            this.common = common;
            this.successed = successed;
        }

        public int Common
        {
            get { return common; }
        }

        public int Successed
        {
            get { return successed; }
        }

        public int Failed
        {
            get { return common - successed; }
        }

        public void CommonTick()
        {
            common++;
        }

        public void SuccessTick()
        {
            successed++;
        }
    }
}
