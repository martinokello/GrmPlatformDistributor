using DomainModel.Models;
using FileDataAccess.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FileDataAccess.Concretes
{
    public class FileAccess:IPrinting,IReader
    {
        public FileAccess()
        {
        }

        public void DoPrint(string lineToPrint)
        {
            Console.Out.WriteLine(lineToPrint);
        }

        public Record[] ReadFile(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var records = new List<Record>();
            if (fileInfo.Exists)
            {
                var streamReader = fileInfo.OpenText();
                //Remove headers:
                streamReader.ReadLine();
                 
                var line = string.Empty;
                while (!string.IsNullOrEmpty(line = streamReader.ReadLine()))
                {
                    try
                    {
                        var lines = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        var actualDistributions = new List<RecordAccess>();
                        var distributions = lines[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var d in distributions)
                        {
                            switch (d.ToLower())
                            {
                                case "digital download":
                                    actualDistributions.Add(RecordAccess.Digital_Download);
                                    break;
                                case "streaming":
                                    actualDistributions.Add(RecordAccess.Streaming);
                                    break;
                            }
                        }
                        var record = new Record
                        {
                            Artist = lines[0],
                            Title = lines[1],
                            Usages = actualDistributions.ToArray(),
                            StartDate = DateTime.Parse(lines[3], new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }),
                            EndDate = (lines.Length > 4 ? DateTime.Parse(lines[4], new DateTimeFormatInfo { FullDateTimePattern = "dd MMM yyyy" }) : DateTime.Now.AddYears(-100))
                        };
                        records.Add(record);

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                streamReader.Close();
            }

            return records.ToArray();
        }
    }
}
