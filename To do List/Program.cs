using System;
using System.Collections.Generic;

class Programm
{
    private List<Dictionary<string, string>> aufgabenListe;

    public Programm()
    {
        aufgabenListe = new List<Dictionary<string, string>>();
    }

    public void AufgabeHinzufuegen()
    {
        Console.Write("Geben Sie den Namen der Aufgabe ein: ");
        string name = Console.ReadLine();
        Console.Write("Geben Sie die Beschreibung der Aufgabe ein: ");
        string beschreibung = Console.ReadLine();

        Dictionary<string, string> aufgabe = new Dictionary<string, string>();
        aufgabe.Add("name", name);
        aufgabe.Add("beschreibung", beschreibung);

        aufgabenListe.Add(aufgabe);
        Console.WriteLine("Aufgabe wurde hinzugefügt!");
    }

    public void AufgabenAnzeigen()
    {
        if (aufgabenListe.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben vorhanden.");
        }
        else
        {
            for (int i = 0; i < aufgabenListe.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {aufgabenListe[i]["name"]} - {aufgabenListe[i]["beschreibung"]}");
            }
        }
    }

    public void AufgabeBearbeiten()
    {
        AufgabenAnzeigen();
        Console.Write("Wählen Sie die Nummer der Aufgabe, die Sie bearbeiten möchten: ");

        if (int.TryParse(Console.ReadLine(), out int auswahl) && auswahl > 0 && auswahl <= aufgabenListe.Count)
        {
            // Die aktuelle Aufgabe speichern
            var aktuelleAufgabe = aufgabenListe[auswahl - 1];

            // Name bearbeiten
            Console.Write($"Neuer Name der Aufgabe (aktueller Name: {aktuelleAufgabe["name"]}): ");
            string name = Console.ReadLine();

            // Wenn der Name leer bleibt, behalten wir den alten Wert
            if (string.IsNullOrWhiteSpace(name))
            {
                name = aktuelleAufgabe["name"];
            }

            // Beschreibung bearbeiten
            Console.Write($"Neue Beschreibung der Aufgabe (aktuelle Beschreibung: {aktuelleAufgabe["beschreibung"]}): ");
            string beschreibung = Console.ReadLine();

            // Wenn die Beschreibung leer bleibt, behalten wir den alten Wert
            if (string.IsNullOrWhiteSpace(beschreibung))
            {
                beschreibung = aktuelleAufgabe["beschreibung"];
            }

            // Aufgabe aktualisieren
            aufgabenListe[auswahl - 1]["name"] = name;
            aufgabenListe[auswahl - 1]["beschreibung"] = beschreibung;

            Console.WriteLine("Aufgabe wurde aktualisiert!");
        }
        else
        {
            Console.WriteLine("Ungültige Auswahl.");
        }
    }


    public void AufgabeLoeschen()
    {
        AufgabenAnzeigen();
        Console.Write("Wählen Sie die Nummer der Aufgabe, die Sie löschen möchten: ");
        if (int.TryParse(Console.ReadLine(), out int auswahl) && auswahl > 0 && auswahl <= aufgabenListe.Count)
        {
            aufgabenListe.RemoveAt(auswahl - 1);
            Console.WriteLine("Aufgabe wurde gelöscht!");
        }
        else
        {
            Console.WriteLine("Ungültige Auswahl.");
        }
    }

    public void Hauptmenue()
    {
        while (true)
        {
            Console.WriteLine("--- Willkommen bei der To do Liste ---");
            Console.WriteLine("\nHauptmenü:");
            Console.WriteLine("1. Aufgabe hinzufügen");
            Console.WriteLine("2. Aufgabenliste anzeigen");
            Console.WriteLine("3. Aufgabe bearbeiten");
            Console.WriteLine("4. Aufgabe löschen");
            Console.WriteLine("5. Programm beenden");

            string auswahl = Console.ReadLine();

            switch (auswahl)
            {
                case "1":
                    AufgabeHinzufuegen();
                    break;
                case "2":
                    AufgabenAnzeigen();
                    break;
                case "3":
                    AufgabeBearbeiten();
                    break;
                case "4":
                    AufgabeLoeschen();
                    break;
                case "5":
                    Console.WriteLine("Programm beendet.");
                    return;
                default:
                    Console.WriteLine("Ungültige Auswahl, bitte erneut versuchen.");
                    break;
            }
        }
    }

    static void Main(string[] args)
    {
        Programm programm = new Programm();
        programm.Hauptmenue();
    }
}
