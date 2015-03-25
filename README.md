# TwitterCards

Demo .NET web app which shows a Twitter home timeline. 

The frontend project is built with MVC 5 and Web API 2, using Unity for dependency injection and interception. The Core library project contains a set of Twitter retrieval interfaces, and a TweetSharp implementation.

## Technical Detail

### Unity Interception
Twitter rate-limit API access, so a simple memory cache is used to cache Twitter retrieval methods. To avoid method clutter and improve maintainability, Unity is used to intercept all cacheable method calls, returning the cached output where appropriate instead of executing the method. Caching is enabled for a method by decorating it (or its class) with the CacheResult attribute. The caching and attribute implementations are here: https://github.com/tombowers/TwitterCards/tree/master/TwitterCards.Core/Caching. Interception is registered for those interfaces which require it here: https://github.com/tombowers/TwitterCards/blob/master/TwitterCards/App_Start/UnityConfig.cs.

### Presentation
The front end project is a standard MVC 5 web app. There is a Web API controller which is used by the front end to get serialized Tweet data. Tweets are animated as they are displayed using the jQuery Transit plugin. Transit provides cross-browser CSS3 transitions and transforms, falling back to jQuery animate. https://github.com/rstacruz/jquery.transit.

### Frontend App Folder Structure
The MVC app is structured differently to the conventions pushed by most .NET MVC guides. As apps grow, the default structure, where controllers are stored together, can become difficult to maintain. A ViewModels folder also becomes a maintenence nightmare. In TwitterCards, a separate folder is included for every controller, and its ViewModels are included in a subfolder. This groups code for site sections into logical areas. It also keeps ViewModels out of a ViewModel bin, and out of the Views folder. This greatly declutters the Views folder, which contains views only! All CSS, images, and Javascript, are located in a separate Presentation folder.

### TweetSharp
TweetSharp uses Hammock to perform the calls to the Twitter REST API. All access to Twitter throughout the front end is abstracted, so the objects returned are not specific to TweetSharp, but are instead instantiated as custom concrete objects using the TweetSharpExtensions class. I intend to remove TweetSharp, as it's no longer being actively maintained, replacing it with a custom implementation. Using a different library is as simple as providing a new implementation of ITwitterRetriever, and swapping out the dependency in UnityConfig.

### Authentication
The front end project uses the Authorize attribute and a cookie to restrict access to logged in users. The login process involves redirecting the user to Twitter to authorise the app. This follows the OAuth 3-legged authorization process. https://dev.twitter.com/oauth/3-legged.
