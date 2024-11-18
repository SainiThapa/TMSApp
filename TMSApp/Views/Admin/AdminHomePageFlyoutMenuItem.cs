using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSApp.Views.Admin
{
    public class AdminHomePageFlyoutMenuItem
    {
        public AdminHomePageFlyoutMenuItem(Type targetType, int id, string title)
        {
            TargetType = targetType;
            Id = id;
            Title = title;
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}