using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3
{
    class LoadBalancerTest
    {
        private Order order1;
        private Order order2;
        private Order order3;

        private LoadBalancer loadBalancer;

        private AlgoEngine engine1;
        private AlgoEngine engine2;

        [SetUp]
        public void SetUp()
        {
            order1 = new Order();
            order2 = new Order();
            order3 = new Order();

            engine1 = new AlgoEngine();
            engine2 = new AlgoEngine();

            loadBalancer = new LoadBalancer(engine1, engine2);
        }

        [Test]
        public void RoutingOrderToLessNumberOfOrders()
        {
            AlgoEngine engine = loadBalancer.Add(order1);
            Assert.AreEqual(engine1, engine);

            engine = loadBalancer.Add(order2);
            Assert.AreEqual(engine2, engine);

            engine = loadBalancer.Add(order3);
            Assert.AreEqual(engine1, engine);
        }

        [Test]
        public void DynamicallyShiftLoadToOtherEngine()
        {
            loadBalancer.Add(order1);
            loadBalancer.Add(order2);

            AlgoEngine engine = loadBalancer.ShiftOrder(order1, 1);
            Assert.True(engine.Contains(order1));
        }

        [Test]
        public void DetermineRealtimeEngineLoad()
        {
            loadBalancer.Add(order1);
            loadBalancer.Add(order2);
            loadBalancer.Add(order3);

            int[] engineLoading = loadBalancer.GetEngineLoading();
            Assert.AreEqual(new int[] { 2, 1 }, engineLoading);
        }

        [Test]        

        public void WhenHitAllThresholdsThenAlertAllInstances()
        {
            engine1.Thresholds = 1;
            engine2.Thresholds = 1;

            loadBalancer.Add(order1);
            loadBalancer.Add(order2);

            ActualValueDelegate<object> testDelegate = () => loadBalancer.Add(order3);
            Assert.That(testDelegate, Throws.TypeOf<AllThresholdsReachedException>());
        }
    }
}
