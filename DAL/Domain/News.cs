﻿namespace DAL.Domain
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// News model
    /// </summary>
    public class News
    {
        #region Properties
        
        /// <summary>
        /// News id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// News Title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// News text
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Byte array of news image
        /// </summary>
        public virtual byte[] Image { get; set; }

        /// <summary>
        /// News added time
        /// </summary>
        public virtual DateTime TimeCreate { get; set; }

        /// <summary>
        /// News author
        /// </summary>
        public virtual Player Author { get; set; }

        /// <summary>
        /// Count viewers
        /// </summary>
        public virtual int ViewersCount { get; set; }

        /// <summary>
        /// Comment collection
        /// </summary>
        public virtual ISet<Comment> Comments { get; set; }

        #endregion
    }
}