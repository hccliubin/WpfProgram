﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Control.UserControls
{
    /// <summary> 分页滑动控件 </summary>
    public partial class TPageControl : UserControl
    {
        public TPageControl()
        {
            InitializeComponent();

            sboardLeftIamge = (Storyboard)this.FindResource("StoryboardLeftImage");

            sboardRightIamge = (Storyboard)this.FindResource("StoryboardRightImage");

        }

        static TPageControl()
        {
            LeftWidthProperty = DependencyProperty.Register("LeftWidth", typeof(double), typeof(TPageControl), new FrameworkPropertyMetadata(50.0, LeftWidthChange),
               ShareClass.UnDoubleValueCheck);
            RightWidthProperty = DependencyProperty.Register("RightWidth", typeof(double), typeof(TPageControl), new FrameworkPropertyMetadata(50.0, RightWidthChange),
               ShareClass.UnDoubleValueCheck);
        }

        #region - 成员变量 -

        int pageCount = 0;

        int pageSelect = 0;

        bool isDown = false;

        double down_pX = 0;

        double down_pY = 0;

        bool isMoveSure = false;

        double oldX = 0;

        bool isInMove = false;

        object changeLock = new object();

        Storyboard sboard;

        Storyboard sboardLeftIamge, sboardRightIamge;

        #endregion

        #region - 依赖属性 -

        //设置左右按钮图片

        //设置左按钮区域宽度
        public static readonly DependencyProperty LeftWidthProperty;

        public double LeftWidth
        {
            get { return (double)GetValue(LeftWidthProperty); }
            set { SetValue(LeftWidthProperty, value); }
        }
        private static void LeftWidthChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TPageControl pageControl = (TPageControl)sender;
            pageControl.LeftGrid.Width = new GridLength(pageControl.LeftWidth);
        }

        //设置右按钮区域宽度
        public static readonly DependencyProperty RightWidthProperty;

        public double RightWidth
        {
            get { return (double)GetValue(RightWidthProperty); }
            set { SetValue(RightWidthProperty, value); }
        }
        private static void RightWidthChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TPageControl pageControl = (TPageControl)sender;
            pageControl.RightGrid.Width = new GridLength(pageControl.RightWidth);
        }


        public bool IsAutoMove
        {
            get { return (bool)GetValue(IsAutoMoveProperty); }
            set { SetValue(IsAutoMoveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoMove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoMoveProperty =
            DependencyProperty.Register("IsAutoMove", typeof(bool), typeof(TPageControl), new PropertyMetadata(false, IsAutoMovePropertyChangedCallback));

        public static void IsAutoMovePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TPageControl;

            if (control == null) return;

            if (!(bool)e.NewValue)
            {
                time.Stop();
            }


            time.Elapsed += (object ss, ElapsedEventArgs ee) =>
            {
                lock (_lock)
                {
                    control.NextPage();
                }
            };

            time.Start();
        }

        static object _lock = new object();
        static Timer time = new Timer(2000);
        #endregion


        #region - 成员方法 -

        /// <summary>  添加页,为了实现拖拽功能,panel一律不准使用MouseLeftDown来实现触发事件,否则会影响翻页拖拽动作  </summary>
        /// <param name="panels">页(继承Panel即可,无论大小,强制拉升填满页)</param>
        /// <param name="defaultPageNum">添加完后默认页,0:保持在当前页</param>
        public void AddPage(List<Panel> panels, int defaultPageNum)
        {
            _AddPage(panels, defaultPageNum);
        }

        /// <summary> 获取页宽 </summary> 
        public double GetActualWidth()
        {
            return canvasPageContent.ActualWidth;
        }

        public double GetActualHeight()
        {
            return canvasPageContent.ActualHeight;
        }

        /// <summary> 获取页高 </summary>
        public double GetPageHieght()
        {
            return GetActualHeight() - 2;
        }

        /// <summary> 清理所有页 </summary>
        public void ClearPage()
        {
            _ClearPage();
        }

        private void _ClearPage()
        {
            wrapPanelPages.Children.Clear();
            pageCount = 0;
            PageSelect = 0;
            imageLeft.Visibility = System.Windows.Visibility.Hidden;
            imageRight.Visibility = System.Windows.Visibility.Hidden;

            pageBar1.Clear();
        }

        private void _AddPage(List<Panel> panels, int defaultPageNum)
        {
            if (panels.Count > 0)
            {
                //计算总页数
                pageCount += panels.Count;

                //设置wrapPanelPages宽度
                wrapPanelPages.Width = (GetActualWidth()) * pageCount;

                //加载页
                foreach (Panel panel in panels)
                {
                    //使用viewbox来控制Panel铺满页面
                    Viewbox viewbox = new Viewbox();
                    //viewbox.Stretch = Stretch.Fill;
                    viewbox.Width = GetActualWidth();
                    viewbox.Height = GetActualHeight();
                    viewbox.Child = panel;
                    wrapPanelPages.Children.Add(viewbox);
                }

                //改变页
                if (defaultPageNum > 0)
                    PageSelect = defaultPageNum;
                if (PageSelect > pageCount)
                    PageSelect = pageCount;
                if (PageSelect == 0)
                    PageSelect = 1;
                Canvas.SetLeft(wrapPanelPages, -GetActualWidth() * (PageSelect - 1));

                //改变按钮状态
                ChangeButtonStatus();

                pageBar1.CreatePageEllipse(pageCount, ChangePage);

                pageBar1.SelectPage(PageSelect);

                pageBar1.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                PageSelect = 0;
            }

            if (this.pageCount == 1) this.pageBar1.Visibility = Visibility.Hidden;
        }

        public void SetPage(int defaultPageNum)
        {
            //改变页
            if (defaultPageNum > 0)
                PageSelect = defaultPageNum;
            if (PageSelect > pageCount)
                PageSelect = pageCount;
            if (PageSelect == 0)
                PageSelect = 1;
            Canvas.SetLeft(wrapPanelPages, -GetActualWidth() * (PageSelect - 1));
            //改变按钮状态
            ChangeButtonStatus();



            pageBar1.CreatePageEllipse(pageCount, ChangePage);
            pageBar1.SelectPage(PageSelect);
            pageBar1.Visibility = System.Windows.Visibility.Visible;

            if (this.pageCount == 1) this.pageBar1.Visibility = Visibility.Hidden;
        }

        void ChangePage(int index)
        {
            this.PageSelect = index;

            Canvas.SetLeft(wrapPanelPages, -(PageSelect - 1) * GetActualWidth());
        }


        /// <summary> 翻页实现 </summary>
        private void ChangePage(bool isRight)
        {
            double pageWidth = GetActualWidth();

            lock (changeLock)
            {
                if (isInMove)
                    return;
                isInMove = true;
            }
            if (isRight)
            {
                if (PageSelect == pageCount)
                {
                    lock (changeLock)
                    {
                        isInMove = false;
                    }
                    return;
                }
                else
                {

                    isInMove = true;
                    double listLeft_now = Canvas.GetLeft(wrapPanelPages);
                    double listLeft_sur = -(PageSelect - 1) * pageWidth;
                    double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                    double toX = -pageWidth;
                    double time = 1000 * Math.Abs(Math.Abs(toX) - Math.Abs(formX)) / pageWidth;
                    sboardRightBegin(formX, toX, time);
                }
            }
            else
            {
                if (PageSelect == 1)
                {
                    lock (changeLock)
                    {
                        isInMove = false;
                    }
                    return;
                }
                else
                {
                    isInMove = true;
                    double listLeft_now = Canvas.GetLeft(wrapPanelPages);
                    double listLeft_sur = -(PageSelect - 1) * pageWidth;
                    //启动左翻动画-翻页
                    double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                    double toX = pageWidth;
                    double time = 1000 * Math.Abs(Math.Abs(toX) - Math.Abs(formX)) / pageWidth;
                    sboardLeftBegin(formX, toX, time);
                }
            }
        }

        /// <summary> 改变翻页按钮状态 </summary>
        private void ChangeButtonStatus()
        {
            if (pageCount == 0)
            {
                return;
            }
            if (PageSelect == 1 && pageCount == 1)
            {
                imageLeft.Visibility = System.Windows.Visibility.Hidden;
                imageRight.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (PageSelect == 1 && pageCount > 1)
            {
                imageLeft.Visibility = System.Windows.Visibility.Hidden;
                imageRight.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (PageSelect == pageCount)
                {
                    imageLeft.Visibility = System.Windows.Visibility.Visible;
                    imageRight.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    imageLeft.Visibility = System.Windows.Visibility.Visible;
                    imageRight.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }


        #region 拖拽实现

        private void canvasPageContent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
            System.Windows.Point position = e.GetPosition(canvasPageContent);
            down_pX = position.X;
            down_pY = position.Y;
            oldX = down_pX;
        }

        private void canvasPageContent_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
            {
                e.Handled = isMoveSure;
                changePos();
            }
        }

        private void canvasPageContent_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                System.Windows.Point position = e.GetPosition(canvasPageContent);
                Canvas.SetLeft(wrapPanelPages, Canvas.GetLeft(wrapPanelPages) + (position.X - oldX));
                oldX = position.X;

                if (Math.Abs(down_pX - position.X) > 150)
                {
                    isMoveSure = true;
                }
            }
        }

        private void canvasPageContent_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                changePos();
            }
        }

        private void changePos()
        {
            double pageWidth = this.GetActualWidth();
            isDown = false;
            isMoveSure = false;
            double listLeft_now = Canvas.GetLeft(wrapPanelPages);
            double listLeft_sur = -(PageSelect - 1) * pageWidth;
            //右翻页动作
            if (listLeft_now < listLeft_sur)
            {
                if (PageSelect == pageCount)
                {
                    //已经达到最大页面-回滚
                    isInMove = true;
                    double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                    double toX = 0;
                    double time = 1000 * Math.Abs(formX) / pageWidth;
                    Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                    sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                    sboard.Completed += sboardNoChange_Completed;
                    sboard.Begin();
                }
                else
                {
                    bool SureRight = false;
                    //移动距离
                    double dis = Math.Abs(listLeft_now - listLeft_sur);
                    //达到翻页确认标准
                    if (dis >= 150)
                        SureRight = true;
                    if (SureRight)
                    {
                        //启动右翻动画-翻页
                        isInMove = true;
                        double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                        double toX = -pageWidth;
                        double time = 1000 * Math.Abs(Math.Abs(toX) - Math.Abs(formX)) / pageWidth;
                        Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                        sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                        sboard.Completed += sboardRight_Completed;
                        sboard.Begin();
                    }
                    else
                    {
                        //未达到翻页要求-回滚
                        isInMove = true;
                        double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                        double toX = 0;
                        double time = 1000 * Math.Abs(formX) / pageWidth;
                        Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                        sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                        sboard.Completed += sboardNoChange_Completed;
                        sboard.Begin();
                    }
                }
            }
            //左翻页确认
            else
            {
                //第一页左翻-回滚
                if (PageSelect == 1)
                {
                    isInMove = true;
                    double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                    double toX = 0;
                    double time = 1000 * Math.Abs(formX) / pageWidth;
                    Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                    sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                    sboard.Completed += sboardNoChange_Completed;
                    sboard.Begin();
                }
                else
                {
                    //确认翻页参数
                    bool SureLeft = false;
                    //移动距离
                    double dis = Math.Abs(listLeft_now - listLeft_sur);
                    //达到翻页确认标准
                    if (dis >= 150)
                        SureLeft = true;
                    if (SureLeft)
                    {
                        isInMove = true;
                        //启动左翻动画-翻页
                        double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                        double toX = pageWidth;
                        double time = 1000 * Math.Abs(Math.Abs(toX) - Math.Abs(formX)) / pageWidth;
                        Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                        sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                        sboard.Completed += sboardLeft_Completed;
                        sboard.Begin();
                    }
                    else
                    {
                        isInMove = true;
                        //未达到翻页要求-回滚
                        double formX = listLeft_now + (PageSelect - 1) * pageWidth;
                        double toX = 0;
                        double time = 1000 * Math.Abs(formX) / pageWidth;
                        Canvas.SetLeft(wrapPanelPages, listLeft_sur);
                        sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
                        sboard.Completed += sboardNoChange_Completed;
                        sboard.Begin();
                    }
                }
            }
        }

        #endregion

        #region 翻页按钮

        private void imageLeft_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //this.imageLeft.Source = new BitmapImage(new Uri("Images/左滑.png", UriKind.Relative));
            //翻页
            //sboardLeftIamge.Begin();

            ChangePage(false);
        }



        private void imageRight_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.imageRight.Source = new BitmapImage(new Uri("Images/右滑.png", UriKind.Relative));
            //翻页
            //sboardRightIamge.Begin();

            ChangePage(true);
        }

        #endregion

        public event Action<int> SelectChanged;

        #region 动画处理

        //左翻动画
        private void sboardLeftBegin(double formX, double toX, double time)
        {
            sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
            sboard.Completed += sboardLeft_Completed;
            sboard.Begin();
        }

        //右翻动画
        private void sboardRightBegin(double formX, double toX, double time)
        {
            sboard = ShareClass.CeaterAnimation_Xmove(wrapPanelPages, formX, toX, time, 0);
            sboard.Completed += sboardRight_Completed;
            sboard.Begin();
        }

        //翻页回滚结束
        private void sboardNoChange_Completed(object sender, EventArgs e)
        {
            lock (changeLock)
            {
                isInMove = false;
            }
        }

        //右翻页结束
        private void sboardRight_Completed(object sender, EventArgs e)
        {
            PageSelect++;

            pageBar1.SelectPage(PageSelect);

            sboard.Stop();

            ChangeButtonStatus();

            Canvas.SetLeft(wrapPanelPages, -(PageSelect - 1) * GetActualWidth());
            lock (changeLock)
            {
                isInMove = false;
            }

            if (this.SelectChanged != null)
                this.SelectChanged(PageSelect);
        }

        //左翻页结束
        private void sboardLeft_Completed(object sender, EventArgs e)
        {
            PageSelect--;
            pageBar1.SelectPage(PageSelect);
            sboard.Stop();
            ChangeButtonStatus();
            Canvas.SetLeft(wrapPanelPages, -(PageSelect - 1) * GetActualWidth());
            lock (changeLock)
            {
                isInMove = false;
            }

            if (this.SelectChanged != null)
                this.SelectChanged(PageSelect);
        }

        //private void imageLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    this.imageLeft.Source = new BitmapImage(new Uri("Images/left.png", UriKind.Relative));
        //}

        //private void imageRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    this.imageRight.Source = new BitmapImage(new Uri("Images/right.png", UriKind.Relative));
        //}

        #endregion


        /// <summary> 页面大小变化，修改canvasPageContent的裁剪范围 </summary>
        private void canvasPageContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            canvasPageRectangle.Rect = new Rect(0, 0, this.GetActualWidth(), GetActualHeight());
        }

        public void OnNext()
        {
            this.Dispatcher.Invoke(() =>
            {

                ChangePage(true);

                if (this.PageSelect == this.pageCount)
                {
                    //this.SetPage(1);

                    this.ChangePage(1);
                }
            });

        }

        public void OnLast()
        {
            this.Dispatcher.Invoke(() => ChangePage(false));
        }

        public bool CanNext
        {
            get
            {
                return this.PageSelect < this.pageCount - 1;
            }
        }

        public bool CanLast
        {
            get
            {
                return this.PageSelect > 0;
            }
        }

        public List<T> FindAll<T>() where T : class
        {
            List<T> collection = new List<T>();

            foreach (var item in this.wrapPanelPages.Children)
            {
                if (item is Viewbox)
                {
                    Viewbox v = item as Viewbox;

                    Panel p = v.Child as Panel;

                    FindAll<T>(p, collection);
                }
            }

            return collection;
        }

        public void FindAll<T>(Panel c, List<T> collection) where T : class
        {

            foreach (var item in c.Children)
            {
                if (item is T)
                {
                    T t = item as T;

                    collection.Add(t);
                }

                if (item is Panel)
                {
                    FindAll<T>(item as Panel, collection);
                }
            }

        }


        #region - 滚动条功能 -

        private void scrolls_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            this.ScrollMove(e.Source);

        }

        private void scrolls_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            this.ScrollMove(e.Source);
        }

        Point last;
        void ScrollMove(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            double y = temp.Y - last.Y;

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset - y);

            last = temp;

            this.CheckPosition();

        }

        void GetLast(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            last = temp;
        }

        private void scrolls_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void scrolls_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            //this.scrolls.LineUp();

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset - 120);

            this.CheckPosition();
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            //this.scrolls.LineDown();

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset + 120);

            this.CheckPosition();
        }

        void CheckPosition()
        {
            if (this.scrolls.VerticalOffset == 0)
            {
                //this.up.Visibility = Visibility.Hidden;
            }

            else if (IsVerticalScrollBarAtButtom(this.scrolls))
            {
                //this.down.Visibility = Visibility.Hidden;
            }
            else
            {
                //this.up.Visibility = Visibility.Visible;

                //this.down.Visibility = Visibility.Visible;
            }

        }

        public bool IsVerticalScrollBarAtButtom(ScrollViewer s)
        {
            bool isAtButtom = false;
            double dVer = s.VerticalOffset;
            double dViewport = s.ViewportHeight;
            double dExtent = s.ExtentHeight;
            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }

            return isAtButtom;
        }

        public void NextPage()
        {
            this.OnNext();
        }

        public void LastPage()
        {
            this.OnLast();
        }

        #endregion

        /// <summary> 创建内容 </summary>
        public static TPageControl Create(List<UserControl> ctrls)
        {
            TPageControl tPageControl1 = new TPageControl();

            if (ctrls.Count == 0) return tPageControl1;

            Action action = () =>
            {
                List<Panel> Panels = new List<Panel>();

                tPageControl1.ClearPage();

                foreach (var item in ctrls.Cast<System.Windows.Controls.Control>())
                {
                    WrapPanel rectangle = new WrapPanel();
                    rectangle.Width = tPageControl1.Width;
                    rectangle.Height = tPageControl1.Height;
                    WrapPanel w = item.Parent as WrapPanel;
                    if (w != null) w.Children.Clear();
                    rectangle.Children.Add(item);
                    Panels.Add(rectangle);
                }

                // Todo ：设置默认选中页
                tPageControl1.AddPage(Panels, 0);
            };


            tPageControl1.Loaded += (object sender, System.Windows.RoutedEventArgs e) =>
            {
                action();

            };

            return tPageControl1;
        }

        public void AddControls(List<UserControl> ctrls)
        {
            Action action = () =>
            {
                List<Panel> Panels = new List<Panel>();

                this.ClearPage();

                foreach (var item in ctrls.Cast<System.Windows.Controls.Control>())
                {
                    WrapPanel rectangle = new WrapPanel();
                    rectangle.Width = this.Width;
                    rectangle.Height = this.Height;
                    WrapPanel w = item.Parent as WrapPanel;
                    if (w != null) w.Children.Clear();
                    rectangle.Children.Add(item);
                    Panels.Add(rectangle);
                }

                // Todo ：设置默认选中页
                this.AddPage(Panels, 0);
            };

            action();

            this.Loaded += (object sender, System.Windows.RoutedEventArgs e) =>
            {
                action();

            };
        }

        public List<UserControl> BindControls
        {
            get { return (List<UserControl>)GetValue(BindControlsProperty); }
            set { SetValue(BindControlsProperty, value); }
        }

        public int PageSelect { get => pageSelect; set => pageSelect = value; }

        // Using a DependencyProperty as the backing store for BindControls.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindControlsProperty =
            DependencyProperty.Register("BindControls", typeof(List<UserControl>), typeof(TPageControl), new PropertyMetadata(null, PropertyChangedCallback));

        private void imageRight_Click(object sender, RoutedEventArgs e)
        {
            this.OnNext();
        }

        private void imageLeft_Click(object sender, RoutedEventArgs e)
        {
            this.OnLast();
        }

        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TPageControl control = d as TPageControl;

            if (control == null) return;

            List<UserControl> children = e.NewValue as List<UserControl>;

            if (children == null) return;

            control.AddControls(children);

        }

        #endregion
    }


    public static class ShareClass
    {
        /// <summary>
        /// 判断是否double类型,且大于等于0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool UnDoubleValueCheck(object value)
        {
            if (value.GetType() == typeof(double))
            {
                double radius = (double)value;
                if (radius < 0) return false;
                return true;
            }
            else
                return false;
        }

        //创建移动动画X轴
        internal static Storyboard CeaterAnimation_Xmove(DependencyObject element, double formX, double toX, double animationTime, double waitTime)
        {
            Storyboard storyboard = new Storyboard();
            {
                if (waitTime > 0)
                {
                    DoubleAnimationUsingKeyFrames animation = moveAnimationX(element, formX, formX, waitTime);
                    storyboard.Children.Add(animation);
                }
                if (animationTime >= 0)
                {
                    DoubleAnimationUsingKeyFrames animation = moveAnimationX(element, formX, toX, animationTime);
                    storyboard.Children.Add(animation);
                }
            }
            return storyboard;
        }

        //创建移动动画X轴
        private static DoubleAnimationUsingKeyFrames moveAnimationX(DependencyObject element, double formX, double toX, double moveAnimationTime)
        {
            //x方向动画
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, element);
            DependencyProperty[] propertyChain = new DependencyProperty[]
                    {
                        System.Windows.Controls.Control.RenderTransformProperty,
                        TransformGroup.ChildrenProperty,
                        TranslateTransform.XProperty,
                    };
            Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[3].(2)", propertyChain));
            //添加时间轴
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(formX, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 0))));
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(toX, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(moveAnimationTime))));
            return animation;
        }
    }
}
