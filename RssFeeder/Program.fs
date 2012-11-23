// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open Feed
open System
open System.Windows.Forms 
open System.Windows.Forms.Design
open System.Drawing 
 
let label = 
  let tempLabel = new Label()
  do tempLabel.Name <- "LabelDescription"
  do tempLabel.Text <- "Als Testing Windows Forms"
  do tempLabel.Location <- new Drawing.Point(26,100)
  tempLabel

let richTextBox =
  let myRichTextBox = new RichTextBox()
  do myRichTextBox.Name <- "RichTextBox1"
  do myRichTextBox.Text <- Feed.displayContent.ToString()
  do myRichTextBox.ClientSize <- new System.Drawing.Size(600,500)
  do myRichTextBox.DetectUrls <- true
  myRichTextBox 

// let webBrowser = 
//   let myWebBrowser = new WebBrowser()
//   do myWebBrowser.Text <- Feed.displayContent.ToString()
//   myWebBrowser
  
let button = 
  let feedButton = new Button()
  do feedButton.Name <- ""
  do feedButton.Text <- "Process Feed"
  do feedButton.ClientSize <- new System.Drawing.Size(15,15)
  feedButton

let form =
  let myForm = new Form()
  do myForm.Name <- "Windows Application Test"
  do myForm.BackColor <- Color.Gray
  do myForm.Text <- "Time is...."
  do myForm.ClientSize <- new System.Drawing.Size(800,600)
  do myForm.Controls.Add(label) 
  do myForm.Controls.Add(richTextBox)
  do myForm.Controls.Add(button)
//  do myForm.Controls.Add(webBrowser)
  myForm

let ticker = 
  let temp = new Timer()
  temp.Enabled <- true
  temp.Interval <- 1000
  temp.Tick.Add(fun _-> label.Text <- DateTime.Now.ToLongTimeString())



do Application.Run(form)
