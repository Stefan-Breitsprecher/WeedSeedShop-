using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeedSeedShop
{
	// Die Klasse JsonDataHandler kümmert sich um das Laden und Speichern von Kundendaten
	public static class JsonDataHandler
	{
		private const string FilePath = "customers.json"; // Der Pfad zur JSON-Datei, in der Kundendaten gespeichert werden

		// Methode zum Laden der Kunden aus einer JSON-Datei
		public static List<Customer> LoadCustomers()
		{
			// Überprüfen, ob die Datei existiert
			if (!File.Exists(FilePath))
				return new List<Customer>(); // Gebe eine leere Liste zurück, wenn die Datei nicht existiert

			// Lese den Inhalt der Datei
			string json = File.ReadAllText(FilePath);
			// Deserialisiere den JSON-Inhalt in eine Liste von Kunden
			return JsonSerializer.Deserialize<List<Customer>>(json);
		}

		// Methode zum Speichern der Kunden in einer JSON-Datei
		public static void SaveCustomers(List<Customer> customers)
		{
			// Serialisiere die Liste der Kunden in JSON-Format
			string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true }); // Mit Einrückungen für bessere Lesbarkeit
																												   // JSON-String wird in die Datei
			File.WriteAllText(FilePath, json);
		}
	}
}
