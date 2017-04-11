using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using static Xamarin.Forms.BindableProperty;

namespace SampleProject.Controls
{
    public class InfiniteListView : ListView
    {
        public static readonly BindableProperty LoadMoreCommandProperty = Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteListView));

        public ICommand LoadMoreCommand
        {
            get => (ICommand) GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        public InfiniteListView()
        {
            ItemAppearing += OnItemAppearing;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs itemVisibilityEventArgs)
        {
            var items = ItemsSource as IList;

            if (items == null || itemVisibilityEventArgs.Item != items[items.Count - 1]) return;

            if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
            {
                LoadMoreCommand.Execute(null);
            }
        }
    }
}
