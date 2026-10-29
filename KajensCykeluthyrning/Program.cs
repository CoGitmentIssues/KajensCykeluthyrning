using System;
using System.Globalization;

namespace KajensCykeluthyrning;

public class Program
{
    // Programmets startpunkt. Visar menyn och hanterar användarens inmatning.
    public static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("sv-SE");

        Cykeluthyrning uthyrning = new Cykeluthyrning();
        bool korProgrammet = true;


        // Upprepar menyn tills användaren väljer att avsluta.
        while (korProgrammet)
        {
            Console.WriteLine("\n=== Kajens Cykeluthyrning ===");
            Console.WriteLine("1. Visa fordon");
            Console.WriteLine("2. Registrera kund");
            Console.WriteLine("3. Hyr ut");
            Console.WriteLine("4. Lämna tillbaka");
            Console.WriteLine("5. Ladda");
            Console.WriteLine("6. Avsluta");
            Console.Write("Val: ");

            string val = Console.ReadLine() ?? "";

            // Kör det menyalternativ som matchar användarens val.
            switch (val)
            {

                case "1":
                    {
                        uthyrning.VisaFordon();
                        break;
                    }

                case "2":
                    {
                        string namn = LasText("Namn: ");
                        int alder = LasHeltal("Ålder: ", 0);
                        bool arMedlem = LasMedlemskap();

                        uthyrning.RegistreraKund(namn, alder, arMedlem);
                        break;
                    }

                case "3":
                    {
                        string kundnamn = LasText("Kundens namn: ");
                        int nummer = LasHeltal("Fordonsnummer: ", 1);
                        int timmar = LasHeltal("Antal timmar: ", 1);

                        uthyrning.HyrUt(kundnamn, nummer, timmar);
                        break;
                    }

                case "4":
                    {
                        int nummer = LasHeltal("Fordonsnummer: ", 1);
                        uthyrning.LamnaTillbaka(nummer);
                        break;
                    }

                case "5":
                    {
                        int nummer = LasHeltal("Fordonsnummer: ", 1);
                        uthyrning.Ladda(nummer);
                        break;
                    }

                case "6":
                    {
                        korProgrammet = false;
                        Console.WriteLine("Programmet avslutas.");
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Ogiltigt val. Välj 1–6.");
                        break;
                    }
            }
        }
    }

    // Läser text och frågar igen om svaret är tomt eller bara blanksteg.
    private static string LasText(string meddelande)
    {
        while (true)
        {
            Console.Write(meddelande);
            string text = (Console.ReadLine() ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            Console.WriteLine("Du behöver skriva något.");
        }
    }

    // Läser ett heltal som måste vara minst det angivna minimivärdet.
    private static int LasHeltal(string meddelande, int minstaVarde)
    {
        while (true)
        {
            Console.Write(meddelande);
            string inmatning = Console.ReadLine() ?? "";

            if (int.TryParse(inmatning, out int tal) && tal >= minstaVarde)
            {
                return tal;
            }

            Console.WriteLine(
                $"Ange ett heltal som är minst {minstaVarde}.");
        }
    }

    // Omvandlar j till true och n till false. Frågar igen vid ogiltigt svar.
    private static bool LasMedlemskap()
    {
        while (true)
        {
            Console.Write("Medlem (j/n): ");
            string svar = (Console.ReadLine() ?? "").Trim().ToLower();

            if (svar == "j")
            {
                return true;
            }

            if (svar == "n")
            {
                return false;
            }

            Console.WriteLine("Svara med j eller n.");
        }
    }
}