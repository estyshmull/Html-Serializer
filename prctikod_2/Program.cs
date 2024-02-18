// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Text;
using prctikod_2;

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

var html = await Load("https://forum.netfree.link/category/1/%D7%94%D7%9B%D7%A8%D7%96%D7%95%D7%AA");
var cleanHtml = new Regex("\\n").Replace(html, "");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

HtmlElement rootElement = new HtmlElement();
HtmlElement currentElement = new HtmlElement();
currentElement.Attributes = new List<string>();
currentElement.Classes = new List<string>();
currentElement.Children = new List<HtmlElement>();

//מחיקת השורה הראשונה כי בכל קבצי
//HTMLה היא אותו דבר ואין צורך בה
htmlLines = htmlLines.Skip(1);

foreach (string s in htmlLines)
{
    var oneLine = s.Split(' ');
    string firstWord = oneLine[0];

    if (firstWord == "html")
    {
        rootElement.Name = "html";
        rootElement.Attributes = new List<string>();
        rootElement.Children = new List<HtmlElement>();
        rootElement.Classes = new List<string>();
        rootElement.Attributes.Add("lang=en");

        var attribute = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(s);

        foreach (var item in attribute)
        {
            //Console.WriteLine(item);
            string cTag = item.ToString();
            string[] objs = cTag.Split('=');

            switch (objs[0])
            {
                case "id":
                    rootElement.Id = objs[1];
                    Console.WriteLine(objs[1]);
                    break;
                case "class":
                    string[] classObjs = objs[1].Split(' ');
                    foreach (var x in classObjs)
                    {
                        rootElement.Classes.Add(x);
                    }
                    break;

                default:
                    rootElement.Attributes.Add(cTag);
                    break;
            }
        }

        currentElement = rootElement;
    }

    else if (firstWord == "/html")
    {
        break;
    }

    else if (firstWord.Length > 0 && firstWord[0] == '/')
        currentElement = currentElement.Parent;

    else
    {
        if (HtmlHelper.Instance.AllTags.Contains(firstWord))
        {
            HtmlElement newElement = new HtmlElement() { Name = firstWord };

            newElement.Attributes = new List<string>();

            currentElement.Children.Add(newElement);

            var attribute = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(s);

            foreach (var item in attribute)
            {
                //Console.WriteLine(item);
                string cTag = item.ToString();
                string[] objs = cTag.Split('=');

                switch (objs[0])
                {
                    case "id":
                        newElement.Id = objs[1];
                        //Console.WriteLine(objs[1]);
                        break;
                    case "class":
                        string[] classObjs = objs[1].Split(' ');
                        if (classObjs.Length > 0)
                            newElement.Classes = new List<string>();
                        foreach (var x in classObjs)
                        {
                            newElement.Classes.Add(x);
                        }
                        break;

                    default:
                        newElement.Attributes.Add(cTag);
                        break;
                }
            }
            newElement.Parent = currentElement;
            if (firstWord[firstWord.Length - 1] != '/' && !HtmlHelper.Instance.SelfClosingTags.Contains(firstWord))
            {
                currentElement = newElement;
                currentElement.Children = new List<HtmlElement>();
            }
        }
        else
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == ' ' && s[i + 1] != ' ')
                    currentElement.InnerHtml+=s[i];
            }
        }

    }
}

var s2 = new Selector();
var results = rootElement.FindElementsUsingSelector(Selector.ToSelector("ul.nav.navbar-nav"));
results.ToList().ForEach(a => Console.WriteLine(a.Name));

