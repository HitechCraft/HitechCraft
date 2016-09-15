namespace HitechCraft.BL.CQRS.Command
{
    public class CurrencyCreateCommand
    {
        public string PlayerName { get; set; }

        public double Gonts => 100.00;

        public double Rubles => 10.00;
    }
}
