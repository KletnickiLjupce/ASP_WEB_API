namespace API_HM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using API_HM.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<API_HM.DAL.APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(API_HM.DAL.APIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //var users = new List<User>
            //{
            //    new User{UserName="jdoe", Name="John Doe",UserId=1 , NomenUserRoleId=2},
            //    new User{UserName="janedean", Name="Jane Dean",UserId=2,NomenUserRoleId=2},
            //};
            //users.ForEach(s => context.Users.AddOrUpdate(s));
            //context.SaveChanges();
            var nomenUserRoles = new List<NomenUserRole>
            {
                new NomenUserRole
                {
                    NomenUserRoleId=1,
                    Name="Admin"
                },
                new NomenUserRole
                {
                    NomenUserRoleId=2,
                    Name="Simple User"
                },
            };
            nomenUserRoles.ForEach(s => context.NomenUserRoles.AddOrUpdate(s));
            context.SaveChanges();

            var users = new List<User>
            {
                new User
                {
                    UserId=1,
                    UserName="jdoe",
                    Name = "John Doe",
                    NomenUserRoleId=2,
                },
                 new User
                {
                    UserId=2,
                    UserName="janedean",
                    Name = "Jane Dean",
                    NomenUserRoleId=2,
                },
                  new User
                {
                    UserId=3,
                    UserName="smithb",
                    Name = "Ben Smith",
                    NomenUserRoleId=2,
                },
                   new User
                {
                    UserId=4,
                    UserName="admin",
                    Name = "Admin",
                    NomenUserRoleId=1,
                }

            };
            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();


            var nomenArticleLevels = new List<NomenArticleLevel>
            {
                new NomenArticleLevel
                {
                    NomenArticleLevelId=1,
                    Name="Begginer"
                },
                 new NomenArticleLevel
                {
                    NomenArticleLevelId=2,
                    Name="Intermediate"
                },
                  new NomenArticleLevel
                {
                    NomenArticleLevelId=3,
                    Name="Advanced"
                }
            };
            nomenArticleLevels.ForEach(s => context.nomenArticleLevels.AddOrUpdate(s));
            context.SaveChanges();

            var authors = new List<Author>
            {
                new Author
                {
                    AuthorId=1,
                    Name="John Doe"
                },
                new Author
                {
                    AuthorId=2,
                    Name="Jane Dean"
                },
                new Author
                {
                    AuthorId=3,
                    Name="Ben Smith"
                }

            };
            authors.ForEach(s => context.Authors.AddOrUpdate(s));
            context.SaveChanges();

            var articles = new List<Article>
            {
                new Article
                {
                    Title="Object Oriented Programming -  Fundamentals",
                    Id="ddc82058-9a67-4926-9314-00faff10ec71",
                    PublishedDate= new DateTime(2019, 12, 12),
                    AuthorId=1,
                    NomenArticleLevelId=1
                },
                new Article
                {
                    Title="Web Development",
                    Id="4cfd5786-d2ac-4224-8fc5-8f9f3b055085",
                    PublishedDate=new DateTime(2020, 3, 2),
                    AuthorId=1,
                    NomenArticleLevelId=2
                },
                 new Article
                {
                    Title="Unit Testing",
                    Id="092fe14e-514f-4b3e-894a-8ff499e2f00f",
                    PublishedDate=new DateTime(2020, 1, 21),
                    AuthorId=1,
                    NomenArticleLevelId=1
                },

                new Article
                {
                    Title="Introduction to Machine Learning",
                    Id="a7368aa7-9fd0-4c69-a577-0744a6b170bc",
                    PublishedDate= new DateTime(2019, 11, 21),
                    AuthorId=2,
                    NomenArticleLevelId=2
                },
                new Article
                {
                    Title="Decision Trees",
                    Id="5429f93a-2ddb-4953-be86-60e8514a2ab0",
                    PublishedDate= new DateTime(2020, 3, 7),
                    AuthorId=2,
                    NomenArticleLevelId=3
                }

                ,
                new Article
                {
                    Title="Relational Databases Fundamentals",
                    Id="3c635ddc-d819-45f9-bdbd-113372a4dbcd",
                    PublishedDate= new DateTime(2020, 1, 15),
                    AuthorId=3,
                    NomenArticleLevelId=3
                },
                new Article
                {
                    Title="Relational Databases - Advanced",
                    Id="2bfbb45d-267e-4d7b-b111-2963ab040816",
                    PublishedDate= new DateTime(2020, 3, 2),
                    AuthorId=3,
                    NomenArticleLevelId=3
                },

            };
            articles.ForEach(s => context.Articles.AddOrUpdate(s));
            context.SaveChanges();

        }
    }
}

