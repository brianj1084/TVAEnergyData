using System;
using System.Collections.Generic;
using TVAEnergyData.EIAClient.Models;

namespace TVAEnergyData.Web.Models
{
    public class Series
    {
        public string SeriesId { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Updated { get; set; }
        public List<Sample> Samples { get; set; }

        public static Series FromEIAResponse(EIAResponse eiaResponse)
        {
            var series = new Series
            {
                SeriesId = eiaResponse.Series[0].SeriesId,
                Name = eiaResponse.Series[0].Name,
                Units = eiaResponse.Series[0].Units,
                Description = eiaResponse.Series[0].Description,
                Start = GetDateTimeFromEIADateString(eiaResponse.Series[0].Start),
                End = GetDateTimeFromEIADateString(eiaResponse.Series[0].End),
                Updated = DateTime.Parse(eiaResponse.Series[0].Updated),
                Samples = new List<Sample>()
            };


            foreach (var item in eiaResponse.Series[0].Data)
            {
                var sample = new Sample
                {
                    Time = GetDateTimeFromEIADateString(item[0].String), Value = item[1].Integer ?? 0
                };

                series.Samples.Add(sample);
            }

            return series;
        }

        private static DateTime GetDateTimeFromEIADateString(string dateTimeString)
        {
            var year = int.Parse(dateTimeString.Substring(0, 4));
            var month = int.Parse(dateTimeString.Substring(4, 2));
            var day = int.Parse(dateTimeString.Substring(6, 2));
            var hour = int.Parse(dateTimeString.Substring(9, 2));

            return new DateTime(year, month, day, hour, 0, 0, DateTimeKind.Utc);
        }
    }
}
