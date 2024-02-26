Thank you for your interest in my project!
In this project, I developed a tool for processing and manipulating HTML. This tool can be used for various purposes, such as:

Developing a Crawler (or Scrapper): A mechanism that crawls websites and analyzes their HTML to extract the desired information.
Analyzing websites: To discover the technologies and libraries they use.
Extracting data from shopping or second-hand websites: To display it on another website.
The tool consists of two parts:

Html Serializer: A service that accesses a web page URL, reads the returned HTML, and converts it to C# objects.
Html Query: An interface that allows you to query the HTML objects produced by the Html Serializer.
The code in this project focuses on developing the Html Serializer.

The main development steps:

Web page retrieval:
Using the HttpClient object to make a web request.
Receiving a string containing all the HTML.
Tag parsing:
Splitting the string into parts according to the HTML tag structure.
Cleaning up empty strings, line breaks, and unnecessary spaces.
Further development:

The code in this project provides a basis for developing our own Crawler. In the future, additional functionality can be added, such as:

Tag attribute parsing.
DOM tree creation.
XPath querying.
I invite you to review the code and the attached documentation.

I would be happy to answer any questions about the project.

Thank you very much
