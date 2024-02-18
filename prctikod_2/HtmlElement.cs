using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prctikod_2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                // שליפת האלמנט הראשון מהתור
                HtmlElement element = queue.Dequeue();

                // החזרת האלמנט
                yield return element;
                if (element.Children != null)
                {
                    // הוספת כל הבנים של האלמנט לתור
                    foreach (HtmlElement child in element.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement parent = this.Parent;

            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }

        public List<HtmlElement> FindElementsUsingSelector(Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            FindElementsRecursively(this, selector, result);
            return result.ToList();
        }

        private void FindElementsRecursively(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
        {
            if (selector == null)
                return;

            foreach (HtmlElement descendant in element.Descendants())
            {
                if (MatchesSelector(descendant, selector))
                {
                    if (selector.Child == null)
                    {
                        result.Add(descendant);
                    }
                    else
                    {
                        FindElementsRecursively(descendant, selector.Child, result);
                    }
                }
            }
        }

        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            bool matchesClasses = false;
            bool matchesTagName = string.IsNullOrEmpty(selector.TagName) || element.Name == selector.TagName;
            bool matchesId = string.IsNullOrEmpty(selector.Id) || element.Id == selector.Id;
            if (element.Classes != null)
                matchesClasses = selector.Classes.Any(c => element.Classes.Contains(c));

            return matchesTagName && matchesId && matchesClasses;
        }
    }
}
