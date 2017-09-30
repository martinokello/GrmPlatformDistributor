using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrmPlatformDistributor.Interfaces;
using DomainModel.Models;
using FileDataAccess.Concretes;

namespace GrmPlatformDistributor.Concretes
{
    public class ITunesStrategy:IStreaming,IDownloading, ISearching
    {
        public Record[] ITunesRecords { get; set; }
        private readonly FileAccess _fileAccess;
        public ITunesStrategy(string filePath)
        {
            _fileAccess = new FileAccess();
            ITunesRecords = _fileAccess.ReadFile(filePath);
        }
        public void DoStream()
        {
            //ITunes does not do Streaming, but could do in the future:
            throw new NotImplementedException();
        }

        public void DoDownload()
        {
            //This function to Download goes here:
        }
        
        public void DoPrint(string lineToPrint)
        {
            _fileAccess.DoPrint(lineToPrint);
        }
       
        public Record[] Search(SearchTerm searchTerm)
        {
            return ITunesRecords.Where(p => p.StartDate <= searchTerm.StartDate && p.Usages.Contains(RecordAccess.Digital_Download)).ToArray();
        }
    }
}
