using System.Collections.Generic;

namespace Bilt.DockablePanel
{
    public class MetricsRefreshedMessage
    {
        public List<MetricWrapper> Metrics { get; set; }

        public MetricsRefreshedMessage(List<MetricWrapper> metrics)
        {
            Metrics = metrics;
        }
    }
}
