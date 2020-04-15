using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.Model
{
    public partial class numeroscalzado
    {
        public int GetTotalItems()
        {
            return C36 + C37 + C38 + C39 + C40 + C41 + C42 + C43 + C44 + C45 + C46 + C47;
        }
    }

    public partial class tallastextiles
    {
        public int GetTotalItems()
        {
            return XXS + XS + S + M + L + XL + XXL;
        }
    }
}
