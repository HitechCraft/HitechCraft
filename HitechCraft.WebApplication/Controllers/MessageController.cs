using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Models.Enum;
using HitechCraft.Core.Projector;
using HitechCraft.Core.Repository.Specification.PrivateMessage;


namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using BL.CQRS.Query;
    using Models;
    using System;
    using System.Linq;
    using BL.CQRS.Command;

    #endregion

    //TODO beta2.0: отправка сообщений
    //TODO beta2.0: ответ на входящие сообщения
    //TODO beta2.0: операции с отправленными сообщениями
    //TODO beta2.0: удаление сообещний прямо со страницы с подробным текстом

    //TODO release2.1: перессыл сообщения текущему/другому пользователям
    //TODO release2.1: отправка сообщений нескольких пользователям (массовая рассылка)

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
                    Specification = !new PrivateMessageRemovedSpec(this.Player.Name) & new PrivateMessageByRecipientSpec(this.Player.Name),
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
                    Specification = !new PrivateMessageRemovedSpec(this.Player.Name) & new PrivateMessageByAuthorSpec(this.Player.Name),
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                });

            return PartialView("_SendedMessages", messages);
        }
        
        [HttpGet]
        public ActionResult GetInboxMessage(int messageId)
        {
            try
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

                if (
                    message.Players.First(x => x.PlayerName == this.Player.Name && x.PlayerType == PMPlayerType.Recipient)
                        .MessageType == PMType.New)
                {
                    this.CommandExecutor.Execute(new PMInboxReadCommand()
                    {
                        PMId = messageId,
                        PlayerName = this.Player.Name
                    });
                }

                return PartialView("_Message", message);
            }
            catch
            {
                return this.Content("NO");
            }
        }

        [HttpPost]
        public ActionResult DeleteMessage(int messageId)
        {
            try
            {
                this.CommandExecutor.Execute(new PMInboxRemoveCommand()
                {
                    PlayerName = this.Player.Name,
                    PMId = messageId
                });

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content("Ошибка удаления сообщения: " + e.Message);
            }
        }

        public int GetNewMessagesCount()
        {
            return new EntityListQueryHandler<PrivateMessage, PrivateMessageViewModel>(this.Container)
                .Handle(new EntityListQuery<PrivateMessage, PrivateMessageViewModel>()
                {
                    Specification = new RecipientPrivateMessageByTypeSpec(this.Player.Name, PMType.New),
                    Projector = this.Container.Resolve<IProjector<PrivateMessage, PrivateMessageViewModel>>()
                }).Count; ;
        }

        public ActionResult GetMessageOptions(int messageId)
        {
            return PartialView("_MessageOptions", messageId);
        }
    }
}