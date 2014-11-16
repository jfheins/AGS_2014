using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace AGS_2014
{
	/// <summary>
	///    Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void EncryptBtn_Click(object sender, RoutedEventArgs e)
		{
			string plaintext = SourceTxtBox.Text;

			string cleantext = "";

			for (int i = 0; i < plaintext.Length; i++)
			{
				if (char.IsLetterOrDigit(plaintext[i]))
					cleantext += plaintext[i];
			}

			string ciphertext = "";
			char[][] matrix = MakeMatrix(cleantext, 4);

			foreach (char item in matrix[0])
				ciphertext += item;

			for (int i = 0; i < matrix[1].Length; i++)
			{
				ciphertext += matrix[1][i];
				if (i < matrix[3].Length)
					ciphertext += matrix[3][i];
			}

			foreach (char item in matrix[2])
				ciphertext += item;

			string finalCipher = "";
			int cipherindex = 0;

			for (int i = 0; i < plaintext.Length; i++)
			{
				if (char.IsLetterOrDigit(plaintext[i]))
					finalCipher += ciphertext[cipherindex++];
				else
					finalCipher += plaintext[i];
			}
			TargetTxtBox.Text = finalCipher;
		}

		private void DecryptBtn_Click(object sender, RoutedEventArgs e)
		{
			var sw = new Stopwatch();
			sw.Start();
			string ciphertext = SourceTxtBox.Text;

			string cleantext = "";

			for (int i = 0; i < ciphertext.Length; i++)
			{
				if (char.IsLetterOrDigit(ciphertext[i]))
					cleantext += ciphertext[i];
			}

			string plaintext = "";

			int rows = cleantext.Length/4;
			int remainder = cleantext.Length%4;

			string col1, col3, col24;
			var col2 = new char[rows];
			var col4 = new char[rows];

			switch (remainder)
			{
				case 0:
					// Geht genau auf
					col1 = cleantext.Substring(0, rows);
					col24 = cleantext.Substring(rows, rows*2);
					col3 = cleantext.Substring(3*rows, rows);
					break;

				case 1:
					col1 = cleantext.Substring(0, rows + 1);
					col24 = cleantext.Substring(rows + 1, rows*2);
					col3 = cleantext.Substring(3*rows + 1, rows);
					break;

				case 2:
					col2 = new char[rows + 1];
					col1 = cleantext.Substring(0, rows + 1);
					col24 = cleantext.Substring(rows + 1, rows*2 + 1);
					col3 = cleantext.Substring(3*rows + 2, rows);
					break;

				case 3:
					col2 = new char[rows + 1];
					col1 = cleantext.Substring(0, rows + 1);
					col24 = cleantext.Substring(rows + 1, rows*2 + 1);
					col3 = cleantext.Substring(3*rows + 2, rows + 1);
					break;

				default:
					return;
			}

			for (int i = 0; i < col24.Length; i++)
			{
				if (i%2 == 0)
					col2[i/2] = col24[i];
				else
					col4[i/2] = col24[i];
			}

			for (int i = 0; i < rows + 1; i++)
			{
				try
				{
					plaintext += col1[i];
					plaintext += col2[i];
					plaintext += col3[i];
					plaintext += col4[i];
				}
				catch (IndexOutOfRangeException)
				{
					break;
				}
			}

			string finalPlaintext = "";
			int pindex = 0;
			foreach (var c in ciphertext)
			{
				if (char.IsLetterOrDigit(c))
					finalPlaintext += plaintext[pindex++];
				else
					finalPlaintext += c;
			}
			sw.Stop();
			statusLbl.Content = string.Format("Das Entschlüsseln von {0} Zeichen dauerte {1}ms", plaintext.Length, sw.ElapsedMilliseconds);
			TargetTxtBox.Text = finalPlaintext;
		}

		private static char[][] MakeMatrix(string text, int columns)
		{
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