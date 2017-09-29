using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainModel.Models;
using GrmPlatformDistributor.Concretes;
using GrmPlatformDistributor.Interfaces;
using System.Globalization;
using GrmPlatformDistributor;

namespace GrmPlatformTest
{
    [TestClass]
    public class GrmPlatformTest
    {
        /*
        Tinie Tempah|Frisky (Live from SoHo)|digital download, streaming|01 Feb 2012|
        Tinie Tempah|Miami 2 Ibiza|digital download|01 Feb 2012|
        Tinie Tempah|Till I'm Gone|digital download|01 Aug 2012|
        Monkey Claw|Black Mountain|digital download|01 Feb 2012|
        Monkey Claw|Iron Horse|digital download, streaming|01 June 2012|
        Monkey Claw|Motor Mouth|digital download, streaming|01 Mar 2011|
        Monkey Claw|Christmas Special|streaming|25 Dec 2012|31 Dec 2012
        */
        private readonly Record[] _records = new Record[]{
            new Record { Artist ="Tinie Tempah", Title="Frisky (Live from SoHo)", Usages=new RecordAccess[]{RecordAccess.Digital_Download,RecordAccess.Streaming },StartDate =DateTime.Parse("01 Feb 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Tinie Tempah", Title="Miami 2 Ibiza", Usages=new RecordAccess[]{RecordAccess.Digital_Download },StartDate =DateTime.Parse("01 Feb 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Tinie Tempah", Title="Till I'm Gone", Usages=new RecordAccess[]{RecordAccess.Digital_Download},StartDate =DateTime.Parse("01 Aug 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Monkey Claw", Title="lack Mountain", Usages=new RecordAccess[]{RecordAccess.Digital_Download },StartDate =DateTime.Parse("01 Feb 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Monkey Claw", Title="Iron Horse", Usages=new RecordAccess[]{RecordAccess.Digital_Download,RecordAccess.Streaming },StartDate =DateTime.Parse("01 June 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Monkey Claw", Title="Motor Mouth", Usages=new RecordAccess[]{RecordAccess.Digital_Download,RecordAccess.Streaming },StartDate =DateTime.Parse("01 Mar 2011", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
            new Record { Artist ="Monkey Claw", Title="Christmas Special", Usages=new RecordAccess[]{RecordAccess.Streaming },StartDate =DateTime.Parse("31 Dec 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })},
        };
        private ISearching _itunesStrategy = new ITunesStrategy("dummy/filePath");
        private ISearching _youTubeStrategy = new YouTubeStrategy("dummy/filePath");
        
        [TestInitialize()]
        public void Setup()
        {
            (_itunesStrategy as ITunesStrategy).ITunesRecords = _records;
            (_youTubeStrategy as YouTubeStrategy).YouTubeRecords = _records;
        }
        [TestMethod]
        public void Test_ISearchOn_YouTube_Returns_Correct_Count_Of_Data()
        {
            var records = _youTubeStrategy.Search(new SearchTerm { Distributor = "YouTube", StartDate = DateTime.Parse("01 Feb 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" })});
            Assert.AreEqual(2,records.Length);
        }

        [TestMethod]
        public void Test_ISearchOn_Itunes_Returns_Correct_Count_Of_Data()
        {
            var records = _itunesStrategy.Search(new SearchTerm { Distributor = "ITunes", StartDate = DateTime.Parse("01 June 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }) });
            Assert.AreEqual(5, records.Length);
        }
        [TestMethod]
        public void Test_IPrintOn_YouTube_Prints_Correctly()
        {
            var printedRecord = Program.FormatRecordForPrint(new Record { Artist = "Tinie Tempah", Title = "Frisky (Live from SoHo)", Usages = new RecordAccess[] { RecordAccess.Digital_Download, RecordAccess.Streaming }, StartDate = DateTime.Parse("01 Feb 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }) });
            Assert.AreEqual("Tinie Tempah|Frisky (Live from SoHo)|digital download, streaming|01 Feb 2012|".ToLower(),printedRecord.ToLower());
        }

        [TestMethod]
        public void Test_IPrintOn_Itunes_Prints_Correcty()
        {
            var printedRecord = Program.FormatRecordForPrint(new Record { Artist = "Monkey Claw", Title = "Christmas Special", Usages = new RecordAccess[] { RecordAccess.Streaming }, StartDate = DateTime.Parse("25 Dec 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }),EndDate = DateTime.Parse("31 Dec 2012", new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }) });
            Assert.AreEqual("Monkey Claw|Christmas Special|streaming|25 Dec 2012|31 Dec 2012".ToLower(),printedRecord.ToLower());
        }


    }
}
