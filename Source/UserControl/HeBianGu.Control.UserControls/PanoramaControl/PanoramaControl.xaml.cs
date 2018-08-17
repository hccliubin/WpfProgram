using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Control.UserControls.PanoramaControl
{
    /// <summary>
    /// PanoramaControl.xaml 的交互逻辑
    /// </summary>
    public partial class PanoramaControl : UserControl
    {
        public PanoramaControl()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(new MessageBoxService());
        }
    }

    public class MainWindowViewModel : INPCBase
    {
        private Random rand = new Random(DateTime.Now.Millisecond);
        private List<DummyTileData> dummyData = new List<DummyTileData>();
        private IMessageBoxService messageBoxService;



        public MainWindowViewModel(IMessageBoxService messageBoxService)
        {
            this.messageBoxService = messageBoxService;

            //create some dummy data
            dummyData.Add(new DummyTileData("Add", @"Images/Add.png"));
            dummyData.Add(new DummyTileData("Adobe", @"Images/Adobe.png"));
            dummyData.Add(new DummyTileData("Android", @"Images/Android.png"));
            dummyData.Add(new DummyTileData("Author", @"Images/Author.png"));
            dummyData.Add(new DummyTileData("Blogger", @"Images/Blogger.png"));
            dummyData.Add(new DummyTileData("Copy", @"Images/Copy.png"));
            dummyData.Add(new DummyTileData("Delete", @"Images/Delete.png"));
            dummyData.Add(new DummyTileData("Digg", @"Images/Digg.png"));
            dummyData.Add(new DummyTileData("Edit", @"Images/Edit.png"));
            dummyData.Add(new DummyTileData("Facebook", @"Images/Facebook.png"));
            dummyData.Add(new DummyTileData("GMail", @"Images/GMail.png"));
            dummyData.Add(new DummyTileData("RSS", @"Images/RSS.png"));
            dummyData.Add(new DummyTileData("Save", @"Images/Save.png"));
            dummyData.Add(new DummyTileData("Search", @"Images/Search.png"));
            dummyData.Add(new DummyTileData("Trash", @"Images/Trash.png"));
            dummyData.Add(new DummyTileData("Twitter", @"Images/Twitter.png"));
            dummyData.Add(new DummyTileData("VisualStudio", @"Images/VisualStudio.png"));
            dummyData.Add(new DummyTileData("Wordpress", @"Images/Wordpress.png"));
            dummyData.Add(new DummyTileData("Yahoo", @"Images/Yahoo.png"));
            dummyData.Add(new DummyTileData("YouTube", @"Images/YouTube.png"));

            //Great some dummy groups
            List<PanoramaGroup> data = new List<PanoramaGroup>();
            List<IPanoramaTile> tiles = new List<IPanoramaTile>();

            for (int i = 0; i < 4; i++)
            {
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(true));

                tiles.Add(CreateTile(true));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));

                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));

                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
                tiles.Add(CreateTile(false));
            }

            data.Add(new PanoramaGroup("Settings",
                CollectionViewSource.GetDefaultView(tiles)));

            PanoramaItems = data;

        }


        private PanoramaTileViewModel CreateTile(bool isDoubleWidth)
        {
            DummyTileData dummyTileData = dummyData[rand.Next(dummyData.Count)];
            return new PanoramaTileViewModel(messageBoxService,
                dummyTileData.Text, dummyTileData.ImageUrl, isDoubleWidth);
        }


        private IEnumerable<PanoramaGroup> panoramaItems;

        public IEnumerable<PanoramaGroup> PanoramaItems
        {
            get { return this.panoramaItems; }

            set
            {
                if (value != this.panoramaItems)
                {
                    this.panoramaItems = value;
                    NotifyPropertyChanged("CompanyName");
                }
            }
        }
    }


    public class INPCBase : INotifyPropertyChanged
    {
        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

    }


    public class PanoramaTileViewModel : INPCBase, IPanoramaTile
    {
        private IMessageBoxService messageBoxService;
        private System.Timers.Timer liveUpdateTileTimer = new System.Timers.Timer();

        public PanoramaTileViewModel(IMessageBoxService messageBoxService, string text, string imageUrl, bool isDoubleWidth)
        {
            if (isDoubleWidth)
            {
                liveUpdateTileTimer.Interval = 1000;
                liveUpdateTileTimer.Elapsed += new ElapsedEventHandler(LiveUpdateTileTimer_Elapsed);
                liveUpdateTileTimer.Start();
            }


            this.messageBoxService = messageBoxService;
            this.Text = text;
            this.ImageUrl = imageUrl;
            this.IsDoubleWidth = isDoubleWidth;
            this.TileClickedCommand = new SimpleCommand<object, object>(ExecuteTileClickedCommand);
        }

        void LiveUpdateTileTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Counter < 10)
                Counter++;
            else
                Counter = 0;
            NotifyPropertyChanged("Counter");
        }




        public int Counter { get; set; }
        public string Text { get; private set; }
        public string ImageUrl { get; private set; }
        public bool IsDoubleWidth { get; private set; }
        public ICommand TileClickedCommand { get; private set; }

        public void ExecuteTileClickedCommand(object parameter)
        {
            messageBoxService.ShowMessage(string.Format("you clicked {0}", this.Text));
        }
    }
    public class DummyTileData
    {
        public string Text { get; private set; }
        public string ImageUrl { get; private set; }

        public DummyTileData(string text, string imageUrl)
        {
            this.Text = text;
            this.ImageUrl = imageUrl;
        }
    }

    public interface IMessageBoxService
    {
        void ShowMessage(string message);
    }

    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
