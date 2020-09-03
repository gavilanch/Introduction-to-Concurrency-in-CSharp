using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_6
{
    public class Thread_Safety
    {
        public void btnStart_Click()
        {
            Console.WriteLine("start");

            Random random = new Random();

            var concurrentDictionary = new ConcurrentDictionary<double, int>();

            var mutex = new object();

            Parallel.For(1, 1000000, i =>
            {
                double key;
                lock (mutex)
                {
                    key = random.NextDouble();
                }
                concurrentDictionary.AddOrUpdate(key, 1, (key, currentValue) => currentValue + 1);
            });
            var moreFrequents = concurrentDictionary.OrderByDescending(x => x.Value).Take(5).ToList();

            foreach (var item in moreFrequents)
            {
                Console.WriteLine($"The key {item.Key} is repeated {item.Value} times");
            }

            Console.WriteLine("end");
        }
    }
}
