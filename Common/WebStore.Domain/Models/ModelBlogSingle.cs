namespace WebStore.Domain.Models
{
    public class ModelBlogSingle
    {
        public BlogPostArea Blog_PostArea { get; set; }
        public ModelBlogPost[] Comments { get; set; }

        public string PicSocialshare { get; set; }
        public ModelBlogPost FirstCommit { get; set; }

        public Tag TagItem { get; set; }

        public ModelBlogSingle() { }

        public ModelBlogSingle(BlogPostArea blog, ModelBlogPost comment, ModelBlogPost[] comments, string picSocialshare, Tag tag)
        {
            Blog_PostArea = blog;
            Comments = comments;
            PicSocialshare = picSocialshare;
            FirstCommit = comment;
            TagItem = tag;
        }
    }
}
