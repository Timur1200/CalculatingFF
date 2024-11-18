using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatingFF
{
    public  class TaskManager
    {
        public static TaskManager _this { get;  set; }
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        // Функция для запуска задач
        public Task RunTask(Action action)
        {
            return Task.Run(() => action(), _cancellationTokenSource.Token);
        }

        // Функция для отмены всех задач
        public void CancelAllTasks()
        {
            return;
            _cancellationTokenSource.Cancel();
        }

        // Пример использования
        public async Task ExampleUsage()
        {
            // Запуск нескольких задач
            //var task1 = RunTask(() => LongRunningOperation("Task 1"));
            //var task2 = RunTask(() => LongRunningOperation("Task 2"));

            // Ждем некоторое время
            await Task.Delay(2000);

            // Отменяем все задачи
            CancelAllTasks();

            try
            {
               // await Task.WhenAll(task1, task2);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("All tasks were canceled.");
            }
        }

        
    }
}
