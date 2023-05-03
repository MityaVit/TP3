using Lalalend_3.core.commands;
using Lalalend_3.src.core.commands;
using Lalalend_3.src.core.commands.temperature;
using Lalalend_3.src.core.commands.inflation;
using Lalalend_3.src.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3.core
{
    internal class ChartPresenter : IChartPresenter
    {

        static Dictionary<string, Func<AbstractCommandFactory>> commands
            = new Dictionary<string, Func<AbstractCommandFactory>>()
            {
                {"Температура", () => new TemperatureCommandFactory()},
                {"Инфляция", () => new InflationCommandFactory()}
            };

        AbstractCommandFactory commandFactory;

        IChartView view;

        public IChartView View
        {
            set
            {
                if (view != null)
                {
                    this.view.RequestedStatistics -= RunCommand;
                    this.view.ChangedCommand -= ChangeCommand;
                }
                this.view = value;
                this.view.RequestedStatistics += RunCommand;
                this.view.ChangedCommand += ChangeCommand;
                view.SetCommands(commands.Keys.ToList());
            }
        }

        public void ShowAdditionalInfo(string info)
        {
            view.ShowAdditionalInfo(info);
        }

        public void ShowChart(List<Series> series)
        {
            view.ShowChart(series);
        }

        public void ShowGrid(List<string> columnsName, List<List<string>> rows)
        {
            view.ShowGrid(columnsName, rows);
        }

        private void RunCommand()
        {
            string csv = view.GetCSV();
            if (csv == "" || commandFactory == null) return;
            var command = commandFactory.CreateFromCSV(csv);
            command.Run(this);
        }

        private void ChangeCommand(String code)
        {
            commandFactory = commands[code]();
        }
    }
}
