namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using Common.DI;
    using System;
    using System.Web.Mvc;
    using HitechCraft.BL.CQRS.Command;

    #endregion

    public class CommentController : BaseController
    {
        public CommentController(IContainer container) : base(container)
        {
        }

        [HttpPost]
        [Authorize]
        public ContentResult Create(int newsId, string text)
        {
            if (text.Length <= 0) return this.Content("Поле не может быть пустым");

            try
            {
                this.CommandExecutor.Execute(new CommentCreateCommand()
                {
                    NewsId = newsId,
                    Text = text,
                    AuthorName = this.Player.Name
                });
                
                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        [Authorize]
        public ContentResult Edit(int id, string text)
        {
            if (text.Length <= 0) return this.Content("Поле не может быть пустым");

            try
            {
                this.CommandExecutor.Execute(new CommentUpdateCommand()
                {
                    Id = id,
                    Text = text
                });

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public ContentResult Delete(int id)
        {
            try
            {
                this.CommandExecutor.Execute(new CommentRemoveCommand()
                {
                    Id = id
                });

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }
    }
}