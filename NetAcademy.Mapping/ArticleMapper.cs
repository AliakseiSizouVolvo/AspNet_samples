using NetAcademy.DataBase.Entities;
using NetAcademy.DTOs;
using NetAcademy.UI.Models;
using Riok.Mapperly.Abstractions;

namespace NetAcademy.UI.Mapper;

[Mapper]
public partial class ArticleMapper
{
    [MapProperty(nameof(Article.Description), 
        nameof(ArticleModel.ShortDescription))]
    public partial ArticleModel ArticleDtoToArticleModel(ArticleDto article);

    [MapProperty([nameof(Article.Source), nameof(Article.Source.Name)], 
        [nameof(ArticleDto.SourceName)])]
    public partial ArticleDto ArticleToArticleDto(Article article);
}