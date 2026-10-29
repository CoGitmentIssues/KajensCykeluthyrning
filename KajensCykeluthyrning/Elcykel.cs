namespace KajensCykeluthyrning;

// Elcykel ärver från Fordon och lägger till batterinivå och laddning.
public class Elcykel : Fordon
{
    public int Batteriniva { get; private set; }

    public Elcykel(int nummer, int batteriniva) : base(nummer, 70m)
    {
        if (batteriniva < 0 || batteriniva > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(batteriniva),
                "Batterinivån måste vara mellan 0 och 100.");
        }

        Batteriniva = batteriniva;
    }
    // Laddar batteriet till 100 procent.
    public void Ladda()
    {
        Batteriniva = 100;
    }
    // Minskar batteriet med 10 procentenheter per timme, som lägst till 0.
    public void ForbrukaBatteri(int timmar)
    {
        // Batteriet kan aldrig bli lägre än 0 procent.
        if (timmar > Batteriniva / 10)
        {
            Batteriniva = 0;
        }
        else
        {
            Batteriniva -= timmar * 10;
        }
    }
}