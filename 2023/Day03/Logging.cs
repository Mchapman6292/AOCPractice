using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Day03
{
    public class AppLogger
    {
        private ILogger _logger;

        public AppLogger()
        {
            CreateLogger();
        }

        private void CreateLogger()
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
            _logger.Information($"CharRecord - Symbol: {record.Symbol}, Index: {record.Index}, IsChecked: {record.isChecked}");
        }

        public void LogDigitRecord(DigitRecord record)
        {
            _logger.Information($"DigitRecord - Value: {record.Value}, StartIndex: {record.StartIndex}, EndIndex: {record.EndIndex}, IsAdjacent: {record.IsAdjacent}");
        }


        public void ClearLog()
        {
            File.WriteAllText(@"C:\Users\mchap\source\repos\AOCPractice\AOCDay4Part1.log", string.Empty);
            CreateLogger();
        }




        public void LogList(List<Char> items)
        {
            if (!items.Any())
            {
                _logger.Information("list is empty");
                return;
            }

            var itemStrings = items.Select(item => item.ToString());
            _logger.Information($"List values: {string.Join(", ", itemStrings)}");
        }




        public void LogEnum<T>(List<T> items)
        {
            if (!items.Any())
            {
                _logger.Information("list is empty");
                return;
            }

            var itemStrings = items.Select(item => item?.ToString() ?? "null");
            _logger.Information($"List values: {itemStrings}");
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

                var type = typeof(T);
                var properties = type.GetProperties()
                    .Select(p => $"{p.Name}: {p.GetValue(item)}")
                    .Concat(type.GetFields()
                        .Select(f => $"{f.Name}: {f.GetValue(item)}"));

                _logger.Information($"    [{properties}]", string.Join(", ", properties));

                foreach (var property in type.GetProperties())
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listValue = property.GetValue(item);
                        if (listValue is IEnumerable<object> list)
                        {
                            _logger.Information($"    Logging list property: {property.Name}");
                            LogEnum(list.Cast<object>().ToList());
                        }
                    }
                }
            }
        }





        public void Debug(string message) => _logger.Debug(message);
        public void Debug(string message, params object[] propertyValues) => _logger.Debug(message, propertyValues);
        public void Info(string message) => _logger.Information(message);
        public void Info(string message, params object[] propertyValues) => _logger.Information(message, propertyValues);
    }
}


