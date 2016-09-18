namespace HitechCraft.BL.CQRS.Command
{
    public class ReferPayCommand
    {
        public string RefererName { get; set; }
        
        public double RubleAmount { get; set; }

        public int GiftPercents { get; set; }
    }
}
