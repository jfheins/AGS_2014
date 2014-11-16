using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AGS_2014
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void EncryptBtn_Click(object sender, RoutedEventArgs e)
		{
			var plaintext = SourceTxtBox.Text;

			string cleantext = "";

         for (int i = 0; i < plaintext.Length; i++)
         {
               if (char.IsLetterOrDigit(plaintext[i]))
                  cleantext += plaintext[i];
         }

			string ciphertext = "";
			var matrix = MakeMatrix(cleantext, 4);
			foreach (var item in matrix[0])
				ciphertext += item;

			for (int i = 0; i < matrix[1].Length; i++)
			{
				ciphertext +=  matrix[1][i];
				if (i < matrix[3].Length)
					ciphertext +=  matrix[3][i];
			}

			foreach (var item in matrix[2])
				ciphertext += item;

			string final_cipher = "";
			int cipherindex = 0;
			for (int i = 0; i < plaintext.Length; i++)
			{
				if (char.IsLetterOrDigit(plaintext[i]))
					final_cipher += ciphertext[cipherindex++];
				else
					final_cipher += plaintext[i];
			}
			TargetTxtBox.Text = final_cipher;
		}

		private void DecryptBtn_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine(MakeMatrix("0123456789abcdefg", 4));
		}

		private static char[][] MakeMatrix(string text, int columns)
		{
			var rows = (int)((text.Length - 1) / columns + 1);
			var result = new char[columns][];

			for (int col = 0; col < columns; col++)
			{
				var chars = new List<char>();
				for (int pos = col; pos < text.Length; pos += columns)
				{
					chars.Add(text[pos]);
				}
				result[col] = chars.ToArray();
			}

			return result;
		}
	}
}
