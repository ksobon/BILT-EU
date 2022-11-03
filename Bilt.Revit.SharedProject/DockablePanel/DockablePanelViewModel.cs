using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Bilt.DockablePanel
{
    public class DockablePanelViewModel : ViewModelBase
    {
        public DockablePanelModel Model { get; set; }
        public RelayCommand Refresh { get; set; }

        private ObservableCollection<MetricWrapper> _metrics = new ObservableCollection<MetricWrapper>();
        public ObservableCollection<MetricWrapper> Metrics
        {
            get { return _metrics; }
            set { _metrics = value; RaisePropertyChanged(() => Metrics); }
        }

        public DockablePanelViewModel()
        {
            Model = new DockablePanelModel();

            Refresh = new RelayCommand(OnRefresh);

            Messenger.Default.Register<MetricsRefreshedMessage>(this, OnMetricsRefreshedMessage);
        }

        private void OnRefresh()
        {
            Model.Refresh();
        }

        private void OnMetricsRefreshedMessage(MetricsRefreshedMessage obj)
        {
            Metrics = new ObservableCollection<MetricWrapper>(obj.Metrics);
        }
    }
}
