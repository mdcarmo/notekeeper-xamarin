using NoteKeepper.Models;
using NoteKeepper.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteKeepper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ItemsViewModel()
        {
            Title = "NoteKeeper";
            Notes = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "SaveNote",
                async (sender, note) =>
                {
                    Notes.Add(note);
                    await NoteDataStore.AddNoteAsync(note);
                }
                );

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "UpdateNote",
                async (sender, note) =>
                {
                    await NoteDataStore.UpdateNoteAsync(note);

                    await ExecuteLoadItemsCommand();
                }
                );
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await NoteDataStore.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}