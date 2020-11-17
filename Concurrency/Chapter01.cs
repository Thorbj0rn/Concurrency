using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Concurrency
{
    public class Chapter01
    {
        /// <summary>
        /// Избегайте async void! Возможно создать async-метод, который возвращает void, 
        /// но это следует делать только при написании async-обработчика событий. 
        /// Обычный async-метод без возвращаемого значения должен возвращать Task, а не void.
        /// </summary>
        /// <returns></returns>
        public async Task DoSomething()
        {
            int value = 13;
            // Асинхронно ожидать 1 секунду.
            await Task.Delay(TimeSpan.FromSeconds(1));
            value *= 2;
            // Асинхронно ожидать 1 секунду.
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine(value);
        }

        /// <summary>
        /// Если вызвать метод DoSomethingAsync из UI-потока, каждая из его синхронных частей 
        /// будет выполняться в этом UI-потоке, но если вызвать его из потока из пула потоков, 
        /// то каждая из синхронных частей будет выполняться в любом потоке из пула потоков.
        /// Чтобы обойти это поведение по умолчанию, можно выполнить await по результату метода 
        /// расширения ConfigureAwait с передачей false в параметре continueOnCapturedContext.
        /// </summary>
        /// <returns></returns>
        public async Task DoSomethingAsync()
        {
            int value = 13;
            // Асинхронно ожидать 1 секунду.
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            value *= 2;
            // Асинхронно ожидать 1 секунду.
            
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            Trace.WriteLine(value);
        }


        /// <summary>
        /// Блокировка не сработала при вызове из консольного приложения, а при вызове из WinApp сработала. 
        /// Видимо дело в том что UI поток один и он блокирует в случае с WinApp, а в случае с консольным
        /// приложением используется пул потоков, и программа продолжает работу в одном из них.
        /// </summary>
        /// <returns></returns>
        async Task<int> WaitAsync()
        {
            // await сохранит текущий контекст ...
            await Task.Delay(TimeSpan.FromSeconds(3));//.ConfigureAwait(false);
            // ... и попытается возобновить метод в этой точке с этим контекстом.
            return 34;
        }
        public int Deadlock()
        {
            // Начать задержку.
            var task = WaitAsync();
            // Синхронное блокирование с ожиданием завершения async-метода.
            var res = task.Result;

            //Console.WriteLine($"End of deadlock. Result is {res}");

            return res;
        }
    }
}
