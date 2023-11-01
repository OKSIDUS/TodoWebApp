using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp.Services;
public class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int TodoListId { get; set; }

    public bool IsDone { get; set; }
}
