using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace linhang.Controls
{
    public class ContainerBox:Viewbox
    {
        private int _Index;

        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
    }
}
