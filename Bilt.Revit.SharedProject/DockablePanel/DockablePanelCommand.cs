using System;
using System.Reflection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Bilt.Utilities;

namespace Bilt.DockablePanel
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class DockablePanelCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePanelUtils.ShowDockablePanel(commandData.Application);

            return Result.Succeeded;
        }

        public static void CreateButton(RibbonPanel panel)
        {
            var assembly = Assembly.GetExecutingAssembly();
            panel.AddItem(
                new PushButtonData(
                    MethodBase.GetCurrentMethod()?.DeclaringType?.Name,
                    "Show" + Environment.NewLine + "Panel",
                    assembly.Location,
                    MethodBase.GetCurrentMethod()?.DeclaringType?.FullName)
                {
                    ToolTip = "Dockable Panel button tooltip.",
                    LargeImage = ImageUtils.LoadImage(assembly, "_32x32.showPanel.png")
                });
        }
    }
}
