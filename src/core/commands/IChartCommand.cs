using System;

namespace Lalalend_3.core.commands
{
    /// <summary>
    /// Команда отображения статистики.
    /// </summary>
    public interface IChartCommand
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="presenter">Презентер, отвечающий за отображение на экране.</param>
        void Run(IChartPresenter presenter);
    }
}
