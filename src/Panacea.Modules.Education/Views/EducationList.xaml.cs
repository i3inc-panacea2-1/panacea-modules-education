﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Panacea.Modules.Education.Views
{
    /// <summary>
    /// Interaction logic for EducationList.xaml
    /// </summary>
    public partial class EducationList : UserControl
    {
        public EducationList()
        {
            InitializeComponent();
        }

        private void TreeMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if ((TreeMenuControl.ItemContainerGenerator.ContainerFromItem(e.NewValue) as TreeViewItem) != null)
                {
                    foreach (var it in TreeMenuControl.Items)
                    {
                        (TreeMenuControl.ItemContainerGenerator.ContainerFromItem(it) as TreeViewItem).IsExpanded =
                            false;
                    }
                    (TreeMenuControl.ItemContainerGenerator.ContainerFromItem(e.NewValue) as TreeViewItem)
                        .IsExpanded =
                        true;
                }
            }
            catch
            {
            }
        }

        private void Lst_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (SelectedCategory == null) return;
            //SelectedCategory.ContainerHeight = e.NewSize.Height;
        }
    }
}
