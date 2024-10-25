using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeedSeedShop
{
	// Die Klasse CannabisShop verwaltet den Einkaufsvorgang
	public class CannabisShop
	{
		private Dictionary<string, (decimal price, decimal discount, string description)> cannabisSeeds;
		private Dictionary<string, (decimal price, decimal discount)> accessories;
		private List<string> cart;
		//private List<Customer> customers;

		// Konstruktor, der die Produkte und den Warenkorb initialisiert
		public CannabisShop()
		{
			cart = new List<string>();
			Customer.LoadCustomers(); // Kunden aus der JSON-Datei laden
			InitializeProducts();
		}

		// Methode zur Initialisierung der Produkte
		private void InitializeProducts()
		{
			// Produkte mit Preisen und Beschreibungen für Cannabis-Samen
			cannabisSeeds = new Dictionary<string, (decimal, decimal, string)>()
			{
				{ "Northern Lights", (15.99m, 0.1m, "Wuchszeit: 6-8 Wochen, Blütezeit: 8-9 Wochen, THC: 18-22%, Wirkung: Entspannt, beruhigend") }, // 10% Rabatt
                { "Blueberry", (8.99m, 0.15m, "Wuchszeit: 7-9 Wochen, Blütezeit: 8-10 Wochen, THC: 19-24%, Wirkung: Euphorisch, kreativ") }, // 15% Rabatt
                { "Amnesia Haze", (11.99m, 0.05m, "Wuchszeit: 8-10 Wochen, Blütezeit: 10-12 Wochen, THC: 20-25%, Wirkung: Energetisch, erhebend") }, // 5% Rabatt
				{ "Lutz und Grub Skunk #4", (34000.99m, 0.05m, "Wuchszeit: 104 Wochen, Blütezeit: finde es selbst raus, THC: so-la-la,\n   Wirkung: some Times maby good some Time maby shit") }
			};

			// Produkte mit Preisen für Zubehör
			accessories = new Dictionary<string, (decimal, decimal)>()
			{
				{ "Pflanztopf", (14.99m, 0.0m) }, // kein Rabatt
                { "Bio Dünger", (19.99m, 0.1m) }, // 10% Rabatt
                { "Growbox", (149.99m, 0.2m) } // 20% Rabatt
            };
		}

		// Methode, um den Einkaufsvorgang zu starten
		public void Start()
		{
			bool shopping = true;

			while (shopping)
			{
				Console.Clear();
				Console.WriteLine("\n\nWillkommen bei WeedSeedShop \t++++ Happy 420 SALE++++\n");
				Console.WriteLine("Wählen Sie eine Kategorie aus:\n");
				Console.WriteLine("1. Cannabis Samen");
				Console.WriteLine("2. Zubehör");
				Console.WriteLine("3. Warenkorb ansehen");
				Console.WriteLine("4. Bestellung abschließen");
				Console.WriteLine("5. Kundenkonto erstellen");
				Console.WriteLine("6. Als Kunde einloggen");
				Console.WriteLine("7. Beenden\n");

				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						DisplaySeeds();
						break;
					case "2":
						DisplayAccessories();
						break;
					case "3":
						ViewCart();
						break;
					case "4":
						Checkout();
						shopping = false; // Beende den Einkauf
						break;
					case "5":
						CreateCustomerAccount();
						break;
					case "6":
						Login();
						break;
					case "7":
						shopping = false; // Beende das Programm
						break;
					default:
						Console.WriteLine("Ungültige Auswahl. Bitte erneut versuchen.");
						break;
				}
			}

			Console.WriteLine("Vielen Dank für Ihren Einkauf bei Cannabis Zubehör!");
		}

		// Methode zur Anzeige der Cannabis-Samen
		private void DisplaySeeds()
		{
			Console.Clear();
			Console.WriteLine("Verfügbare Cannabis-Samen:");

			int i = 1;
			foreach (var seed in cannabisSeeds)
			{
				decimal finalPrice = seed.Value.price * (1 - seed.Value.discount);
				Console.WriteLine($"{i}. {seed.Key} - Originalpreis: {seed.Value.price:C}, Preis nach Rabatt: {finalPrice:C}");
				Console.WriteLine($"   Beschreibung: {seed.Value.description}\n");
				i++;
			}

			Console.WriteLine("Geben Sie die Nummer des Produkts ein, das Sie kaufen möchten, oder drücken Sie Enter, um zurückzukehren:");
			string input = Console.ReadLine();

			if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= cannabisSeeds.Count)
			{
				string selectedSeed = new List<string>(cannabisSeeds.Keys)[itemIndex - 1];
				cart.Add(selectedSeed);
				Console.WriteLine($"{selectedSeed} wurde zum Warenkorb hinzugefügt.");
			}
			else
			{
				Console.WriteLine("Ungültige Eingabe. Zurück zum Menü.");
			}

			Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

		// Methode zur Anzeige des Zubehörs
		private void DisplayAccessories()
		{
			Console.Clear();
			Console.WriteLine("Verfügbare Zubehör:");

			int i = 1;
			foreach (var accessory in accessories)
			{
				decimal finalPrice = accessory.Value.price * (1 - accessory.Value.discount);
				Console.WriteLine($"{i}. {accessory.Key} - Originalpreis: {accessory.Value.price:C}, Preis nach Rabatt: {finalPrice:C}\n");
				i++;
			}

			Console.WriteLine("Geben Sie die Nummer des Produkts ein, das Sie kaufen möchten, oder drücken Sie Enter, um zurückzukehren:");
			string input = Console.ReadLine();

			if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= accessories.Count)
			{
				string selectedAccessory = new List<string>(accessories.Keys)[itemIndex - 1];
				cart.Add(selectedAccessory);
				Console.WriteLine($"{selectedAccessory} wurde zum Warenkorb hinzugefügt.");
			}
			else
			{
				Console.WriteLine("Ungültige Eingabe. Zurück zum Menü.");
			}

			Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

		// Methode zur Anzeige des Warenkorbs
		private void ViewCart()
		{
			Console.Clear();
			if (cart.Count == 0)
			{
				Console.WriteLine("Ihr Warenkorb ist leer.");
			}
			else
			{
				Console.WriteLine("Ihr Warenkorb enthält folgende Artikel:");
				decimal totalPrice = 0m;

				int index = 1;
				foreach (var item in cart)
				{
					decimal price = GetItemPrice(item);
					totalPrice += price;
					Console.WriteLine($"{index}. {item}: {price:C}");
					index++;
				}

				// Rabatt gewähren, wenn mehr als 3 Samen im Warenkorb sind
				int seedCount = cart.Count(item => cannabisSeeds.ContainsKey(item));
				if (seedCount > 3)
				{
					totalPrice *= 0.95m; // 5% Rabatt
					Console.WriteLine("\nZusätzlicher Rabatt von 5% für den Kauf von mehr als 3 Samen angewendet!");
				}

				Console.WriteLine($"\nGesamtsumme: {totalPrice:C}");
				Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
			}

			Console.ReadKey();
		}

		// Methode zur Berechnung des Preises eines Artikels
		private decimal GetItemPrice(string item)
		{
			if (cannabisSeeds.ContainsKey(item))
				return cannabisSeeds[item].price * (1 - cannabisSeeds[item].discount);
			else if (accessories.ContainsKey(item))
				return accessories[item].price * (1 - accessories[item].discount);
			return 0m;
		}

		// Methode zum Abschluss der Bestellung
		private void Checkout()
		{
			Console.Clear();
			if (cart.Count == 0)
			{
				Console.WriteLine("Ihr Warenkorb ist leer. Sie können keine Bestellung abschließen.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Wie möchten Sie bezahlen?");
			Console.WriteLine("1. Kreditkarte");
			Console.WriteLine("2. PayPal");
			Console.WriteLine("3. Girokarte");

			string paymentChoice = Console.ReadLine();
			switch (paymentChoice)
			{
				case "1":
					Console.WriteLine("Sie haben Kreditkarte als Zahlungsmethode gewählt.");
					break;
				case "2":
					Console.WriteLine("Sie haben PayPal als Zahlungsmethode gewählt.");
					break;
				case "3":
					Console.WriteLine("Sie haben Girokarte als Zahlungsmethode gewählt.");
					break;
				default:
					Console.WriteLine("Ungültige Zahlungsmethode. Bestellung abgebrochen.");
					return;
			}

			// Gesamtsumme berechnen
			decimal totalPrice = cart.Sum(item => GetItemPrice(item));

			// Rabatt gewähren, wenn mehr als 3 Samen im Warenkorb sind
			int seedCount = cart.Count(item => cannabisSeeds.ContainsKey(item));
			if (seedCount > 3)
			{
				totalPrice *= 0.95m; // 5% Rabatt
				Console.WriteLine("Zusätzlicher Rabatt von 5% angewendet!");
			}

			Console.WriteLine($"Ihre Bestellung wird bearbeitet. Gesamtsumme: {totalPrice:C}");
			cart.Clear(); // Warenkorb leeren nach der Bestellung
			Console.WriteLine("Vielen Dank für Ihre Bestellung!");
			Console.ReadKey();
		}

		// Methode zur Erstellung eines Kundenkontos
		private void CreateCustomerAccount()
		{
			Console.Clear();
			Console.WriteLine("Kundenkonto erstellen");

			Console.Write("Vorname: ");
			string name = Console.ReadLine();
			Console.Write("Nachname: ");
			string lastname = Console.ReadLine();
			Console.Write("E-Mail: ");
			string email = Console.ReadLine();

			// Erstellen eines neuen Kunden mit generierter Kundennummer
			Customer newCustomer = new Customer(name, email, lastname);
			//customers.Add(newCustomer);
			Customer.SaveCustomers(); // Speichere den neuen Kunden in der JSON-Datei

			// Zeige dem Kunden die generierte Kundennummer an
			Console.WriteLine($"Ihr Kundenkonto wurde erfolgreich erstellt.");
			Console.WriteLine($"Ihre Kundennummer lautet: {newCustomer.Kundennummer}");
			Console.WriteLine("Bitte verwenden Sie diese Kundennummer für zukünftige Anmeldungen.");
			Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

		// Methode für den Kunden-Login
		private void Login()
		{
			Console.Clear();
			Console.WriteLine("Bitte melden Sie sich an");

			Console.Write("E-Mail: ");
			string email = Console.ReadLine();
			Console.Write("Kundennummer: ");
			string kundennummer = Console.ReadLine();

			// Prüfen, ob ein Kunde mit der passenden E-Mail und Kundennummer existiert
			var customer = Customer.customers.FirstOrDefault(c => c.Email == email && c.Kundennummer == kundennummer);
			if (customer != null)
			{
				Console.WriteLine($"Willkommen zurück, {customer.Name} {customer.Lastname}!");
			}
			else
			{
				Console.WriteLine("Kunde nicht gefunden oder falsche Kundennummer. Bitte erneut versuchen.");
			}

			Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
			Console.ReadKey();
		}

	}

}
