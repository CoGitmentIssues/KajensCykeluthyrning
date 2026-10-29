namespace KajensCykeluthyrning;

// Kopplar ihop en kund, ett fordon och antalet hyrda timmar.
// Används för att beräkna pris och eventuell medlemsrabatt.
public class Uthyrning
{
    public Kund Kund { get; }
    public Fordon Fordon { get; }
    public int AntalTimmar { get; }

    public Uthyrning(Kund kund, Fordon fordon, int antalTimmar)
    {
        Kund = kund;
        Fordon = fordon;
        AntalTimmar = antalTimmar;
    }

    public decimal BeraknaGrundpris()
    {
        return Fordon.BeraknaPris(AntalTimmar);
    }

    public decimal BeraknaRabatt()
    {
        if (Kund.ArMedlem)
        {
            return BeraknaGrundpris() * 0.10m;
        }

        return 0m;
    }

    public decimal BeraknaTotalpris()
    {
        return BeraknaGrundpris() - BeraknaRabatt();
    }
}