﻿using Bookish.Data;
using Bookish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bookish.DataServices
{
    /// <summary>
    /// Comment service is basic CRUD for comments
    /// </summary>
    public class CommentService : ICommentService
    {
        private Context context;

        public CommentService(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// CommentModel queryable that is able to determine the total
        /// amount of sub comments by utilizing a subquery
        /// </summary>
        /// <param name="predicate">The where predicate for filtering</param>
        /// <returns>
        /// A CommentModel queryable
        /// </returns>
        public IQueryable<CommentModel> CommentModelQuery(IQueryable<Comment> commentQuery)
        {
            return commentQuery
                .Select(com => new
                {
                    com,
                    children = context.Comments.Where(c => c.Commented_UnderId == com.Id)
                })
                .Select(com => new CommentModel
                {
                    Body = com.com.Body,
                    Commented_At = com.com.Commented_At,
                    Id = com.com.Id,
                    Parent_Id = com.com.Commented_UnderId,
                    Post_Id = com.com.Commented_OnId,
                    Commented_By = com.com.Commented_By.Username,
                    Votes = com.com.Ratings.Where(r => r.IsUpvoted).Count(),
                    TotalComments = com.children.Count()
                });
        }

        /// <summary>
        /// Creates a comment from a comment model
        /// </summary>
        /// <param name="comment">The comment to create</param>
        /// <returns>
        /// The newly created comment model
        /// </returns>
        public CommentModel CreateComment(AuthUserModel authUser, CommentModel comment)
        {
            // TODO: Verify information
            Comment commentDB = new Comment
            {
                Body = comment.Body,
                Commented_ById = authUser.Id,
                Commented_At = DateTime.Now,
                Commented_UnderId = comment.Parent_Id,
                Commented_OnId = comment.Post_Id,
            };

            context.Comments.Add(commentDB);
            context.SaveChanges();

            return this.CommentModelQuery(context.Comments.Where(com => com.Id == commentDB.Id))
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of comments model from a given queryable
        /// </summary>
        /// <param name="commentQuery">The queryable to convert to models</param>
        /// <returns>
        /// A list of comment models
        /// </returns>
        public List<CommentModel> GetCommentModels(IQueryable<Comment> commentQuery)
        {
            return CommentModelQuery(commentQuery)
                .ToList();
        }

        /// <summary>
        /// Gets a list of comments under a given post
        /// </summary>
        /// <param name="postId">The post id</param>
        /// <param name="skip">How many comments to skip</param>
        /// <param name="take">How many comments to take</param>
        /// <returns>
        /// A list of comment models for a given post
        /// </returns>
        public List<CommentModel> GetPostComments(int postId, int skip, int take)
        {
            return this.CommentModelQuery(context.Comments.Where(com => com.Commented_OnId == postId && com.Commented_UnderId == null))
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        /// <summary>
        /// Gets a list of comments under a given comment
        /// </summary>
        /// <param name="commentId">The comment id</param>
        /// <param name="skip">How many comments to skip</param>
        /// <param name="take">How many comments to take</param>
        /// <returns>
        /// A list of comment models for a given comment
        /// </returns>
        public List<CommentModel> GetSubComments(int commentId, int skip, int take)
        {
            return this.CommentModelQuery(context.Comments.Where(com => com.Commented_UnderId == commentId))
                .OrderBy(com => com.Id)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
