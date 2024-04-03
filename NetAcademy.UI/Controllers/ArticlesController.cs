﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Authorize(Policy = "OnlyFor18+")]
        public async Task<IActionResult> Index()
        {
            var articles = (await _articleService.GetArticlesAsync())
                .Select(article => new ArticleModel()
                {
                    Id = article.Id,
                    Description = article.Description,
                    PublicationDate = article.PublicationDate,
                    SourceLink = article.SourceLink,
                    Title = article.Title,
                    Text = article.Text
                }).ToArray();
            var isAdmin = false;//todo check from claims or from DB 
            return View(new ArticlesIndexViewModel()
            {
                Articles = articles,
                IsAdmin = isAdmin
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AggregateAsync()
        {
            //split our aggregation to 3 steps 
            //rss get some common data to take source url 
            //take article 'body' and copy to our storage 
            // after having an article twxt try to rate it

            var rssLink = @"https://www.pcgamesn.com/mainrss.xml";
            await _articleService.AggregateFromSourceAsync(rssLink);

            return RedirectToAction("Index");
        }
    }
}
