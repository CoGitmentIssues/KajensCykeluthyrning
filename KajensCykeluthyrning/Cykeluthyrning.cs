using System;
using System.Collections.Generic;
using System.Linq;

namespace KajensCykeluthyrning;

// Hanterar fordon, kunder och pågående uthyrningar samt uthyrningsreglerna.
public class Cykeluthyrning
{
    // Listan kan innehålla både Cykel och Elcykel eftersom båda ärver från Fordon.
    private List<Fordon> fordon = new List<Fordon>();
    private List<Kund> kunder = new List<Kund>();
    private List<Uthyrning> uthyrningar = new List<Uthyrning>();

    public Cykeluthyrning()

    // Lägger in två cyklar och två elcyklar med unika nummer vid starten.
    {
        fordon.Add(new Cykel(1));
        fordon.Add(new Cykel(2));
        fordon.Add(new Elcykel(3, 80));
        fordon.Add(new Elcykel(4, 25));
    }
    // Lägger in två cyklar och två elcyklar med unika nummer vid starten.
    public void VisaFordon()
    {
        Console.WriteLine("\nNr  Typ       Status    Batteri");

        foreach (Fordon f in fordon)
        {
            string status = "ledig";

            if (f.ArUthyrd)
            {
                status = "uthyrd";
            }

            if (f is Elcykel elcykel)
            {
                Console.WriteLine(
                    $"{f.Nummer,-4}{"Elcykel",-10}{status,-10}" +
                    $"{elcykel.Batteriniva} %");
            }
            else
            {
                Console.WriteLine(
                    $"{f.Nummer,-4}{"Cykel",-10}{status,-10}");
            }
        }
    }

    public void RegistreraKund(string namn, int alder, bool arMedlem)
    {
        namn = namn.Trim();

        if (string.IsNullOrWhiteSpace(namn) || alder < 0)
        {
            Console.WriteLine("Ange ett namn och en giltig ålder.");
            return;
        }

        Kund? befintligKund = HittaKund(namn);

        if (befintligKund != null)
        {
            Console.WriteLine("Det finns redan en kund med det namnet.");
            return;
        }

        Kund kund = new Kund(namn, alder, arMedlem);
        kunder.Add(kund);

        Console.WriteLine($"{kund.Namn} är registrerad.");
    }

    private Kund? HittaKund(string namn)
    {
        return kunder.FirstOrDefault(k =>
            k.Namn.Equals(namn.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private Fordon? HittaFordon(int nummer)
    {
        return fordon.FirstOrDefault(f => f.Nummer == nummer);
    }

    // Kontrollerar kund, fordon, timmar, uthyrningsstatus, ålder och batteri.
    // Om alla kontroller godkänns sparas uthyrningen och fordonet markeras uthyrt.
    public void HyrUt(string kundnamn, int fordonsnummer, int timmar)
    {
        Kund? kund = HittaKund(kundnamn);
        Fordon? valtFordon = HittaFordon(fordonsnummer);

        if (kund == null)
        {
            Console.WriteLine("Kunden finns inte. Registrera kunden först.");
            return;
        }

        if (valtFordon == null)
        {
            Console.WriteLine("Fordonet finns inte.");
            return;
        }

        if (timmar <= 0)
        {
            Console.WriteLine("Antalet timmar måste vara större än 0.");
            return;
        }

        if (valtFordon.ArUthyrd)
        {
            Console.WriteLine("Fordonet är redan uthyrt.");
            return;
        }

        int minimiAlder = 15;

        if (valtFordon is Elcykel)
        {
            minimiAlder = 18;
        }

        if (kund.Alder < minimiAlder)
        {
            Console.WriteLine(
                $"Kunden måste vara minst {minimiAlder} år.");
            return;
        }

        if (valtFordon is Elcykel elcykel && elcykel.Batteriniva < 30)
        {
            Console.WriteLine(
                $"Elcykel {elcykel.Nummer} har för lite batteri " +
                $"({elcykel.Batteriniva} %). Ladda den först.");
            return;
        }

        Uthyrning uthyrning = new Uthyrning(kund, valtFordon, timmar);
        uthyrningar.Add(uthyrning);
        valtFordon.MarkeraUthyrd();

        Console.WriteLine(
            $"{kund.Namn} hyr {valtFordon.GetType().Name} " +
            $"{valtFordon.Nummer} i {timmar} timmar.");
    }
    // Uppdaterar batteri och status, visar kvitto och tar bort pågående uthyrning.
    public void LamnaTillbaka(int fordonsnummer)
    {
        Uthyrning? uthyrning = uthyrningar.FirstOrDefault(u =>
            u.Fordon.Nummer == fordonsnummer);

        if (uthyrning == null)
        {
            Console.WriteLine("Ingen pågående uthyrning hittades för numret.");
            return;
        }

        if (uthyrning.Fordon is Elcykel elcykel)
        {
            elcykel.ForbrukaBatteri(uthyrning.AntalTimmar);
        }

        uthyrning.Fordon.MarkeraLedig();

        Console.WriteLine("\nKVITTO");
        Console.WriteLine($"Kund: {uthyrning.Kund.Namn}");

        Console.WriteLine(
            $"{uthyrning.Fordon.GetType().Name} " +
            $"{uthyrning.Fordon.Nummer}, " +
            $"{uthyrning.AntalTimmar} timmar " +
            $"à {uthyrning.Fordon.Timpris:F2} kr");

        Console.WriteLine(
            $"Grundpris: {uthyrning.BeraknaGrundpris():F2} kr");

        if (uthyrning.Kund.ArMedlem)
        {
            Console.WriteLine(
                $"Medlemsrabatt (10 %): " +
                $"-{uthyrning.BeraknaRabatt():F2} kr");
        }

        Console.WriteLine(
            $"Att betala: {uthyrning.BeraknaTotalpris():F2} kr");

        uthyrningar.Remove(uthyrning);
    }

    public void Ladda(int fordonsnummer)
    {
        Fordon? valtFordon = HittaFordon(fordonsnummer);

        if (valtFordon == null)
        {
            Console.WriteLine("Fordonet finns inte.");
            return;
        }

        if (valtFordon.ArUthyrd)
        {
            Console.WriteLine("Fordonet måste lämnas tillbaka före laddning.");
            return;
        }

        if (valtFordon is Elcykel elcykel)
        {
            elcykel.Ladda();
            Console.WriteLine(
                $"Elcykel {elcykel.Nummer} är laddad till 100 %.");
        }
        else
        {
            Console.WriteLine("En vanlig cykel har inget batteri att ladda.");
        }
    }
}