namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class IKTransactionToIKTransactionViewModelMapper : BaseMapper<IKTransaction, IKTransactionViewModel>
    {
        public IKTransactionToIKTransactionViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<IKTransaction, IKTransactionViewModel>()
                .ForMember(dst => dst.TransactionId, ext => ext.MapFrom(src => src.TransactionId))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.Time, ext => ext.MapFrom(src => src.Time));
        }
    }
}