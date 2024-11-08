using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSApp.Views.User
{
    public class UserHomePageFlyoutMenuItem
    {
        public UserHomePageFlyoutMenuItem()
        {
            TargetType = typeof(UserHomePageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}