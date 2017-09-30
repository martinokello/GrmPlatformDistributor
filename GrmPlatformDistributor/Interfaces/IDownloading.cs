using FileDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrmPlatformDistributor.Interfaces
{
    public interface IDownloading: IPrinting
    {
        void DoDownload();
    }
}
