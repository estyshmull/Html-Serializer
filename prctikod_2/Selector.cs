using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prctikod_2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector ToSelector(string s)
        {
            bool ifRoot = false;
            string[] qSelect = s.Split(' ');
            Selector rootSelector = new Selector();
            Selector currentSelector = new Selector();

            rootSelector.Classes = new List<string>();
            currentSelector.Classes = new List<string>();

            foreach (string select in qSelect)
            {
                if (!ifRoot)
                {
                    string[] arrElement = Regex.Split(qSelect[0], @"\s*[#.]\s*", RegexOptions.IgnoreCase).Where(s => !string.IsNullOrEmpty(s)).ToArray();

                    foreach (var item in arrElement)
                    {
                        if (qSelect[0].Contains("#" + item))
                            rootSelector.Id = item;

                        else if (qSelect[0].Contains("." + item))
                            rootSelector.Classes.Add(item);

                        else if (HtmlHelper.Instance.AllTags.Contains(item))
                            rootSelector.TagName = item;
                    }
                    currentSelector = rootSelector;
                    ifRoot = true;
                }
                else
                {
                    Selector sunSelector = new Selector() { Parent = currentSelector };

                    currentSelector.Child = sunSelector;
                    sunSelector.Classes = new List<string>();

                    currentSelector = sunSelector;

                    string[] arrElement = Regex.Split(select, @"\s*[#.]\s*", RegexOptions.IgnoreCase).Where(s => !string.IsNullOrEmpty(s)).ToArray();

                    foreach (var item in arrElement)
                    {
                        if (select.Contains("#" + item))
                            currentSelector.Id = item;

                        else if (select.Contains("." + item))
                            currentSelector.Classes.Add(item);

                        else if (HtmlHelper.Instance.AllTags.Contains(item))
                            currentSelector.TagName = item;
                    }
                    Console.WriteLine("father: " + currentSelector.TagName + " chlidren: " + currentSelector.Parent.TagName);
                }
            }
            return rootSelector;
        }
    }
}
