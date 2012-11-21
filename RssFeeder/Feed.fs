module Feed

open System.Net
open System
open System.IO

let readFeed callback url =
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    callback reader url

let myCallback (reader:IO.StreamReader) url = 
    let html = reader.ReadToEnd()
    html // return the html

        // build a function with the callback "baked in"
let fetchUrl1 = readFeed myCallback

// all the sites i want to get the rss feed
let sites   =   ["http://techcrunch.com/startups/";
                "http://techcrunch.com/"]

// display the content
let displayContent  =   sites |>    List.map fetchUrl1


