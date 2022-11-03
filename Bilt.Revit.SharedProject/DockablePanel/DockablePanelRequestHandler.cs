using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.Messaging;

namespace Bilt.DockablePanel
{
    public enum RequestId
    {
        None,
        Refresh
    }

    public class DockablePanelRequestHandler : IExternalEventHandler
    {
        public RequestId Request { get; set; }

        public void Execute(UIApplication app)
        {
            try
            {
                switch (Request)
                {
                    case RequestId.None:
                        return;
                    case RequestId.Refresh:
                        Refresh(app);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void Refresh(UIApplication app)
        {
            var doc = app.ActiveUIDocument.Document;

            var warnings = doc.GetWarnings().ToList();
            var warningsScore = warnings.Count.ToString();
            var warningsColor = new SolidColorBrush(warnings.Count > 10 ? Colors.LightCoral : Colors.LightGreen);
            var warningsMetric = new MetricWrapper("Warnings", warningsScore, warningsColor);

            var rooms = new FilteredElementCollector(doc)
                .OfClass(typeof(SpatialElement))
                .WhereElementIsNotElementType()
                .Cast<SpatialElement>()
                .Where(x => x.Area <= 0)
                .ToList();
            var roomsScore = rooms.Count.ToString();
            var roomsColor = new SolidColorBrush(rooms.Count > 0 ? Colors.LightCoral : Colors.LightGreen);
            var roomsMetric = new MetricWrapper("Unplaced Rooms", roomsScore, roomsColor);

            var metrics = new List<MetricWrapper> {warningsMetric, roomsMetric};
            Messenger.Default.Send(new MetricsRefreshedMessage(metrics));
        }

        public string GetName()
        {
            return "Dockable Panel Request Handler";
        }
    }
}
