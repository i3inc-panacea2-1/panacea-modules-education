using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Modules.Education.Controls
{
    public class TreeMenu : TreeView
    {
        static TreeMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeMenu), new FrameworkPropertyMetadata(typeof(TreeMenu)));
        }
        public TreeMenu()
            : base()
        {
            this.SelectedItemChanged += ___ICH;
            Loaded += TreeMenu_Loaded;
        }

        private void TreeMenu_Loaded(object sender, RoutedEventArgs e)
        {

            if (VisualChildrenCount > 0)
            {
                var grid = this.GetVisualChild(0) as Grid;
                if (grid != null && grid.ColumnDefinitions.Count == 3)
                {
                    grid.ColumnDefinitions.RemoveAt(2);
                    grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                }
            }

        }

        void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }

        public object SelectedItem_
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }
        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("SelectedItem_", typeof(object), typeof(TreeMenu), new UIPropertyMetadata(null));

    }
}
