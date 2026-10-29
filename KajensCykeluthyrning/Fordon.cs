// Basklass med gemensamma egenskaper och metoder för alla fordon.
// Abstract innebär att vi inte kan skapa ett objekt direkt av Fordon.

namespace KajensCykeluthyrning;

public abstract class Fordon
{

    // Alla fordon har ett nummer, ett timpris och en uthyrningsstatus.
    public int Nummer { get; }
    public decimal Timpris { get; }
    public bool ArUthyrd { get; private set; }

    protected Fordon(int nummer, decimal timpris)
    {
        Nummer = nummer;
        Timpris = timpris;
    }
    // Beräknar priset före medlemsrabatt: timpris gånger antal timmar.
    public decimal BeraknaPris(int timmar)
    {
        return Timpris * timmar;
    }

    public void MarkeraUthyrd()
    {
        ArUthyrd = true;
    }

    public void MarkeraLedig()
    {
        ArUthyrd = false;
    }
}