﻿using API.Entities.Enums;
using API.Infra;

namespace API.Entities
{
    public class News : BaseEntity
    {
        public News() { }

        public News(string hat, string title, string text, string author, string img, Status status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;
            PublishDate = DateTime.Now;
            Slug = Helper.GenerateSlug(Title);
            Status = status;

            ValidateEntity();

        }

        public Status ChangeStatus(Status status)
        {
            switch (status)
            {
                case Status.Active:
                    status = Status.Active;
                    break;
                case Status.Inactive:
                    status = Status.Inactive;
                    break;
                case Status.Draft:
                    status = Status.Draft;
                    break;
            }

            return status;
        }

        public string Hat { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Author { get; private set; }
        public string Img { get; private set; }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "Title must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "Hat must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Text, "Text must not be empty");

            AssertionConcern.AssertArgumentLength(Title, 90, "Title must have until 90 characters");
            AssertionConcern.AssertArgumentLength(Hat, 40, "Hat must have until 40 characters");
        }
    }
}
