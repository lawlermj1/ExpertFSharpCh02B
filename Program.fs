// Learn more about F# at http://fsharp.org

open System.IO
open System.Net
// open FSharp.Core.String 

// Get the contents of the URL via a web request 
let http (url: string) =
    let req = WebRequest.Create(url) 
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

// http "http://news.bbc.co.uk" 

// dotnet run "http://news.bbc.co.uk" "https://en.wikipedia.org/wiki/The_world%27s_100_most_threatened_species" "x"
// -> unhandled exception 

[<EntryPoint>]
let main argv =
    for name in argv do 
//  name = "http://news.bbc.co.uk" 
//  get first chars 
//  Either use take, then convert back to string 
//  take is only available on Seq collections!?! 
//        let htmlOut = http "http://news.bbc.co.uk" |> Seq.take 200 |> Seq.toArray |> System.String
//        let htmlOut = http name |> Seq.take 200 |> Seq.toArray |> System.String
//  Or use dot notation to take first lot 
//  let htmlOut = (http "http://news.bbc.co.uk").[..200]   
        let htmlOut = (http name).[..200]   
        printfn  "http: %s"  htmlOut 
        printfn  "-----------------------------------------------"           
    printfn "Bye from ExpertF#Ch02B"
    0 // return an integer exit code
