﻿using _01_BECKQuery.Contract.Comment;

namespace _01_BECKQuery.Contract.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public double DoubleUnitPrice { get; set; }
        public string UnitPrice { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductPictureQueryModel> ProductPictures { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
    }
}
