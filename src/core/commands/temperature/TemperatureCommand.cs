using Lalalend_3.core.commands;
using Lalalend_3.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3.src.core.commands.temperature
{
    internal class TemperatureCommand : IChartCommand
    {
        readonly List<List<float>> data;

        public TemperatureCommand(List<List<float>> data)
        {
            this.data = data;
        }

        public void Run(IChartPresenter presenter)
        {
            // Table display
            List<string> columnsName = new List<string>() { "Число", "Минимальная температура", "Средняя температура", "Максимальная температура" };
            List<List<string>> rows = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                rows.Add(data[i].Select((e) => e.ToString()).ToList());
            }
            presenter.ShowGrid(columnsName, rows);

            // Chart display
            Series minTemp = new Series();
            minTemp.ChartType = SeriesChartType.Spline;
            minTemp.YAxisType = AxisType.Primary;
            minTemp.Name = "Температура минимальная";
            data.ForEach((point) => minTemp.Points.AddXY(point[0], point[1]));

            Series avgTemp = new Series();
            avgTemp.ChartType = SeriesChartType.FastLine;
            avgTemp.YAxisType = AxisType.Secondary;
            avgTemp.Name = "Температура средняя";
            data.ForEach((point) => avgTemp.Points.AddXY(point[0], point[2]));

            Series maxTemp = new Series();
            maxTemp.ChartType = SeriesChartType.FastLine;
            maxTemp.YAxisType = AxisType.Secondary;
            maxTemp.Name = "Температура максимальная";
            data.ForEach((point) => maxTemp.Points.AddXY(point[0], point[3]));


            presenter.ShowChart(new List<Series> { minTemp, avgTemp, maxTemp });

            float maxTemperatureDifference = 0;
            int maxTemperatureDifferenceIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                float minTemperature = data[i][1];
                float maxTemperature = data[i][3];
                float temperatureDifference = maxTemperature - minTemperature;
                if (temperatureDifference > maxTemperatureDifference)
                {
                    maxTemperatureDifference = temperatureDifference;
                    maxTemperatureDifferenceIndex = i;
                }
            }

            // Show maximum temperature difference and date
            if (maxTemperatureDifferenceIndex >= 0)
            {
                float maxTemperature = data[maxTemperatureDifferenceIndex][3];
                float minTemperature = data[maxTemperatureDifferenceIndex][1];
                string date = data[maxTemperatureDifferenceIndex][0].ToString();
                presenter.ShowAdditionalInfo($"Самый сильный перепад температуры за месяц: {maxTemperatureDifference} (на {date} число), максимальная температура: {maxTemperature}, минимальная температура: {minTemperature}");
            }
            else
            {
                presenter.ShowAdditionalInfo("Данные отсутствуют");
            };
        }
    }
}
