using System;

namespace LoginFlow.Models
{
    public class EditUserPage : ContentPage
    {
        private User originalUser;
        private User editedUser;

        private Entry nameEntry;
        private Entry emailEntry;
        private Entry passwordEntry;

        public EditUserPage(User selectedItem)
        {
            originalUser = selectedItem;

            Title = "Editar usuario";
            BackgroundColor = Colors.WhiteSmoke;

            nameEntry = new Entry
            {
                Placeholder = "Name",
                Text = originalUser.Name
            };
            emailEntry = new Entry
            {
                Placeholder = "Email",
                Text = originalUser.Email
            };
            passwordEntry = new Entry
            {
                Placeholder = "Password",
                Text = originalUser.Password
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
            editedUser = new User
            {
                Name = nameEntry.Text,
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            Navigation.PopModalAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            editedUser = null;
            Navigation.PopModalAsync();
        }

        public User EditedUser
        {
            get { return editedUser; }
        }
    }
}
