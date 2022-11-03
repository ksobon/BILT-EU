using System.Linq;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Bilt.DockablePanel;

namespace Bilt
{
    public class AppCommand : IExternalApplication
    {
        public static DockablePanelRequestHandler Handler { get; set; }
        public static ExternalEvent Event { get; set; }

        public Result OnStartup(UIControlledApplication app)
        {
            try
            {
                app.CreateRibbonTab("BILT");
            }
            catch
            {
                // ignored
            }

#if Revit2022
            var ribbonPanel = app.GetRibbonPanels("BILT").FirstOrDefault(x => x.Name == "BILT 2022") ??
                              app.CreateRibbonPanel("BILT", "BILT 2022");
#else
           var ribbonPanel = app.GetRibbonPanels("BILT").FirstOrDefault(x => x.Name == "BILT 2023") ??
                              app.CreateRibbonPanel("BILT", "BILT 2023");
#endif

            DockablePanelCommand.CreateButton(ribbonPanel);

            Handler = new DockablePanelRequestHandler();
            Event = ExternalEvent.Create(Handler);

            app.ControlledApplication.DocumentOpened += OnDocumentOpened;
            app.ControlledApplication.DocumentSaved += OnDocumentSaved;
            app.ControlledApplication.DocumentSynchronizedWithCentral += OnDocumentSynched;

            DockablePanelUtils.RegisterDockablePanel(app);

            return Result.Succeeded;
        }

        private static void OnDocumentSynched(object sender, DocumentSynchronizedWithCentralEventArgs e)
        {
            Refresh();
        }

        private static void OnDocumentSaved(object sender, DocumentSavedEventArgs e)
        {
            Refresh();
        }

        private static void OnDocumentOpened(object sender, DocumentOpenedEventArgs e)
        {
            Refresh();
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            app.ControlledApplication.DocumentOpened -= OnDocumentOpened;
            app.ControlledApplication.DocumentSaved -= OnDocumentSaved;
            app.ControlledApplication.DocumentSynchronizedWithCentral -= OnDocumentSynched;

            return Result.Succeeded;
        }

        #region Utilities

        private static void Refresh()
        {
            Handler.Request = RequestId.Refresh;
            Event.Raise();
        }

        #endregion
    }
}
