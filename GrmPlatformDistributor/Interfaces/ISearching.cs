﻿using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrmPlatformDistributor.Interfaces
{
    public interface ISearching
    {
        Record[] Search(SearchTerm searchTerm);
    }
}
