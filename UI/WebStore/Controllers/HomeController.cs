using System;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Models;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private ModelBlogPost[] blogPost = new ModelBlogPost[]
        {
            new ModelBlogPost("Розовые футболки для девушек прибыли в магазин", "Mac Doe", "13:33", "ДЕК 5, 2013", "blog-one.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." ),
            new ModelBlogPost("Розовые футболки для девушек прибыли в магазин", "Mac Doe", "13:33", "ДЕК 5, 2013", "blog-two.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." ),
            new ModelBlogPost("Розовые футболки для девушек прибыли в магазин", "Mac Doe", "13:33", "ДЕК 5, 2013", "blog-three.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." ),
        };

        private ModelProductDetal modelProductDetal = new ModelProductDetal();

        ModelBlogSingle modelBlogSingle = new ModelBlogSingle(new BlogPostArea("Девушки розовая футболка прибыла в магазин", "Mac Doe", "13:33", "ДЕК 5, 2013", "blog-one.jpg", new string[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
            "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.",
            "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.",
        }),
            new ModelBlogPost("", "Annie Davis", "13:33", "ДЕК 5, 2013", "man-one.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.  Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."), 
            new ModelBlogPost[]
            {
                new ModelBlogPost("", "Janis Gallagher", "13:33", "ДЕК 5, 2013", "man-two.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.  Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."),
                new ModelBlogPost("", "Janis Gallagher", "13:33", "ДЕК 5, 2013", "man-three.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.  Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."),
                new ModelBlogPost("", "Janis Gallagher", "13:33", "ДЕК 5, 2013", "man-four.jpg", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.  Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."),
            },
            "socials.png",
            new Tag(new []{ "Розовый", "Майка", "Девушкам" }, "(6 голосов)")
        );

        private ModelCart modelCart = new ModelCart(new Item[]
        {
            new Item("Colorblock Scuba", "1089772", "$59", "one.png"),
            new Item("Colorblock Scuba", "1089772", "$59", "two.png"),
            new Item("Colorblock Scuba", "1089772", "$59", "three.png"),
        },
            new string[]
            {
                "Россия",
                "Соединенные Штаты",
                "Бангладеш",
                "Великобритания",
                "Индия",
                "Пакистан",
                "Украина",
                "Канада",
                "Дубай"
            },
            new string[]
            {
                "Москва",
                "Дакка",
                "Лондон",
                "Дили",
                "Лахор",
                "Аляска",
                "Канада",
                "Дубай"
            });

        public IActionResult Index() => View(modelProductDetal);

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult SomeAction() => View();

        public IActionResult Error404() => View();

        public IActionResult Blog() => View(blogPost);

        public IActionResult BlogSingle() => View(modelBlogSingle);

        public IActionResult CheckOut() => View(modelCart);

        public IActionResult ContactUs() => View();

        public IActionResult ErrorStatus(string code)
        {
            return RedirectToAction(nameof(Error404));
        }
    }
}