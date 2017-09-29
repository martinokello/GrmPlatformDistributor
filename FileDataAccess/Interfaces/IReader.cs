using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataAccess.Interfaces
{
    public interface IReader
    {
        Record[] ReadFile(string filePath);
    }
}
