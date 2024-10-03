using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Repräsentiert ein einzelnes ToDo-Element mit Name, Beschreibung und Priorität.
/// </summary>
public class ToDoItem
{
    private string name;

    // Property mit Validierung
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Der Name darf nicht leer sein.");
            }
            name = value; // weist  dem privaten Attribut name den neuen Wert zu, der durch value übergeben wird
        }
    }

    private string beschreibung; // Das private Feld

    public string Beschreibung // Die Property ermöglicht den Zugriff auf das private Feld beschreibung von außen und prüft, ob der Wert gültig ist, bevor er gespeichert wird
    {
        get { return beschreibung; } // Getter: Gibt den Wert von 'beschreibung' zurück
        set
        {
            // Setter: Überprüft, ob der neue Wert gültig ist
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Die Beschreibung darf nicht leer sein.");
            }
            beschreibung = value; // Weist den gültigen Wert dem Feld 'beschreibung' zu
        }
    }


    private string priorität;
    public string Priorität
    {
        get { return priorität; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                (value != "hoch" && value != "mittel" && value != "niedrig"))
            {
                throw new ArgumentException("Die Priorität muss hoch, mittel oder niedrig sein.");
            }
            priorität = value;
        }
    }

    // Konstruktor zum Initialisieren eines neuen ToDo-Items
    public ToDoItem(string name, string beschreibung, string priorität)
    {
        Name = name;
        Beschreibung = beschreibung;
        Priorität = priorität;
    }

    // Überschreibt die ToString-Methode, um das ToDo-Item in lesbarer Form anzuzeigen
    public override string ToString()
    {
        return $"Name: {Name}, Beschreibung: {Beschreibung}, Priorität: {Priorität}";
    }
}

/// <summary>
/// Verwalten der ToDo-Liste und deren Operationen (Hinzufügen, Anzeigen, Bearbeiten, Löschen).
/// </summary>
public class ToDoManager
{
    // Liste zur Speicherung der ToDo-Items
    private List<ToDoItem> aufgabenListe;

    // Konstruktor zur Initialisierung der ToDo-Liste
    public ToDoManager()
    {
        aufgabenListe = new List<ToDoItem>();
    }

    /// <summary>
    /// Fügt eine neue Aufgabe zur Liste hinzu.
    /// </summary>
    public void AufgabeHinzufuegen()
    {
        string name;
        while (true)
        {
            Console.Write("Geben Sie den Namen der Aufgabe ein: ");
            name = Console.ReadLine();

            try
            {
                // Testet Eingabe direkt im Konstruktor
                ToDoItem testAufgabe = new ToDoItem(name, "Beschreibung", "hoch"); // Temporäres Objekt zur Validierung
                break; // Gültiger Name, Schleife beenden
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }

        string beschreibung;
        while (true)
        {
            Console.Write("Geben Sie die Beschreibung der Aufgabe ein: ");
            beschreibung = Console.ReadLine();

            try
            {
                ToDoItem testAufgabe = new ToDoItem(name, beschreibung, "hoch"); // Temporäres Objekt zur Validierung
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }

        string priorität;
        while (true)
        {
            Console.Write("Geben Sie die Priorität der Aufgabe ein (hoch, mittel, niedrig): ");
            priorität = Console.ReadLine().ToLower();

            try
            {
                ToDoItem testAufgabe = new ToDoItem(name, beschreibung, priorität); // Temporäres Objekt zur Validierung
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }

        // Hier wird das endgültige ToDoItem-Objekt erstellt
        ToDoItem aufgabe = new ToDoItem(name, beschreibung, priorität);
        aufgabenListe.Add(aufgabe);
        Console.WriteLine("Aufgabe wurde hinzugefügt!");
    }



    /// <summary>
    /// Zeigt die Aufgabenliste an, sortiert nach Priorität (hoch, mittel, niedrig).
    /// </summary>
    public void AufgabenAnzeigen()
    {
        if (aufgabenListe.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben vorhanden.");
        }
        else
        {
            // Sortiere die Aufgaben nach Priorität
            var sortierteListe = aufgabenListe.OrderBy(a => a.Priorität).ToList();

            Console.WriteLine("\n--- Aufgabenliste ---");
            for (int i = 0; i < sortierteListe.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Aufgabe:\n {sortierteListe[i]}");
            }
        }
    }

    /// <summary>
    /// Ermöglicht das Bearbeiten einer bestehenden Aufgabe.
    /// </summary>
    public void AufgabeBearbeiten()
    {
        AufgabenAnzeigen();
        Console.Write("Wählen Sie die Nummer der Aufgabe, die Sie bearbeiten möchten: ");

        if (int.TryParse(Console.ReadLine(), out int auswahl) && auswahl > 0 && auswahl <= aufgabenListe.Count)
        {
            ToDoItem aktuelleAufgabe = aufgabenListe[auswahl - 1];

            Console.Write($"Neuer Name der Aufgabe (aktueller Name: {aktuelleAufgabe.Name}): ");
            string name = Console.ReadLine();
            Console.Write($"Neue Beschreibung der Aufgabe (aktuelle Beschreibung: {aktuelleAufgabe.Beschreibung}): ");
            string beschreibung = Console.ReadLine();

            string priorität;
            while (true)
            {
                Console.Write($"Neue Priorität der Aufgabe (aktuelle Priorität: {aktuelleAufgabe.Priorität}): ");
                priorität = Console.ReadLine().ToLower(); // Umwandeln in Kleinbuchstaben

                if (priorität == "hoch" || priorität == "mittel" || priorität == "niedrig" || string.IsNullOrWhiteSpace(priorität))
                {
                    break; // Gültige Eingabe, Schleife beenden
                }
                else
                {
                    Console.WriteLine("Ungültige Priorität. Bitte verwenden Sie 'hoch', 'mittel' oder 'niedrig'.");
                }
            }

            // Aktualisiere die Aufgabe mit neuen Werten oder behalte den alten Wert bei leerer Eingabe
            aktuelleAufgabe.Name = string.IsNullOrWhiteSpace(name) ? aktuelleAufgabe.Name : name; // ternärer Operator
            aktuelleAufgabe.Beschreibung = string.IsNullOrWhiteSpace(beschreibung) ? aktuelleAufgabe.Beschreibung : beschreibung;
            aktuelleAufgabe.Priorität = string.IsNullOrWhiteSpace(priorität) ? aktuelleAufgabe.Priorität : priorität;

            Console.WriteLine("Aufgabe wurde aktualisiert!");
        }
        else
        {
            Console.WriteLine("Ungültige Auswahl.");
        }
    }

    /// <summary>
    /// Löscht eine Aufgabe aus der Liste.
    /// </summary>
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

    /// <summary>
    /// Ermöglicht das Suchen von Aufgaben anhand eines Stichworts in der Beschreibung.
    /// </summary>
    public void AufgabeSuchen()
    {
        Console.Write("Geben Sie ein Stichwort zur Suche ein: ");
        string stichwort = Console.ReadLine()?.ToLower(); // Umwandlung in Kleinbuchstaben

        var gefundeneAufgaben = aufgabenListe
            .Where(a => a.Name.ToLower().Contains(stichwort) || a.Beschreibung.ToLower().Contains(stichwort)) // Suche in Name und Beschreibung
            .ToList();

        if (gefundeneAufgaben.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben mit diesem Stichwort gefunden.");
        }
        else
        {
            Console.WriteLine("Gefundene Aufgaben:");
            foreach (var aufgabe in gefundeneAufgaben)
            {
                Console.WriteLine(aufgabe);
            }
        }
    }



    /// <summary>
    /// Hauptklasse zum Starten des Programms.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ToDoManager manager = new ToDoManager();

            while (true)
            {
                Console.WriteLine("\n--- ToDo-Listen-Verwaltung ---");
                Console.WriteLine("1. Aufgabe hinzufügen");
                Console.WriteLine("2. Aufgaben anzeigen (sortiert nach Priorität)");
                Console.WriteLine("3. Aufgabe bearbeiten");
                Console.WriteLine("4. Aufgabe löschen");
                Console.WriteLine("5. Aufgaben suchen");
                Console.WriteLine("6. Programm beenden");

                string auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        manager.AufgabeHinzufuegen();
                        break;
                    case "2":
                        manager.AufgabenAnzeigen();
                        break;
                    case "3":
                        manager.AufgabeBearbeiten();
                        break;
                    case "4":
                        manager.AufgabeLoeschen();
                        break;
                    case "5":
                        manager.AufgabeSuchen();
                        break;
                    case "6":
                        Console.WriteLine("Programm wird beendet.");
                        return;
                    default:
                        Console.WriteLine("Ungültige Auswahl.");
                        break;
                }
            }
        }
    }
}
