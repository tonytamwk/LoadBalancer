using System;
using System.Linq;

namespace Exercise3
{
    internal class LoadBalancer
    {
        private AlgoEngine[] engines;

        public LoadBalancer(params AlgoEngine[] engines)
        {
            this.engines = engines;
        }

        private AlgoEngine PickByBaseOnNumberOfOrders()
        {
            return engines.Where(x => x.NumberOfProcessingOrders < x.Thresholds).OrderBy(x => x.NumberOfProcessingOrders).FirstOrDefault();
        }

        internal AlgoEngine Add(Order order)
        {
            AlgoEngine engine = PickByBaseOnNumberOfOrders();
            if (engine == null)
            {
                throw new AllThresholdsReachedException();
            }
            engine.Add(order);
            return engine;
        }

        internal AlgoEngine ShiftOrder(Order order, int engineIndex)
        {
            foreach (var engine in engines)
            {
                bool isRemoved = engine.Remove(order);
                if (isRemoved)
                {
                    break;
                }
            }

            var shiftEngine = engines[engineIndex];
            shiftEngine.Add(order);
            return shiftEngine;
        }

        internal int[] GetEngineLoading()
        {
            int[] engineLoading = new int[engines.Length];
            for (int i = 0; i < engines.Length; i++)
            {
                engineLoading[i] = engines[i].NumberOfProcessingOrders;
            }
            return engineLoading;
        }
    }
}