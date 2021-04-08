using NoteKeepper.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace NoteKeepper.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            var message = viewModel.IsNewNote ? "SaveNote" : "UpdateNote";
            MessagingCenter.Send(this, message, viewModel.Note);
            Navigation.PopModalAsync();
        }
    }
}