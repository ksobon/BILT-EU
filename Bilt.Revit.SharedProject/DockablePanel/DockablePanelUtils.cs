﻿using System;
using Autodesk.Revit.UI;

namespace Bilt.DockablePanel
{
    public class DockablePanelUtils
    {
        public static void RegisterDockablePanel(UIControlledApplication app)
        {
            var vm = new DockablePanelViewModel();
            var v = new DockablePanelPage
            {
                DataContext = vm
            };

            var paneId = new DockablePaneId(new Guid("3044B7F4-A10A-4731-845F-8889F574D04B"));
            try
            {
                // (Konrad) It's possible that a dock-able panel with the same id already exists
                // This ensures that we don't get an exception here. 
                app.RegisterDockablePane(paneId, "BILT", v);
            }
            catch
            {
                // ignored
            }
        }

        public static void ShowDockablePanel(UIApplication app)
        {
            var paneId = new DockablePaneId(new Guid("3044B7F4-A10A-4731-845F-8889F574D04B"));
            var dp = app.GetDockablePane(paneId);
            dp?.Show();
        }
    }
}
