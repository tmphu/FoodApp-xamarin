using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an
{
    public class NavPageFlyoutMenuItem
    {
        public NavPageFlyoutMenuItem()
        {
            TargetType = typeof(NavPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}