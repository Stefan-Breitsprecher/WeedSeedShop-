using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.Json;

namespace WeedSeedShop
{
	// Die Hauptklasse des Programms
	class Program
	{
		// Einstiegspunkt des Programms
		static void Main(string[] args)
		{
			// UTF-8 für die korrekte Anzeige des Euro-Zeichens
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			// Erstellen einer neuen Instanz des Shops
			CannabisShop shop = new CannabisShop();
			shop.Start();
		}
	}


}