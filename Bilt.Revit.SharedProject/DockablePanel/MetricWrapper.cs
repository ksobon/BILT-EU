using System.Windows.Media;

namespace Bilt.DockablePanel
{
    public class MetricWrapper
    {
        public string Name { get; set; }
        public string Score { get; set; }
        public SolidColorBrush Color { get; set; }

        public MetricWrapper(string name, string score, SolidColorBrush color)
        {
            Name = name;
            Score = score;
            Color = color;
        }
    }
}
