using System.Linq;
using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper.Rule
{
    public class RuleToRuleViewModelMapper : BaseMapper<RulePoint, RulePointViewModel>
    {
        public RuleToRuleViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<RulePoint, RulePointViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Rules, ext => ext.MapFrom(src => src.Rules.Select(x => new RuleViewModel()
                {
                    Id = x.Id,
                    Text = x.Text
                })));
        }
    }
}