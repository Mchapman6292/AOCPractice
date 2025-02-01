using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Day03.Day03Loggers
{
    public class Day03Logger
    {
        private ILogger day04Logger;

        public Day03Logger()
        {
            CreateLogger();
        }

        private void CreateLogger()
        {
            day04Logger = new LoggerConfiguration()
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
            day04Logger.Information($"CharRecord - Symbol: {record.Symbol}, Index: {record.Index}, IsChecked: {record.isChecked}");
        }

        public void LogDigitRecord(DigitRecord record)
        {
            day04Logger.Information($"DigitRecord - Value: {record.Value}, StartIndex: {record.StartIndex}, EndIndex: {record.EndIndex}, IsAdjacent: {record.IsAdjacent}");
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
                day04Logger.Information("list is empty");
                return;
            }

            var itemStrings = items.Select(item => item.ToString());
            day04Logger.Information($"List values: {string.Join(", ", itemStrings)}");
        }




        public void LogEnum<T>(List<T> items)
        {
            if (!items.Any())
            {
                day04Logger.Information("list is empty");
                return;
            }

            var itemStrings = items.Select(item => item?.ToString() ?? "null");
            day04Logger.Information($"List values: {itemStrings}");
        }


        public void LogRecord<T>(IEnumerable<T> items, string title = "Items")
        {
            if (!items.Any())
            {
                day04Logger.Information($"{title} list is empty");
                return;
            }

            day04Logger.Information($"{title}:");
            foreach (var item in items)
            {
                if (item == null)
                {
                    day04Logger.Information("    null");
                    continue;
                }

                var type = typeof(T);
                var properties = type.GetProperties()
                    .Select(p => $"{p.Name}: {p.GetValue(item)}")
                    .Concat(type.GetFields()
                        .Select(f => $"{f.Name}: {f.GetValue(item)}"));

                day04Logger.Information($"    [{properties}]", string.Join(", ", properties));

                foreach (var property in type.GetProperties())
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listValue = property.GetValue(item);
                        if (listValue is IEnumerable<object> list)
                        {
                            day04Logger.Information($"    Logging list property: {property.Name}");
                            LogEnum(list.Cast<object>().ToList());
                        }
                    }
                }
            }
        }





        public void Debug(string message) => day04Logger.Debug(message);
        public void Debug(string message, params object[] propertyValues) => day04Logger.Debug(message, propertyValues);
        public void Info(string message) => day04Logger.Information(message);
        public void Info(string message, params object[] propertyValues) => day04Logger.Information(message, propertyValues);
    }
}


