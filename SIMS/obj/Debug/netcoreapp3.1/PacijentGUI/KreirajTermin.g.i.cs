﻿#pragma checksum "..\..\..\..\PacijentGUI\KreirajTermin.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06CC4A103725D3F9059E9D5A481F0BFDB3A2F6F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SIMS;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace SIMS {
    
    
    /// <summary>
    /// KreirajTermin
    /// </summary>
    public partial class KreirajTermin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox doktori;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePicker1;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox terminiLista;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Potvrdi;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SIMS;component/pacijentgui/kreirajtermin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.doktori = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            this.doktori.DropDownOpened += new System.EventHandler(this.doktori_DropDownOpened);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            this.doktori.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.doktori_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.datePicker1 = ((System.Windows.Controls.DatePicker)(target));
            
            #line 20 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            this.datePicker1.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePicker1_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.terminiLista = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.Potvrdi = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            this.Potvrdi.Click += new System.Windows.RoutedEventHandler(this.Potvrdi_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 30 "..\..\..\..\PacijentGUI\KreirajTermin.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Odbaci_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

