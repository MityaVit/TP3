using Lalalend_3.core.commands;
using System;

namespace Lalalend_3.src.core.commands
{
    /// <summary>
    /// Абстрактная фабрика для запуска команды.
    /// </summary>
    public abstract class AbstractCommandFactory
    {
        public AbstractCommandFactory() { }

        /// <summary>
        /// Создать команду из текста в формате CSV.
        /// </summary>
        /// <param name="csv">Текст в формате CSV.</param>
        /// <returns></returns>
        public abstract IChartCommand CreateFromCSV(String csv);
    }
}
