using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace VolumeFeedback
{
	public class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

		public void button_Click(object sender, RoutedEventArgs e)
		{
			// Change button text when button is clicked.
			Button button = (Button)sender;
			button.Content = "Hello, Avalonia!";
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}