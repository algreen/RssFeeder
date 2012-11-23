#r "System.Core.dll"
#r "System.Xml.Linq.dll"
open System.Xml.Linq
open System.Data

let xmlLocation = "D:\GitHub\RssFeeder\RssFeeder\UsefulSites.xml"

let xn s = XName.Get(s)

let xd = XDocument.Load(xmlLocation)


let props = xd.Element(xn "parent").Elements(xn "property")

//
//  let property = xd.Descendants(xn "property")
//   .Where(fun (p : XElement) -p.Attribute(xn "name").Value = "Url")
//    .Single()
//    property
//
