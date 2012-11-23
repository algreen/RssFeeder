module Feed

open System.Net
open System
open System.IO

type RssItem = 
    {
        mutable Title           :   String option
        mutable Link            :   String option
        mutable Description     :   String option 
    }
    static member Empty =
        { Title=None; Description=None; Link=None }
let item = RssItem.Empty
let MatchXmlElement (name,value) = 
    match name with
    | "title"       -> item.Title <- Some(value)
    | "link"        -> item.Link  <- Some(value)
    | "description" -> item.Description <- Some(value)
    | _ -> () 


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


