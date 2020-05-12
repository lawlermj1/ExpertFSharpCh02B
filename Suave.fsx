
open System ;; 

#r @"C:\Users\ellam\.nuget\packages\fsharp.data\3.3.3\lib\netstandard2.0\FSharp.Data.dll" ;; 

open FSharp.Data ;;

type Species = HtmlProvider<"https://en.wikipedia.org/wiki/The_world%27s_100_most_threatened_species"> ;; 

let species = 
    [ for x in Species.GetSample().Tables.``Species list``.Rows -> 
        x.Type, x.``Common name`` ] ;; 

// ->         
// val species : (string * string) list =
//  [("Plant (Tree)", "Baishan fir"); ("Insect (butterfly)", "");
//   ("Reptile", "Leaf scaled sea-snake");
//   ("Insect (damselfly)", "Amani flatwing"); ("Insect", "");
//   ("Bird", "Araripe manakin"); ("Fish", "Aci Göl toothcarp");

let speciesSorted = 
    species 
        |> List.countBy fst 
        |> List.sortByDescending snd 

// -> 
// val speciedSorted : (string * int) list =
//  [("Plant", 12); ("Bird", 11); ("Fish", 11); ("Amphibian", 8);
//   ("Plant (tree)", 8); ("Mammal (primate)", 7); ("Reptile", 6); ("Mammal", 4);

#r @"C:\Users\ellam\.nuget\packages\suave\2.5.6\lib\netstandard2.0\Suave.dll" ;; 

open Suave ;; 
open Suave.Http ;; 
open Suave.Successful ;; 
open Suave.Web ;; 

let html = 
    [ yield "<html><body><ul>"
      for (category,count) in speciesSorted do 
         yield sprintf "<li>Category <b>%s</b>: <b>%d</b><li>" category count
      yield "</ul></body></html>"]
    |> String.concat "\n" 

// startWebServer defaultConfig (OK html) 

let angularHeader = """<head> 
   <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"> 
   <script src="http:/ajax.googleapis.com/ajax/libs/angularjs/1.2.26/angular.min.js">
   </script></head>"""

let fancyText =
    [ yield """<html>"""
      yield angularHeader
      yield """ <body>"""
      yield """  <table class="table table-striped">"""
      yield """   <thead><tr><th>Category</th><th>Count</th></tr></thead>""" 
      yield """   <tbody>"""
      for (category,count) in speciesSorted do 
         yield sprintf "<tr><td>%s</td><td>%d</td></tr>" category count 
      yield """   </tbody>"""
      yield """  </table>"""      
      yield """ </body>"""
      yield """</html>"""      
    ] |> String.concat "\n"

startWebServer defaultConfig (OK fancyText) 
