using Binance.Net.Objects;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TickGraph.ViewModels
{
    public class TrendViewModel : ReactiveObject
    {
        #region Name

        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        #endregion

        #region StreamData

        private readonly SourceList<BinanceStreamTick> _streamData = new SourceList<BinanceStreamTick>();


        public IObservableCollection<BinanceStreamTick> StreamData { get; } =
            new ObservableCollectionExtended<BinanceStreamTick>();

        #endregion

        #region Color

        private SolidColorBrush _color;

        public SolidColorBrush Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        #endregion
    }
}
