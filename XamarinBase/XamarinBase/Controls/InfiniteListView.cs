using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using static Xamarin.Forms.BindableProperty;

namespace XamarinBase.Controls
{
    /// <summary>
    /// A List View that is used to act as a infinite list view
    /// Exposes a property to load more movies into the list view.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ListView" />
    public class InfiniteListView : ListView
    {
        /// <summary>
        /// The load more command property
        /// Exposes the <see cref="LoadMoreCommand"/> for this list view
        /// </summary>
        public static readonly BindableProperty LoadMoreCommandProperty = Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteListView));

        public InfiniteListView()
        {
            ItemAppearing += OnItemAppearing;
        }

        /// <summary>
        /// Gets or sets the load more command.
        /// </summary>
        /// <value>
        /// Command used to load more data into the list view. Bind to this command to have more data loaded into the list view.
        /// This command is executed when the list view gets close to showing the last item in the list.
        /// </value>
        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs itemVisibilityEventArgs)
        {
            var items = ItemsSource as IList;

            // if we aren't yet to the end of the list then no reason to execute the load more command.
            if (items == null || itemVisibilityEventArgs.Item != items[items.Count - 1]) return;

            if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
            {
                LoadMoreCommand.Execute(null);
            }
        }
    }
}
