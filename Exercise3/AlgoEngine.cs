using System;
using System.Collections.Generic;

namespace Exercise3
{
    internal class AlgoEngine
    {
        private List<Order> orders;

        public AlgoEngine()
        {
            orders = new List<Order>();
            Thresholds = Int32.MaxValue;
        }

        public void Add(Order order)
        {
            orders.Add(order);
        }

        public int NumberOfProcessingOrders {  get { return orders.Count; } }

        public int Thresholds { get; internal set; }

        public bool Contains(Order order)
        {
            return orders.Contains(order);
        }

        internal bool Remove(Order order)
        {
            return orders.Remove(order);
        }
    }
}