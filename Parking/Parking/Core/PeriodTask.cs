using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Core
{
    public class PeriodTask
    {
        public async Task Run(Action<Car> action, TimeSpan period, CancellationToken cancellationToken, Car car)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(period, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                    action(car);
            }
        }

        public Task Run(Action<Car> action, TimeSpan period, Car car)
        {
            return Run(action, period, CancellationToken.None, car);
        }
    }
}
