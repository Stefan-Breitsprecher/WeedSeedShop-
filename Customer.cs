using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeedSeedShop
{
	// Die Klasse Customer repräsentiert einen Kunden
	public class Customer
	{
		public static readonly List<Customer> customers = JsonDataHandler.LoadCustomers();
		public string Name { get; set; }
		public string Email { get; set; }
		public string Lastname { get; set; }
		public string Kundennummer { get; private set; }

		// Konstruktor zur Initialisierung eines Kunden
		public Customer(string name, string email, string lastname)
		{
			Name = name;
			Lastname = lastname;
			Email = email;
			Kundennummer = GenerateCustomerID();// Übergibt die Kundenliste zur Überprüfung
			customers.Add(this);
		}
		public static void SaveCustomers()
		{
			JsonDataHandler.SaveCustomers(customers);
		}
		public static void LoadCustomers()
		{
			customers.AddRange(JsonDataHandler.LoadCustomers());

		}

		// Methode zur Generierung einer zufälligen Kundennummer
		private string GenerateCustomerID()  // customers wird als Parameter übergeben
		{
			Random random = new Random();
			string newKundennummer;
			do
			{
				newKundennummer = "KundenNr" +
					"" +
					"-" + random.Next(1000, 9999);   // erzeugt automatische Kunden Nr
			} while (customers.Any(c => c.Kundennummer == newKundennummer)); // Prüft, ob die Nummer existiert
			return newKundennummer;
		}
	}
}
