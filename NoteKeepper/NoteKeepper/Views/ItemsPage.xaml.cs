using NoteKeepper.Models;
using NoteKeepper.ViewModels;
using NoteKeepper.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteKeepper.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var note = args.SelectedItem as Note;
            if (note == null)
                return;

            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(note))));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Notes.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}