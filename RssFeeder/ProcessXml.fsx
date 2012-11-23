#I @"C:\Program Files (x86)\FSharpPowerPack-2.0.0.0\bin\FSharp.PowerPack.dll"
#r "FSharp.Powerpack.dll"

#I @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.XML.dll"

open System
open System.Xml
open Microsoft.FSharp.Collections

let wiki = "D:\GitHub\RssFeeder\RssFeeder\UsefulSites.xml"

type name = string
type attributes = (string * string) list 
type LazyXml =
    | Element of (name * attributes * LazyList<LazyXml>)
    | Text of string


let readLazyXml (xmlUri : string) : LazyXml = 
    let readAttributes (reader : XmlReader) = 
        if reader.HasAttributes then
            [ while reader.MoveToNextAttribute() do yield (reader.Name, reader.Value) ]
        else []
    let rec read (reader : XmlReader) = 
        seq {
            if reader.Read() then
                match reader.NodeType with
                | XmlNodeType.Element ->
                    let reader' = (reader.ReadSubtree() |> (fun reader' -> reader'.Read() |> ignore; reader'))
                    yield Element (reader.Name, readAttributes reader, reader' |> read |> LazyList.ofSeq)
                    reader'.Close(); reader.Skip() // close nested reader, move forward current reader 
                    // continue
                    yield! read reader
                | XmlNodeType.EndElement ->
                    ()
                | XmlNodeType.Whitespace ->
                    yield! read reader
                | XmlNodeType.Text ->
                    yield Text reader.Value
                    yield! read reader
                | _ -> failwithf "Not supported XmlNodeType:" // %s" <| reader.NodeType.ToString()
            else
                ()
        }
    XmlReader.Create(xmlUri) |> read |> LazyList.ofSeq |> LazyList.head

printfn "%A" <| readLazyXml wiki 

//Element
//  ("mediawiki",
//   [("xmlns", "http://www.mediawiki.org/xml/export-0.6/");
//    ("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
//    ("xsi:schemaLocation",
//     "http://www.mediawiki.org/xml/export-0.6/ http://www.mediawiki.org/xml/expo
//rt-0.6.xsd");
//    ("version", "0.6"); ("xml:lang", "en")],
//   seq
//     [Element
//        ("siteinfo", [],
//         seq
//           [Element ("sitename", [], seq [Text "Wikipedia"]);
//            Element
//              ("base", [], seq [Text "http://en.wikipedia.org/wiki/Main_Page"]);
//
//            Element ("generator", [], seq [Text "MediaWiki 1.19wmf1"]);
//            Element ("case", [], seq [Text "first-letter"]); ...]);
//      Element
//        ("page", [],
//         seq
//           [Element ("title", [], seq [Text "AccessibleComputing"]);
//            Element ("ns", [], seq [Text "0"]);
//            Element ("id", [], seq [Text "10"]);
//            Element ("redirect", [("title", "Computer accessibility")], seq []);
//
//            ...]);
//      Element
//        ("page", [],
//         seq
//           [Element ("title", [], seq [Text "Anarchism"]);
//            Element ("ns", [], seq [Text "0"]);
//            Element ("id", [], seq [Text "12"]); Element ("sha1", [], seq []);
//            ...]);
//      Element
//        ("page", [],
//         seq
//           [Element ("title", [], seq [Text "AfghanistanHistory"]);
//            Element ("ns", [], seq [Text "0"]);
//            Element ("id", [], seq [Text "13"]);
//            Element ("redirect", [("title", "History of Afghanistan")], seq []);
//
//            ...]); ...])

open System
open System.Net
open System.IO
open System.Xml

let ReadFile filename = 
    use reader = new StreamReader(path=filename)
    reader.ReadToEnd()

let ReadXml filepath = 
    let file = ReadFile filepath
    file

 //----------------------//
 // Main Functions       //
 //----------------------//

let fetchXml callback url =
  let req     =   WebRequest.Create(Uri(url))
  use resp    =   req.GetResponse()
  use stream  =   resp.GetResponseStream()
  use reader  =   new IO.StreamReader(stream) 
  callback reader url

let xmlCallback (reader:IO.StreamReader) url =
  let html = reader.ReadToEnd()
  html

let xmlLocation = "D:\GitHub\RssFeeder\RssFeeder\UsefulSites.xml"

// These next 2 are really the same, so the bottom one hasnt got a lot of use
let test = ReadFile xmlLocation
let fetchXmlResult = fetchXml xmlCallback xmlLocation

let xmlTextReader = new XmlTextReader(xmlLocation)
let reader = XmlReader.Create(new StringReader(xmlLocation))


type Result(value:XmlNode) = 
    member this.Value = value.InnerText

let (|Node|Result|) (node : #System.Xml.XmlNode) = 
    if node.Name = "Url" then
        Result (new Result(node))
    else
        Node (seq {for x in node.ChildNodes -> x})

let extract node =
    let rec extrat node = 
        match node with
        | Result(p) ->
            Seq.singleton p
        | Node(nodes) ->
            Seq.collect (fun (n) -> extract n) nodes
    extract node

let xmlDoc = new XmlDocument()
xmlDoc.LoadXml(xmlString)


