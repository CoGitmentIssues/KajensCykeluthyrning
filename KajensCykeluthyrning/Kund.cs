namespace KajensCykeluthyrning;

// Beskriver en kund med namn, ålder och medlemskap.
public class Kund
{
    public string Namn { get; }
    public int Alder { get; }
    public bool ArMedlem { get; }

    public Kund(string namn, int alder, bool arMedlem)
    {
        Namn = namn;
        Alder = alder;
        ArMedlem = arMedlem;
    }
}