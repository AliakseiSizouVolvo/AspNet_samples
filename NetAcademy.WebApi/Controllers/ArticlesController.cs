using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAcademy.DTOs;
using NetAcademy.WebApi.RequestModels;

namespace NetAcademy.WebApi.Controllers
{
    /// <summary>
    /// Controller to work with articles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private static List<ArticleDto> _articles = new List<ArticleDto>()
        {
            new ArticleDto()
            {
                Id = Guid.NewGuid(),
                Title = "Sample Article",
                Description = "This is a sample article",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                PublicationDate = DateTime.Now,
                SourceLink = "https://www.example.com",
                SourceName = "Example News"
            },
            new ArticleDto()
            {
                Id = Guid.NewGuid(),
                Title = "Another Article",
                Description = "This is another article",
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                PublicationDate = DateTime.Now,
                SourceLink = "https://www.example.com",
                SourceName = "Another News"
            }
        };

        /// <summary>
        /// Get Article By Id
        /// </summary>
        /// <param name="id">Unique Article identifier</param>
        /// <returns>article with specified Id or not found</returns>
        /// <response code="200">Return an article</response>
        /// <response code="404">No article with specified id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        /// <summary>
        /// Get Articles
        /// </summary>
        /// <returns>articles or not found</returns>
        /// <response code="200">Return an articles</response>
        /// <response code="404">No articles</response>
        [HttpGet]
        [ProducesResponseType(typeof(ArticleDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult Get(string? name, string? sourceName)
        {
            var articles = _articles;
            
            
            if (!string.IsNullOrWhiteSpace(name))
            {
                articles = articles.Where(dto => dto.Title.StartsWith(name)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(sourceName))
            {
                articles = articles.Where(dto => dto.SourceName.Equals(sourceName)).ToList();
            }
            return Ok(articles.ToArray());
        }

        /// <summary>
        /// Create a new article
        /// </summary>
        /// <param name="dto">article</param>
        /// <returns></returns>
        /// <response code="201">Return an 201 with link to created article</response>
        /// <response code="400">Incorrect request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ArticleDto dto)
        {
            dto.Id = Guid.NewGuid();
            _articles.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Patch an article. Signature can be used as  generic implementation (better to adapt and use 'correct' version for your app)
        /// </summary>
        /// <param name="id">Article id</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404"></response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Patch(Guid id, PatchRequest request)
        {

            var articleToUpdate = _articles.FirstOrDefault(articleDto
                => articleDto.Id.Equals(id));

            if (articleToUpdate != null)
            {
                foreach (var patchItem in request.PatchItems)
                {
                    var property = typeof(ArticleDto).GetProperty(patchItem.PropertyName);
                    if (property != null)
                    {
                        property.SetValue(articleToUpdate, patchItem.NewValue.ToString());
                    }
                }

                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Put an article
        /// </summary>
        /// <param name="id">Article id</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404"></response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Put(Guid id, ArticleDto dto)
        {

            var articleToUpdate = _articles.FirstOrDefault(articleDto
                => articleDto.Id.Equals(id));

            if (articleToUpdate != null)
            {
                _articles.Remove(articleToUpdate);
                //dto.Id = id;
                _articles.Add(dto);
                return NoContent();
            }
            return NotFound();
        }


        /// <summary>
        /// Remove an article
        /// </summary>
        /// <param name="id">Article id</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404"></response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(Guid id)
        {
            if (_articles.Any(dto => dto.Id.Equals(id)))
            {
                _articles.RemoveAll(a => a.Id == id);
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("Get_1")]
        public IActionResult GetBy1()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("Get_2")]
        public IActionResult GetBy2()
        {
            return Ok();
        }
    }
}
