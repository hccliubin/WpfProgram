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
using System.Collections.ObjectModel;

namespace HeBianGu.Control.DockPanelControl
{
    public class ManagedContentCollection<T> : ReadOnlyObservableCollection<T> where T : ManagedContent
    {
        internal ManagedContentCollection(DockingManager manager)
            : base(new ObservableCollection<T>())
        {
            Manager = manager;
        }


        /// <summary>
        /// Get associated <see cref="DockingManager"/> object
        /// </summary>
        public DockingManager Manager { get; private set; }

        /// <summary>
        /// Override collection changed event to setup manager property on <see cref="ManagedContent"/> objects
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (T cntAdded in e.NewItems)
                    cntAdded.Manager = Manager;
            }

            base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Add a content to the list
        /// </summary>
        /// <param name="contentToAdd"></param>
        internal void Add(T contentToAdd)
        {
            if (!Items.Contains(contentToAdd))
                Items.Add(contentToAdd);
        }

        internal void Remove(T contentToRemove)
        {
            Items.Remove(contentToRemove);
        }
    }
}
