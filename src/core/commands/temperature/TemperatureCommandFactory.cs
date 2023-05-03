using Lalalend_3.core.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lalalend_3.src.core.commands.temperature
{
    internal class TemperatureCommandFactory : AbstractCommandFactory
    {
        public override IChartCommand CreateFromCSV(string csv)
        {
            var data =
                csv.Split('\n').ToList()
                .Select((e) => e.Split(';')
                    .Select((num) => float.Parse(num)).ToList())
                .ToList();
            return new TemperatureCommand(data);
        }
    }
}
