using BiliPlayer.Util;
using BiliPlayer.View;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BiliPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += this.MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!WindowUtil.IsInDesignMode())
            {
                await Task.Delay(100);
                this.compose();
                foreach (IViewPart viewPart in this._views)
                {
                    this.root.Children.Add(viewPart as UIElement);
                    viewPart.Init(this.root);
                }
            }
        }

        private void compose()
        {
            this._mediaElement = new MediaElement
            {
                ScrubbingEnabled = true
            };
            using (AssemblyCatalog assemblyCatalog = new AssemblyCatalog(base.GetType().Assembly))
            {
                this._container = new CompositionContainer(assemblyCatalog, new ExportProvider[0]);
                this._container.ComposeParts(new object[]
                {
                    this
                });
            }
        }

        [ImportMany]
        private IViewPart[] _views;

        [Export]
        private MediaElement _mediaElement;

        private CompositionContainer _container;
    }
}
