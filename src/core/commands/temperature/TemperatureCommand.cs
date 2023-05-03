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
            Series populationSeries = new Series();
            populationSeries.ChartType = SeriesChartType.Spline;
            populationSeries.YAxisType = AxisType.Primary;
            populationSeries.Name = "Температура минимальная";
            data.ForEach((point) => populationSeries.Points.AddXY(point[0], point[1]));

            Series growSeries = new Series();
            growSeries.ChartType = SeriesChartType.FastLine;
            growSeries.YAxisType = AxisType.Secondary;
            growSeries.Name = "Температура средняя";
            data.ForEach((point) => growSeries.Points.AddXY(point[0], point[2]));

            Series growSeries1 = new Series();
            growSeries1.ChartType = SeriesChartType.FastLine;
            growSeries1.YAxisType = AxisType.Secondary;
            growSeries1.Name = "Температура максимальная";
            data.ForEach((point) => growSeries1.Points.AddXY(point[0], point[3]));


            presenter.ShowChart(new List<Series> { populationSeries, growSeries, growSeries1 });

            // Info display
            presenter.ShowAdditionalInfo("Дополнительная информация отсуствует");
        }
    }
}
