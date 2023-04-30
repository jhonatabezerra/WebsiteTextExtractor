# BookGenerator
Get all the chapters of a specific book/LightNovel. 

The idea of this project is to create a script that can pick up chapters from books or light novels according to a link on a page.

Obviously it won't work with all sites and probably the only way to do this is if the site link has something like:
```
https://<DOMAIN>/<BOOK NAME>/chapter-<CHAPTER NUMBER>
```

Currently this project is of type 'Console'. But in the future I will implement something like a Rest API, WebPage or a C# Front-end window.

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
