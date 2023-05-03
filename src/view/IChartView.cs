using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3.src.view
{
    public interface IChartView
    {
        event Action RequestedStatistics;
        event Action<String> ChangedCommand;

        /// <summary>
        /// Выводит на экран график [series]
        /// </summary>
        /// 
        void ShowChart(List<Series> series);
        /// <summary>
        /// Выводит на экран сообщение [info]
        /// </summary>
        void ShowAdditionalInfo(string info);

        /// <summary>
        /// Выводит на экран таблицу.
        /// </summary>
        void ShowGrid(List<string> columnsName, List<List<string>> rows);

        void SetCommands(List<string> commands);

        String GetCSV();
    }
}
