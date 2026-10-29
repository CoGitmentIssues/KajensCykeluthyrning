namespace KajensCykeluthyrning;

// Cykel ärver nummer, timpris, uthyrningsstatus och metoder från Fordon.
public class Cykel : Fordon
{
    // Skickar cykelns nummer och timpriset 40 kr till basklassens konstruktor.
    public Cykel(int nummer) : base(nummer, 40m)
    {
    }
}