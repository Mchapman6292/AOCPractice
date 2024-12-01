using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCPractice.AppLoggers
{

    public class AppLogger
    {
        private readonly Serilog.ILogger _logger;
        public AppLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Debug(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: @"C:\Users\mchap\source\repos\AOCPractice\AOCDay4Part1.log",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day
                )
                .CreateLogger();
        }

        public void LogCharRecord(CharRecord record)
        {
            _logger.Information("CharRecord - Symbol: {Symbol}, Index: {Index}, IsChecked: {IsChecked}",
                record.Symbol, record.Index, record.isChecked);
        }

        public void LogDigitRecord(DigitRecord record)
        {
            _logger.Information("DigitRecord - Value: {Value}, StartIndex: {StartIndex}, EndIndex: {EndIndex}, IsAdjacent: {IsAdjacent}",
                record.Value, record.StartIndex, record.EndIndex, record.IsAdjacent);
        }

        public void LogCharRecords(IEnumerable<CharRecord> records)
        {
            _logger.Information("Logging CharRecord Collection with {Count} records", records.Count());
            foreach (var record in records)
            {
                LogCharRecord(record);
            }
        }

        public void LogDigitRecords(IEnumerable<DigitRecord> records)
        {
            _logger.Information("Logging DigitRecord Collection with {Count} records", records.Count());
            foreach (var record in records)
            {
                LogDigitRecord(record);
            }
        }


        public void LogList<T>(List<T> items)
        {
            if (!items.Any())
            {
                _logger.Information("list is empty");
                return;
            }

            var itemStrings = items.Select(item => item?.ToString() ?? "null");
            _logger.Information("List values: {Values}", string.Join(", ", itemStrings));
        }



        public void LogRecord<T>(IEnumerable<T> items, string title = "Items")
        {
            if (!items.Any())
            {
                _logger.Information($"{title} list is empty");
                return;
            }

            _logger.Information($"{title}:");
            foreach (var item in items)
            {
                if (item == null)
                {
                    _logger.Information("    null");
                    continue;
                }

                var properties = typeof(T).GetProperties()
                    .Select(p => $"{p.Name}: {p.GetValue(item)}")
                    .Concat(typeof(T).GetFields()
                        .Select(f => $"{f.Name}: {f.GetValue(item)}"));

                _logger.Information("    [{Properties}]", string.Join(", ", properties));
            }
        }


 



        public void Debug(string message) => _logger.Debug(message);
        public void Debug(string message, params object[] propertyValues) => _logger.Debug(message, propertyValues);
        public void Info(string message) => _logger.Information(message);
        public void Info(string message, params object[] propertyValues) => _logger.Information(message, propertyValues);
    }
}

