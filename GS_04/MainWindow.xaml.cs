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

namespace GS_04
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

		private void Button_Click(object sender, RoutedEventArgs evta)
		{
			for (int t = 0; t < 10; t++)
			{
				for (int h = 0; h < 10; h++)
				{
					for (int r = 0; r < 10; r++)
					{
						for (int e = 0; e < 10; e++)
						{
							for (int w = 0; w < 10; w++)
							{
								for (int o = 0; o < 10; o++)
								{
									for (int n = 0; n < 10; n++)
									{
										int result =
											(t * 10000 + h * 1000 + r * 100 + e * 10 + e) * 2 + (t * 100 + w * 10 + o) * 2 + o * 100 + n * 10 + e;
										if (result % 10 == n)
										{
											if (result % 100 / 10 == e && result % 10000 / 1000 == e && result % 1000000 / 100000 == e)
											{
												var l = result % 100000 / 10000;
												var v = result % 1000 / 100;
												if (AllUnequal(t, h, r, e, w, o, n, l, v))
												{
												Console.WriteLine(result);
												Console.WriteLine("t:{0} h:{1} r:{2} e:{3} w:{4} o:{5} n:{6} l:{7} v:{8}", t, h, r, e, w, o, n, l, v);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private static bool AllUnequal(params int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				for (int j = i+1; j < values.Length; j++)
				{
					if (values[i] == values[j])
						return false;
					
				}
			}
			return true;
		}
	}
}
