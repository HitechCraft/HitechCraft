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
    }
}