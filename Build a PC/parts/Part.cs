namespace Build_a_PC
{
    public enum RecommendationType
    {
        Basic = 0,
        Ok,
        Best
    }

    public enum PartType
    {
        Case,
        Motherboard,
        CPU,
        Graphic_Card,
        Hard_Drive_1,
        Hard_Drive_2,
        PSU,
        Monitor,
        Keyboard,
        Mouse,
        Accessory,
        Ram
    }

    public class Part
    {
        public static string[] RecommendationTypeName = {"基本", "够用", "最佳"};
        public static string[] PartTypeName = { "机箱", "主板", "中央处理器 CPU", "显卡", "硬盘 1", "硬盘 2", "电源", "显示器", "鼠标", "键盘", "配件", "内存条" };
        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        public string Subtitle => $"{RecommendationTypeName[(int) Recommendation]}：{Name}";

        /// <summary>
        /// Brand of the product
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Recommendation
        /// </summary>
        public RecommendationType Recommendation { get; set; }

        /// <summary>
        /// Type of the part
        /// </summary>
        public PartType PartType { get; set; }

        /// <summary>
        /// Pid of the item
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// Url to search/purchase the item
        /// </summary>
        public string Url => "https://item.jd.com/" + Pid + ".html";

        /// <summary>
        /// Url to search/purchase the item
        /// </summary>
        public string UrlAddToBasket => "https://cart.jd.com/gate.action?pid=" + Pid + "&pcount=1&ptype=1";

        /// <summary>
        /// Price of the product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Image url
        /// </summary>
        public string ImageUrl { get; set; }

        //public async Task<double> GetRealTimePriceAsync()
        //{
        //    var web = new HtmlWeb();
        //    var doc2 = web.LoadFromBrowser(Url, o =>
        //    {
        //        // WAIT until the dynamic text is set
        //        return !o.Contains($"price J-p-{Pid}");
        //    });

        //    Console.WriteLine($"New price loaded for {Name}, {doc2.DocumentNode.SelectSingleNode($"//span[@class='price J-p-{Pid}']").InnerHtml}.");
        //    var docTask = web.LoadFromWebAsync(Url);
        //    await docTask;
        //    if (!docTask.IsCompleted) return 0;

        //    var doc = docTask.Result;
        //    var node = doc.DocumentNode.SelectSingleNode($"//span[@class='price J-p-{Pid}']");
        //    Console.WriteLine($"New price loaded for {Name}, {node.InnerHtml}.");
        //    return double.Parse(node.InnerHtml);
        //}
    }
}
