using System;

namespace LoginFlow.Models
{
    public class EditUserPage : ContentPage
    {
        private Usuario originalUser;
        private Usuario editedUser;

        private Entry nameEntry;
        private Entry emailEntry;
        private Entry passwordEntry;

        public EditUserPage(Usuario selectedItem)
        {
            originalUser = selectedItem;

            Title = "Editar usuario";
            BackgroundColor = Colors.WhiteSmoke;

            nameEntry = new Entry
            {
                Placeholder = "Name",
                Text = originalUser.username
            };
            emailEntry = new Entry
            {
                Placeholder = "Email",
                Text = originalUser.email
            };
            passwordEntry = new Entry
            {
                Placeholder = "Password",
                Text = originalUser.password
            };

            var saveButton = new Button
            {
                Text = "Save"
            };
            saveButton.Clicked += SaveButton_Clicked;

            var cancelButton = new Button
            {
                Text = "Cancel"
            };
            cancelButton.Clicked += CancelButton_Clicked;

            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children =
                {
                    nameEntry,
                    emailEntry,
                    passwordEntry,
                    saveButton,
                    cancelButton
                }
            };
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            editedUser = new Usuario
            {
                username = nameEntry.Text,
                email = emailEntry.Text,
                password = passwordEntry.Text
            };

            Navigation.PopModalAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            editedUser = null;
            Navigation.PopModalAsync();
        }

        public Usuario EditedUser
        {
            get { return editedUser; }
        }
    }
}
