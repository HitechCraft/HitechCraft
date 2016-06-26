namespace WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using System;
    using System.Linq;
    using Domain;
    using System.Data.Entity;

    #endregion

    public class CommentController : BaseController
    {
        [HttpPost]
        public ContentResult Create(int newsId, string text)
        {
            if (text.Length <= 0) return this.Content("Поле не может быть пустым");

            try
            {
                this.context.Comments.Add(new Comment()
                {
                    Author = this.CurrentUser,
                    News = this.context.News.First(n => n.Id == newsId),
                    Text = text,
                    TimeCreate = DateTime.Now
                });

                this.context.SaveChanges();

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        public ContentResult Edit(int id, string text)
        {
            if (text.Length <= 0) return this.Content("Поле не может быть пустым");

            try
            {
                var comment = this.context.Comments.Include("Author").Include("News").First(c => c.Id == id);

                comment.Text = text;

                this.context.Entry(comment).State = EntityState.Modified;;
                this.context.SaveChanges();

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        [HttpPost]
        public ContentResult Delete(int id)
        {
            try
            {
                var comment = this.context.Comments.Include("Author").Include("News").First(c => c.Id == id);

                this.context.Comments.Remove(comment);
                this.context.SaveChanges();

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }
    }
}