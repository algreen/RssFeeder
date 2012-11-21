// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open Feed
open System
open System.Windows.Forms 
open System.Drawing 
 
//[<EntryPoint>]
//let main argv = 
 
let form=new Form()
form.Text<- "My Window Application"
form.BackColor<- Color.Gray  


let richTextBox = new RichTextBox()
//richTextBox.Text = Feed.displayContent
let label = new Label()
label.BackColor = Color.White
label.Text = "Als Testing Windows form"
label.Location = new Point(30,30)

 
Application.Run(form)

//    printfn "%A" argv
//    Feed.sites   =   ["http://techcrunch.com/startups/";
//                "http://techcrunch.com/"]
 //   0 // return an integer exit code
