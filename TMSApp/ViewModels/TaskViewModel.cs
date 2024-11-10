using System;
using System.Collections.Generic;
using System.Text;

namespace TMSApp.ViewModels
{
    public class TaskViewModel
    {
            public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsActive { get; set; }
     public string Status => IsActive ? "Active" : "Completed";

    }
}
