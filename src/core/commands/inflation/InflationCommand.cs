using Lalalend_3.core.commands;
using Lalalend_3.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3.src.core.commands.inflation
{
    internal class InflationCommand : IChartCommand
    {
        readonly List<List<float>> data;

        public InflationCommand(List<List<float>> data)
        {
            this.data = data;
        }

        public void Run(IChartPresenter presenter)
        {
            // Table display
            List<string> columnsName = new List<string>() { "Год","Значение инфляции" };
            List<List<string>> rows = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                rows.Add(data[i].Select((e) => e.ToString()).ToList());
            }
            presenter.ShowGrid(columnsName, rows);

            // Chart display
            Series minINF = new Series();
            minINF.ChartType = SeriesChartType.Spline;
            minINF.YAxisType = AxisType.Primary;
            minINF.Name = "Значение инфляции";
            data.ForEach((point) => minINF.Points.AddXY(point[0], point[1]));

     

           


            presenter.ShowChart(new List<Series> { minINF});

            // Info display
            presenter.ShowAdditionalInfo("Дополнительная информация отсуствует");
        }
    }
}
