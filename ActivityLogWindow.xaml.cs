using System.Collections.Generic;
using System.Windows;

namespace Prog6221_POE
{
    public partial class ActivityLogWindow : Window
    {
        public ActivityLogWindow(List<string> activity)
        {
            InitializeComponent();

            if (activity == null || activity.Count == 0)
            {
                ActivityList.Items.Add("No recent activity found.");
            }
            else
            {
                foreach (var entry in activity)
                {
                    ActivityList.Items.Add(entry);
                }
            }
        }
    }
}
