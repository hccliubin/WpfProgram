﻿/************************************************************************

   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the New BSD
   License (BSD) as published at http://avalondock.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up AvalonDock in Extended WPF Toolkit Plus at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like facebook.com/datagrids

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace HeBianGu.Control.DockPanelControl
{
    public class AvalonDockWindow : Window
    {
        static AvalonDockWindow()
        {
            ShowInTaskbarProperty.OverrideMetadata(typeof(AvalonDockWindow), new FrameworkPropertyMetadata(false));

        }

        internal AvalonDockWindow()
        { }
    }
}
