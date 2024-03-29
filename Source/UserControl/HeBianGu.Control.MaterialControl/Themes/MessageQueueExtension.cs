﻿using System;
using System.Windows.Markup;

namespace HeBianGu.Controls.MaterialControl
{
    /// <summary>
    /// Provides shorthand to initialise a new <see cref="SnackbarMessageQueue"/> for a <see cref="Snackbar"/>.
    /// </summary>
    [MarkupExtensionReturnType(typeof(SnackbarMessageQueue))]    
    public class MessageQueueExtension : MarkupExtension
    {        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {            
            return new SnackbarMessageQueue();
        }
    }
}