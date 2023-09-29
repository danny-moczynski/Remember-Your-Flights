namespace Lab2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Set the binding context to the BusinessLogic class, allowing the UI to interact with the domain logic.
            BindingContext = MauiProgram.BusinessLogic;
        }

        // This method is called when the "Add Airport" button is clicked.
        void AddAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                // Attempt to parse the rating and date visited input as integers and dates.
                if (int.TryParse(Rating.Text, out int rating) && DateTime.TryParse(DateVisited.Text, out DateTime dateVisited))
                {
                    // Call the domain logic to add an airport with the provided data.
                    MauiProgram.BusinessLogic.AddAirport(Id.Text, City.Text, dateVisited, rating);

                    // Clear the input fields after adding the airport.
                    Id.Text = "";
                    City.Text = "";
                    DateVisited.Text = "";
                    Rating.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the airport addition process.
                Console.WriteLine($"An error occurred: {ex}");
            }
        }

        // This method is called when the "Delete Airport" button is clicked.
        void DeleteAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            // Get the currently selected airport in the CollectionView.
            Airport currentAirport = CV.SelectedItem as Airport;

            if (currentAirport != null)
            {
                // Call the domain logic to delete the selected airport.
                MauiProgram.BusinessLogic.DeleteAirport(currentAirport.Id);
            }
        }

        // This method is called when the "Edit Airport" button is clicked.
        void EditAirport_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                // Get the currently selected airport in the CollectionView.
                Airport currentAirport = CV.SelectedItem as Airport;

                if (currentAirport != null && int.TryParse(Rating.Text, out int rating) && DateTime.TryParse(DateVisited.Text, out DateTime dateVisited))
                {
                    // Call the domain logic to edit the selected airport with the new data.
                    MauiProgram.BusinessLogic.EditAirport(currentAirport.Id, currentAirport.City, currentAirport.DateVisited, currentAirport.Rating);

                    // Update the selected airport's properties based on the input fields.
                    currentAirport.Id = Id.Text;
                    currentAirport.City = City.Text;
                    currentAirport.DateVisited = dateVisited;
                    currentAirport.Rating = rating;

                    // Clear the input fields after editing.
                    Id.Text = "";
                    City.Text = "";
                    DateVisited.Text = "";
                    Rating.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the airport editing process.
                Console.WriteLine($"An error occurred: {ex}");
            }
        }
    }
}
