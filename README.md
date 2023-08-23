# BookGenerator

## Insight💡

One of the things I like to do in my free time is read light novels, however, sometimes I like to read on my cell phone or in a specific application that displays the text the way I want without having to worry about the ads in the browser or page load time. So...
> How can I get a specific text from one or more sites, download them in a text file (.txt), and have the option of getting the same text with the translation already included?
---

## TO BE📝
Get all the chapters of a book or LightNovel on the specific website. 
The idea of this project is to create a script that can pick up chapters from books or light novels according to a link on a page.
Obviously, it won't work with all sites and probably the only way to do this is if the site link has something like this:
```
https://<DOMAIN>/<BOOK NAME>/chapter-<CHAPTER NUMBER>
```
Currently, this project is of type 'Console'. But in the future, I will implement something like a Rest API, WebPage, or a C# Front-end window.

Another resource that I want to implement is the possibility of it generating files with the translation.
If that works, maybe I'll implement a feature to write each paragraph with both versions (original language and translated language).

There is an API available from Google that allows me to do this without having to create a key to access Google services.

```cs
var input = text;
var fromLanguage = "en";
var toLanguage = "pt";
var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(input)}";

var clientWithHeader = await url.AllowAnyHttpStatus().GetAsync();
var content = await clientWithHeader.GetStringAsync();
```

> NOTE: I'm using the `Flurl.Http` and `System.Web` library to make the requests.

---
## Features✔️
- [x] Create the initial structure of how the software will behave to get data from the site;
- [x] Create the resource to download data in text format (.txt) passing the path to where the file will be saved;
- [x] Create a feature to translate the text after it is extracted from the page;
- [ ] Create an interface to facilitate data entry;
