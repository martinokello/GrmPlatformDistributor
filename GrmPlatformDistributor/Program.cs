using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GrmPlatformDistributor.Concretes;
using FileDataAccess.Interfaces;
using System.Globalization;
using DomainModel.Models;

namespace GrmPlatformDistributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            var filePathToData = @"c:\DataFile\DistributionList.txt";

            var userInput = Console.ReadLine();
            var searchTerms = SearchTermsSplit(userInput);
            if (searchTerms.Length != 2) throw new Exception("Search Term not correct!!");
            var searchObject = new SearchTerm { Distributor = searchTerms[0], StartDate = DateTime.Parse(searchTerms[1], new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }) };

            IPrinting youTubeStrategy = new YouTubeStrategy(filePathToData);
            IPrinting iTunesStrategy = new ITunesStrategy(filePathToData);
            Record[] results = null;
            Console.Out.WriteLine("Artist|Title|Usage|StartDate|EndDate");
            switch (searchObject.Distributor.ToLower())
            {
                case "itunes":
                    results = (iTunesStrategy as Interfaces.ISearching).Search(searchObject);
                    foreach (var result in results)
                    {
                        string printRec = FormatRecordForPrint(result);
                        iTunesStrategy.DoPrint(printRec);
                    }
                    break;
                case "youtube":
                    results = (youTubeStrategy as Interfaces.ISearching).Search(searchObject);
                    foreach (var result in results)
                    {
                        string printRec = FormatRecordForPrint(result);
                        youTubeStrategy.DoPrint(printRec);
                    }
                    break;
            }
        }
        public static string[] SearchTermsSplit(string input)
        {
            var firstIndexOfSpace = input.IndexOf(' ');
            return new string[] { input.Substring(0, firstIndexOfSpace), input.Substring(firstIndexOfSpace + 1) };
        }

        public static string FormatRecordForPrint(Record record)
        {
            var recordUsages = string.Empty;
            foreach (var us in record.Usages)
            {
                recordUsages += (us.ToString() + ", ");
            }
            return string.Format("{0}|{1}|{2}|{3}", record.Artist, record.Title, recordUsages.Replace('_', ' ').Trim(new char[] { ' ',',' }), record.StartDate.ToString("dd MMM yyyy"))+ "|" + (record.EndDate > DateTime.Now.AddYears(-90) ?  record.EndDate.ToString("dd MMM yyyy") : "");
        }
    }
}
