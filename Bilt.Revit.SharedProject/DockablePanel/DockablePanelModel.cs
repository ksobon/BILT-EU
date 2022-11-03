namespace Bilt.DockablePanel
{
    public class DockablePanelModel
    {
        public void Refresh()
        {
            AppCommand.Handler.Request = RequestId.Refresh;
            AppCommand.Event.Raise();
        }
    }
}
