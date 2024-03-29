﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.Controls.ModernControl
{
    /// <summary>
    /// Adds icon content to a standard button.
    /// </summary>
    public class ModernToggleButton
        : ToggleButton
    {
        /// <summary>
        /// Identifies the EllipseDiameter property.
        /// </summary>
        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ModernToggleButton), new PropertyMetadata(22D));
        /// <summary>
        /// Identifies the EllipseStrokeThickness property.
        /// </summary>
        public static readonly DependencyProperty EllipseStrokeThicknessProperty = DependencyProperty.Register("EllipseStrokeThickness", typeof(double), typeof(ModernToggleButton), new PropertyMetadata(1D));
        /// <summary>
        /// Identifies the IconData property.
        /// </summary>
        public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register("IconData", typeof(Geometry), typeof(ModernToggleButton));
        /// <summary>
        /// Identifies the IconHeight property.
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(ModernToggleButton), new PropertyMetadata(12D));
        /// <summary>
        /// Identifies the IconWidth property.
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(ModernToggleButton), new PropertyMetadata(12D));

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernToggleButton"/> class.
        /// </summary>
        public ModernToggleButton()
        {
            this.DefaultStyleKey = typeof(ModernToggleButton);
        }

        /// <summary>
        /// Gets or sets the ellipse diameter.
        /// </summary>
        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ellipse stroke thickness.
        /// </summary>
        public double EllipseStrokeThickness
        {
            get { return (double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon path data.
        /// </summary>
        /// <value>
        /// The icon path data.
        /// </value>
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon height.
        /// </summary>
        /// <value>
        /// The icon height.
        /// </value>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon width.
        /// </summary>
        /// <value>
        /// The icon width.
        /// </value>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
    }
}
