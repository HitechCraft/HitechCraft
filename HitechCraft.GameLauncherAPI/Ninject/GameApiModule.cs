using HitechCraft.Core.Projector;

namespace HitechCraft.GameLauncherAPI.Ninject
{
    using BL.CQRS.Command;
    using Core.Entity;
    using Core.Models.Json;
    using Mapper;
    using Models;
    
    using WebApplication.Mapper;

    using global::Ninject.Modules;

    public class GameApiModule : NinjectModule
    {
        public override void Load()
        {
            #region Projector Ninjects

            Bind(typeof(IProjector<,>)).To(typeof(BaseMapper<,>));

            Bind(typeof(IProjector<PlayerSkin, PlayerSkinModel>)).To(typeof(PlayerSkinToPlayerSkinModelMapper));
            Bind(typeof(IProjector<PlayerSession, PlayerSessionModel>)).To(typeof(PlayerSessionToPlayerSessionModelMapper));
            Bind(typeof(IProjector<PlayerSession, JsonSessionData>)).To(typeof(PlayerSessionToJsonSessionDataMapper));
            Bind(typeof(IProjector<PlayerSessionModel, PlayerSessionUpdateCommand>)).To(typeof(PlayerSessionModelToPlayerSessionUpdateCommandMapper));
            Bind(typeof(IProjector<News, JsonLauncherNews>)).To(typeof(NewsToJsonLauncherNewsMapper));

            #endregion
        }
    }
}