using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectToolBarScroll
{
    /// <summary>
    /// Lógica de interacción para ToolBarScroll.xaml
    /// </summary>
    /// 

    #region Enums

        public enum EnumModeScroll
        {
            OneToOne,
            VisualItems
        }

    #endregion
    /// <summary>
    /// Constructor
    /// </summary>
    public partial class ToolBarScroll : ToolBar
    {
        public ToolBarScroll()
        {
            InitializeComponent();



            ElementsItems = Items.Children;
            
        }

        #region Private Properties

        private EnumModeScroll _modeScroll = EnumModeScroll.VisualItems;

        private int _visibleItems;

        private List<FrameworkElement> _listElements = null;

        #endregion

        #region Public Properties

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register("ElementsItems", typeof(UIElementCollection), typeof(ToolBarScroll),null);

        #endregion

        #region Public Properties

        public EnumModeScroll ModeScroll
        {
            get
            {
                return _modeScroll;
            }
            set
            {
                _modeScroll = value;

            }
        }

        public int VisibleItems
        {
            get
            {
                return _visibleItems;
            }
            set
            {
                _visibleItems = value;

            }
        }
       
        public UIElementCollection ElementsItems
        {
            get
            {
                return GetValue(ChildrenProperty) as UIElementCollection;
            }
            set
            {
                SetValue(ChildrenProperty, value);

            }
        }

        #endregion

        #region Event
        /// <summary>
        /// Evento de Cargar del Componente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                ToolBar toolBar = sender as ToolBar;
                var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;

                if (overflowGrid != null)
                {
                    overflowGrid.Visibility = Visibility.Collapsed;
                }

                   
                _listElements = new List<FrameworkElement>();
                foreach (UIElement item in ElementsItems)
                {
                    _listElements.Add(item as FrameworkElement);
                }

                if(VisibleItems >= _listElements.Count)
                {
                    bt_Right.IsEnabled = false;
                    VisibleItems = _listElements.Count;
                    
                }

                bt_Left.IsEnabled = false;
                _listElements.Skip<FrameworkElement>(VisibleItems).Cast<Button>().ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
            }
        }

        /// <summary>
        /// Evento de pulsado de boton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Left_Click(object sender, RoutedEventArgs e)
        {
            int firstVisible = _listElements.FindIndex(x => x.Visibility == Visibility.Visible);
            int lastVisible = _listElements.FindLastIndex(x => x.Visibility == Visibility.Visible);
            
            if (ModeScroll == EnumModeScroll.VisualItems)
            {
                _listElements.Select(x => x.Visibility = Visibility.Visible).ToList();
                if ((firstVisible) < VisibleItems)
                {
                    _listElements.Where((value, index) => index >= (VisibleItems)).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
                }
                else
                {
                    _listElements.Where((value, index) => index < (firstVisible - VisibleItems) || index >= (firstVisible)).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
                }
                
            }
            else
            {
                _listElements.Where((value, index) => index == firstVisible - 1).ToList().Select(x => x.Visibility = Visibility.Visible).ToList();
                _listElements.Where((value, index) => index == lastVisible).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
            }

            if (_listElements.FirstOrDefault().Visibility == Visibility.Visible)
            {
                bt_Left.IsEnabled = false;
                bt_Right.IsEnabled = true;
            }
            else
            {
                bt_Right.IsEnabled = true;
                bt_Left.IsEnabled = true;
            }
        }

        /// <summary>
        /// Evento de pulsado del boton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Right_Click(object sender, RoutedEventArgs e)
        {
            int firstCollapsed = _listElements.FindLastIndex(x => x.Visibility == Visibility.Visible) + 1;
            int firstVisible = _listElements.FindIndex(x => x.Visibility == Visibility.Visible);
            int lastCollapsed = _listElements.FindLastIndex(x => x.Visibility == Visibility.Collapsed);

            if (ModeScroll == EnumModeScroll.VisualItems)
            {
                _listElements.Select(x => x.Visibility = Visibility.Visible).ToList();
                if((lastCollapsed - firstCollapsed) < VisibleItems)
                {
                    _listElements.Where((value, index) => index <= (lastCollapsed - VisibleItems)).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
                }
                else
{
                    _listElements.Where((value, index) => index <= (firstCollapsed - 1) || index >= (VisibleItems + firstCollapsed)).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
                }
                
            }
            else
            {
                _listElements.Where((value, index) => index == firstCollapsed).ToList().Select(x => x.Visibility = Visibility.Visible).ToList();
                _listElements.Where((value, index) => index == firstVisible).ToList().Select(x => x.Visibility = Visibility.Collapsed).ToList();
            }

            if (_listElements.Last().Visibility == Visibility.Visible)
            {
                bt_Right.IsEnabled = false;
                bt_Left.IsEnabled = true;
            }
            else
            {
                bt_Right.IsEnabled = true;
                bt_Left.IsEnabled = true;
            }
        }

        #endregion
  
    }
}
