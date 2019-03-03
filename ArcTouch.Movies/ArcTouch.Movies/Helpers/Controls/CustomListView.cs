using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcTouch.Movies.Helpers.Controls
{
    public class CustomListView : ListView
    {
        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        
        public static readonly BindableProperty LoadCommandProperty =
            BindableProperty.Create(nameof(LoadCommand), typeof(ICommand), typeof(CustomListView), default(ICommand));

        
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)this.GetValue(ItemTappedCommandProperty); }
            set { this.SetValue(ItemTappedCommandProperty, value); }
        }
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(CustomListView), default(ICommand));


        public CustomListView()
        {
            this.ItemAppearing += ListViewItemAppearing;
            this.ItemTapped += ListViewItemTapped;
        }

        private void ListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && this.ItemTappedCommand != null && this.ItemTappedCommand.CanExecute(e))
            {
                this.ItemTappedCommand.Execute(e.Item);
                this.SelectedItem = e.Item;
            }
        }

        private void ListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (this.ItemsSource is IList items && e.Item == items[items.Count - 4])
                if (LoadCommand != null && LoadCommand.CanExecute(null))
                    LoadCommand.Execute(null);

        }
    }
}
