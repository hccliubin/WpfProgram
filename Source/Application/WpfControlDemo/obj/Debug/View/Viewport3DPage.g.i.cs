﻿#pragma checksum "..\..\..\View\Viewport3DPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "540F77144BFA66010615E67F6C878C897A5979E3"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfControlDemo.View;


namespace WpfControlDemo.View {
    
    
    /// <summary>
    /// Viewport3DPage
    /// </summary>
    public partial class Viewport3DPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 57 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewport3D view;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.PerspectiveCamera camera;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.RotateTransform3D rot;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.AxisAngleRotation3D camRotation;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FrontSide;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LeftSide;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BackSide;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\View\Viewport3DPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border RightSide;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfControlDemo;component/view/viewport3dpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Viewport3DPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.view = ((System.Windows.Controls.Viewport3D)(target));
            return;
            case 2:
            this.camera = ((System.Windows.Media.Media3D.PerspectiveCamera)(target));
            return;
            case 3:
            this.rot = ((System.Windows.Media.Media3D.RotateTransform3D)(target));
            return;
            case 4:
            this.camRotation = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 5:
            this.FrontSide = ((System.Windows.Controls.Border)(target));
            
            #line 85 "..\..\..\View\Viewport3DPage.xaml"
            this.FrontSide.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.FrontSide_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LeftSide = ((System.Windows.Controls.Border)(target));
            
            #line 138 "..\..\..\View\Viewport3DPage.xaml"
            this.LeftSide.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LeftSide_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BackSide = ((System.Windows.Controls.Border)(target));
            
            #line 181 "..\..\..\View\Viewport3DPage.xaml"
            this.BackSide.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BackSide_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RightSide = ((System.Windows.Controls.Border)(target));
            
            #line 214 "..\..\..\View\Viewport3DPage.xaml"
            this.RightSide.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RightSide_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

