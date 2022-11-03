using System.Linq;
using Autodesk.Revit.UI;
using Bilt.DockablePanel;

namespace Bilt
{
    public class AppCommand : IExternalApplication
    {
        // TODO: Add DockablePanelRequestHandler property
        // TODO: Add ExternalEvent property

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
#if Revit2020
            var ribbonPanel = app.GetRibbonPanels("BILT").FirstOrDefault(x => x.Name == "BILT 2020") ??
                              app.CreateRibbonPanel("BILT", "BILT 2020");
#elif Revit2022
            var ribbonPanel = app.GetRibbonPanels("BILT").FirstOrDefault(x => x.Name == "BILT 2022") ??
                              app.CreateRibbonPanel("BILT", "BILT 2022");
#else
           var ribbonPanel = app.GetRibbonPanels("BILT").FirstOrDefault(x => x.Name == "BILT 2023") ??
                              app.CreateRibbonPanel("BILT", "BILT 2023");
#endif

            DockablePanelCommand.CreateButton(ribbonPanel);

            // TODO: Assign DockablePanelRequestHandler
            // TODO: Assign DockablePanelRequestHandler to ExternalEvent

            // TODO: Add events for:
            // - DocumentOpened
            // - SynchFinished
            // - Saved etc.

            DockablePanelUtils.RegisterDockablePanel(app);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
