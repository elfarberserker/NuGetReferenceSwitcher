//-----------------------------------------------------------------------
// <copyright file="MainDialog.xaml.cs" company="NuGet Reference Switcher">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>http://nugetreferenceswitcher.codeplex.com/license</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using EnvDTE;

using Microsoft.VisualStudio.PlatformUI;

using MyToolkit.Collections;
using MyToolkit.Mvvm;
using NuGetReferenceSwitcher.Presentation.Models;
using NuGetReferenceSwitcher.Presentation.ViewModels;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Window = System.Windows.Window;

namespace NuGetReferenceSwitcher.Presentation.Views
{
    /// <summary>Interaction logic for MainDialog.xaml </summary>
    public partial class MainDialog : DialogWindow
    {
        private OpenFileDialog _dlg;
        
        /// <summary>Initializes a new instance of the <see cref="MainDialog"/> class. </summary>
        /// <param name="application">The application object. </param>
        /// <param name="extensionAssembly">The assembly of the extension. </param>
        public MainDialog(DTE application, Assembly extensionAssembly)
        {
            InitializeComponent();

            Model.ExtensionAssembly = extensionAssembly; 
            Model.Application = application;
            Model.Dispatcher = Dispatcher;
            Model.Config = new Configuration.ConfigManager(application);

            ViewModelHelper.RegisterViewModel(Model, this);

            Model.Projects.ExtendedCollectionChanged += OnProjectsChanged;
            KeyUp += OnKeyUp;
        }

        /// <summary>Gets the view model. </summary>
        public MainDialogModel Model
        {
            get { return (MainDialogModel)Resources["ViewModel"]; }
        }

        private void DialogWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OnOpenHyperlink(object sender, RoutedEventArgs e)
        {
            var uri = ((Hyperlink)sender).NavigateUri;
            System.Diagnostics.Process.Start(uri.ToString());
        }

        private void OnKeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
                Close();
        }

        private void OnProjectsChanged(object sender, ExtendedNotifyCollectionChangedEventArgs<ProjectModel> args)
        {
            if (Model.Projects.Any(p => p.CurrentToNuGetTransformations.Any()))
                Tabs.SelectedIndex = 1;
        }

        private async void OnSwitchToProjectReferences(object sender, RoutedEventArgs e)
        {
            await Model.SwitchToProjectReferencesAsync();
            Close();
        }

        private async void OnSwitchToNuGetReferences(object sender, RoutedEventArgs e)
        {
            await Model.SwitchToNuGetReferencesAsync();
            Close();
        }

        private async void Refresh(object sender, RoutedEventArgs e)
        {
            await Model.Refresh();
        }

        private async void SaveConfig(object sender, RoutedEventArgs e)
        {
            await Model.SaveConfiguration();
        }

        private void OnSelectProjectFile(object sender, RoutedEventArgs e)
        {
            var fntpSwitch = (FromNuGetToProjectTransformation)((Button)sender).Tag;
            if (_dlg == null)
            {
                _dlg = new OpenFileDialog();
                _dlg.Filter = "CSharp Projects (*.csproj)|*.csproj|VB.NET Projects (*.vbproj)|*.vbproj";

                // switch to VB if any VB project is already referenced
                if (Model.Transformations.Any(t => t.ToProjectPath != null && t.ToProjectPath.EndsWith(".vbproj", System.StringComparison.OrdinalIgnoreCase)))
                    _dlg.FilterIndex = 2;
            }

            _dlg.Title = string.Format("Select Project for '{0}'", fntpSwitch.FromAssemblyName);

            if (_dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fntpSwitch.ToProjectPath = _dlg.FileName;
        }

        private void SolutionFolders_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;
            comboBox.ItemsSource = Model.SolutionFolders;
            if (Model.Config.switchConfig != null)
            {
                var selected = comboBox.Items
                    .Cast<SolutionFolderModel>()
                    .Where(item => item.Path == Model.Config.switchConfig.rootFolder)
                    .FirstOrDefault();
                comboBox.SelectedItem = selected;
            }
        }
        private void SolutionFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;
            var selected = comboBox.SelectedItem as SolutionFolderModel;
            if (selected != null && Model.Config.switchConfig != null)
            {
                Model.Config.switchConfig.rootFolder = selected.Path;
            }
        }

        private void NugetFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (Model.Config.switchConfig != null)
            {
                textBox.Text = Model.Config.NugetFilter;
            }
        }
        private void NugetFilter_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (Model.Config.switchConfig != null)
            {
                Model.Config.switchConfig.nugetFilter = textBox.Text;
            }
        }
    }
}
