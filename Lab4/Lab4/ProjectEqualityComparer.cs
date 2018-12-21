using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>         
/// Клас для порівняння даних         
/// </summary>     
public class ProjectEqualityComparer : IEqualityComparer<Project>
{
    public bool Equals(Project x, Project y)
    {
        bool Result = false;
        if (x.id == y.id && x.name == y.name && x.value == y.value && x.finishDate == y.finishDate && x.startDate == y.startDate && x.listName.Equals(y.listName))
            Result = true;
        return Result;
    }
    public int GetHashCode(Project obj)
    {
        return obj.id;
    }
}