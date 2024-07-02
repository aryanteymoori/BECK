using _01_BECKQuery.Contract.Article;

namespace _01_BECKQuery.Contract.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
        public bool IsDeleted { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
    }
}
