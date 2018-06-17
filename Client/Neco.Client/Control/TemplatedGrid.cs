using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Neco.Client.Control
{
    public class TemplatedGrid : Grid
    {
        #region Overrides

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            CreateGrid();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == TemplatedGrid.ItemsSourceProperty.PropertyName)
            {
                CreateGrid();
            }

            base.OnPropertyChanged(propertyName);
        }

        #endregion

        #region Public Properties

        public static readonly BindableProperty ItemsSourceProperty = 
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<IEnumerable<object>>), typeof(TemplatedGrid), null);
        public IEnumerable<IEnumerable<object>> ItemsSource
        {
            get { return (IEnumerable<IEnumerable<object>>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value);  }
        }

        public static readonly BindableProperty ItemTemplateProperty = 
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(TemplatedGrid), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty);  }
            set { SetValue(ItemTemplateProperty, value);  }
        }

        public static readonly BindableProperty VerticalContentAlignmentProperty =
            BindableProperty.Create(nameof(VerticalContentAlignment), typeof(TemplatedGridContentAlignment), typeof(TemplatedGrid), TemplatedGridContentAlignment.Default);
        public TemplatedGridContentAlignment VerticalContentAlignment
        {
            get { return (TemplatedGridContentAlignment)GetValue(VerticalContentAlignmentProperty);  }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        public static readonly BindableProperty HorizontalContentAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalContentAlignment), typeof(TemplatedGridContentAlignment), typeof(TemplatedGrid), TemplatedGridContentAlignment.Default);
        public TemplatedGridContentAlignment HorizontalContentAlignment
        {
            get { return (TemplatedGridContentAlignment) GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        #endregion

        #region Private Methods

        private void CreateGrid()
        {
            //Check for data
            if(this.ItemsSource == null || this.ItemsSource.Count() == 0 || this.ItemsSource.First().Count() == 0)
            {
                return;
            }

            //Create the grid
            this.RowDefinitions = CreateRowDefinitions();
            this.ColumnDefinitions = CreateColumnDefinitions();

            CreateCells();
        }

        private RowDefinitionCollection CreateRowDefinitions()
        {
            var rowDefinitions = new RowDefinitionCollection();

            if(this.VerticalContentAlignment == TemplatedGridContentAlignment.Center)
            {
                rowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            foreach(var row in this.ItemsSource.First())
            {
                rowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            if(this.VerticalContentAlignment == TemplatedGridContentAlignment.Center)
            {
                rowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            return rowDefinitions;
        }

        private ColumnDefinitionCollection CreateColumnDefinitions()
        {
            var columnDefinitions = new ColumnDefinitionCollection();

            if (this.VerticalContentAlignment == TemplatedGridContentAlignment.Center)
            {
                columnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            foreach (var row in this.ItemsSource)
            {
                columnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            if (this.VerticalContentAlignment == TemplatedGridContentAlignment.Center)
            {
                columnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            return columnDefinitions;
        }

        private void CreateCells()
        {
            int startRowIndex = this.VerticalContentAlignment == TemplatedGridContentAlignment.Center ? 1 : 0;
            int colIndex = this.HorizontalContentAlignment == TemplatedGridContentAlignment.Center ? 1 : 0;

            foreach(var column in this.ItemsSource)
            {
                var rowIndex = startRowIndex;

                foreach(var item in column)
                {
                    this.Children.Add(CreateCellView(item), colIndex, rowIndex);
                    rowIndex++;
                }

                colIndex++;
            }
        }

        private Xamarin.Forms.View CreateCellView(object item)
        {
            var view = (Xamarin.Forms.View)this.ItemTemplate.CreateContent();
            var bindableObject = (BindableObject)view;

            if(bindableObject != null)
            {
                bindableObject.BindingContext = item;
            }

            return view;
        }

        #endregion
    }

    public enum TemplatedGridContentAlignment
    {
        Default,
        Center
    }
}
