using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrmPlatformDistributor.Interfaces;
using DomainModel.Models;
using FileDataAccess.Concretes;
using System.Text.RegularExpressions;

namespace GrmPlatformDistributor.Concretes
{
    public class YouTubeStrategy:IStreaming,IDownloading,ISearching
    {
        public Record[] YouTubeRecords { get; set; }
        private readonly FileAccess _fileAccess;

        public YouTubeStrategy(string filePath)
        {
            _fileAccess = new FileAccess();
            YouTubeRecords = _fileAccess.ReadFile(filePath);
        }
        public void DoPrint(string lineToPrint)
        {
            _fileAccess.DoPrint(lineToPrint);
        }

        public void DoDownload()
        {
            //You Tube does not do downloads, but could do in the future:
            throw new NotImplementedException();
        }

        public void DoStream()
        {
            //Stream Function to Extend App when required.
        }

        public Record[] Search(SearchTerm searchTerm)
        {
            return YouTubeRecords.Where(p => p.StartDate <= searchTerm.StartDate && p.Usages.Contains(RecordAccess.Streaming)).ToArray();
        }
    }
}
