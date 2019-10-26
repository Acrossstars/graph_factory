using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TickGraph.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Palitre

        SolidColorBrush[] solidColorBrushes = new SolidColorBrush[3]
        {
            new SolidColorBrush(Color.FromRgb(255,0,0)),
            new SolidColorBrush(Color.FromRgb(0,222,0)),
            new SolidColorBrush(Color.FromRgb(0,0,222)),
        };
            

        #endregion


        #region ApiKey

        private string _apiKey = "qDalyQCEJgWW62Sc1LDJfltLycgoyj4ow1ak9wo4DWUKwWo0IRf0x82bYXr7MQbG";
        public string ApiKey
        {
            get => _apiKey;
            set => this.RaiseAndSetIfChanged(ref _apiKey, value);
            //if (value != null && apiSecret != null)
            //    BinanceDefaults.SetDefaultApiCredentials();
        }

        #endregion

        #region ApiSecret

        private string _apiSecret = "RgnfcrMpvcffhz8m7BNjrK4X2tv41S90nddThx7F9Dnb0EpKOStJuxxCmVXpUZyt";
        public string ApiSecret
        {
            get => _apiSecret;
            set => this.RaiseAndSetIfChanged(ref _apiSecret, value);

            //if (value != null && apiKey != null)
            //    BinanceDefaults.SetDefaultApiCredentials(apiKey, value);
        }

        #endregion

        #region MainCollection

        private readonly SourceList<TrendViewModel> _mainCollection = new SourceList<TrendViewModel>();

        public IObservableCollection<TrendViewModel> MainCollection { get; } =
            new ObservableCollectionExtended<TrendViewModel>();

        #endregion

        #region StreamData

        private readonly SourceList<BinanceStreamTick> _streamData = new SourceList<BinanceStreamTick>();

        public IObservableCollection<BinanceStreamTick> StreamData { get; } =
            new ObservableCollectionExtended<BinanceStreamTick>();

        #endregion

        #region OrdersData

        private readonly SourceList<BinanceStreamTick> _ordersData = new SourceList<BinanceStreamTick>();

        public IObservableCollection<BinanceStreamTick> OrdersData { get; } =
            new ObservableCollectionExtended<BinanceStreamTick>();

        #endregion

        public MainWindowViewModel()
        {
            _mainCollection
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(MainCollection)
                .Subscribe();

            for (int k = 0; k < 3; k++)
            {
                _mainCollection.Add(new TrendViewModel());
            }

            int i = 0;

            foreach (var item in _mainCollection.Items)
            {
                

                item.Name = $"trend {i}";
                item.Color = solidColorBrushes[i];
                i++;
            }

            MainMethod();
        }


        void MainMethod()
        {
            var socketClient = new BinanceSocketClient();

            // Streams
            var successSingleTicker = socketClient.SubscribeToSymbolTickerUpdates("ltcbnb", (data) =>
            {
                _mainCollection.Items.ElementAt(0).StreamData.Add(data);
            });

            var ssuccessSingleTicker = socketClient.SubscribeToSymbolTickerUpdates("dashbnb", (data) =>
            {
                _mainCollection.Items.ElementAt(1).StreamData.Add(data);
            });

            var sssuccessSingleTicker = socketClient.SubscribeToSymbolTickerUpdates("xmrbnb", (data) =>
            {
                _mainCollection.Items.ElementAt(2).StreamData.Add(data);
            });
        }
    }
}
