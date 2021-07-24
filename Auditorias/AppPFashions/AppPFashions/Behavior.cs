using AppPFashions.Models;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppPFashions
{
    class SfListViewAccordionBehavior : Behavior<ContentPage>
    {
        #region Fields

        private caudit00 tappedItem;
        private Syncfusion.ListView.XForms.SfListView listview;
        private AccordionViewModel AccordionViewModel;

        #endregion

        #region Properties
        public SfListViewAccordionBehavior()
        {
            AccordionViewModel = new AccordionViewModel();
        }

        #endregion

        #region Override Methods

        protected override void OnAttachedTo(ContentPage bindable)
        {
            listview = bindable.FindByName<Syncfusion.ListView.XForms.SfListView>("listView");
            listview.ItemsSource = AccordionViewModel.ContactsInfo;
            listview.ItemTapped += ListView_ItemTapped;
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            listview.ItemTapped -= ListView_ItemTapped;
        }
        #endregion

        #region Private Methods

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (tappedItem != null && tappedItem.IsVisible)
            {
                var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);

                tappedItem.IsVisible = false;

                if (Device.RuntimePlatform != Device.macOS)
                    Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(previousIndex, previousIndex, false); });
            }

            if (tappedItem == (e.ItemData as caudit00))
            {
                if (Device.RuntimePlatform == Device.macOS)
                {
                    var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);
                    Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(previousIndex, previousIndex, false); });
                }

                tappedItem = null;
                return;
            }

            tappedItem = e.ItemData as caudit00;
            tappedItem.IsVisible = true;

            if (Device.RuntimePlatform == Device.macOS)
            {
                var visibleLines = this.listview.GetVisualContainer().ScrollRows.GetVisibleLines();
                var firstIndex = visibleLines[visibleLines.FirstBodyVisibleIndex].LineIndex;
                var lastIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;
                Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(firstIndex, lastIndex, false); });
            }
            else
            {
                var currentIndex = listview.DataSource.DisplayItems.IndexOf(e.ItemData);
                Device.BeginInvokeOnMainThread(() => { listview.RefreshListViewItem(currentIndex, currentIndex, false); });
            }
        }

        #endregion
    }
}
