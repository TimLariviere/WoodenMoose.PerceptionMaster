using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace WoodenMoose.PerceptionMaster.UWP.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Random _random = new Random();
        private float _lightPercentage = 0.75f;

        public MainPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    RaisePropertyChanged();
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GeneratePlayground();
        }

        private void GeneratePlayground()
        {
            foreach (var rect in PlaygroundGrid.Children)
            {
                (rect as UIElement).PointerReleased -= OnRectangleClicked;
            }
            PlaygroundGrid.Children.Clear();
            PlaygroundGrid.ColumnDefinitions.Clear();
            PlaygroundGrid.RowDefinitions.Clear();

            int rows = 3;
            int columns = 3;

            var mainColor = Helpers.ColorHelper.GetRandomColor();
            var colorToFind = Helpers.ColorHelper.GetLigtherColor(mainColor, _lightPercentage);
            var indexOfColorToFind = _random.Next(0, rows * columns);

            for (int i = 0; i < rows; i++)
            {
                PlaygroundGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < columns; i++)
            {
                PlaygroundGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var index = r * rows + c;
                    var color = index == indexOfColorToFind ? colorToFind : mainColor;

                    var rectangle = new Rectangle()
                    {
                        Fill = new SolidColorBrush(color),
                        Margin = new Thickness(3),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Tag = index == indexOfColorToFind
                    };
                    rectangle.PointerReleased += OnRectangleClicked;

                    PlaygroundGrid.Children.Add(rectangle);
                    Grid.SetColumn(rectangle, c);
                    Grid.SetRow(rectangle, r);
                }
            }
        }

        private void OnRectangleClicked(object sender, PointerRoutedEventArgs args)
        {
            if ((bool)(sender as FrameworkElement).Tag)
            {
                Score++;
                _lightPercentage = (((25 - Score) / 25.0f) * 0.75f) % 0.75f + 0.10f;
                Mode = (Score / 25) % 2 == 0  ? "Light mode" : "Dark mode";
            }

            GeneratePlayground();
        }
    }
}
