using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
namespace prctikod_2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance { get { return _instance; } }
        public string[] AllTags { get; }
        public string[] SelfClosingTags { get; }


        private HtmlHelper()
        {
            string allTagsJson = File.ReadAllText("folder/AllTags.json");
            string selfClosingTagsJson = File.ReadAllText("folder/SelfClosingTags.json");
            AllTags = JsonSerializer.Deserialize<string[]>(allTagsJson);
            SelfClosingTags = JsonSerializer.Deserialize<string[]>(selfClosingTagsJson);

        }
    }
}
