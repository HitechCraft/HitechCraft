using System.Linq;
using HitechCraft.Common.Models.Enum;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Repository.Specification;

namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Common.DI;
    using DAL.Domain;
    using Models;

    [Authorize]
    public class MessageController : BaseController
    {
        public MessageController(IContainer container) : base(container)
        {
        }

        public ActionResult Index()
        {
            ViewBag.NewMessagesCount = this.NewMessagesCount;

            return View();
        }
        
        [HttpGet]
        public ActionResult GetInboxMessages()
        {
            var messages = new EntityListQueryHandler<PrivateMessage, PrivateMessageViewModel>(this.Container)
                .Handle(new EntityListQuery<PrivateMessage, PrivateMessageViewModel>()
                {
                    Specification = new PrivateMessageByRecipientSpec(this.Player.Name),
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                });

            return PartialView("_InboxMessages", messages);
        }

        [HttpGet]
        public ActionResult GetSendedMessages()
        {
            var messages = new EntityListQueryHandler<PrivateMessage, PrivateMessageViewModel>(this.Container)
                .Handle(new EntityListQuery<PrivateMessage, PrivateMessageViewModel>()
                {
                    Specification = new PrivateMessageByAuthorSpec(this.Player.Name),
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                });

            return PartialView("_SendedMessages", messages);
        }
        
        [HttpGet]
        public ActionResult GetInboxMessage(int messageId)
        {
            var message = new EntityQueryHandler<PrivateMessage, PrivateMessageViewModel>(this.Container)
                .Handle(new EntityQuery<PrivateMessage, PrivateMessageViewModel>()
                {
                    Id = messageId,
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                });

            //проверка на внедрение в html Id недоступного входяшего сообщения при запросе
            if (!message.Players.Any(x => x.PlayerName == this.Player.Name && x.PlayerType == PMPlayerType.Recipient))
                return this.Content("NO");

            return PartialView("_Message", message);
        }

        public int GetNewMessagesCount()
        {
            return this.NewMessagesCount;
        }
    }
}