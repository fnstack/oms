[<AutoOpen>]
module OMS.API.ProductHttpHandlers

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open MongoDB.Bson
open OMS.Data
open OMS.Application
open FsToolkit.ErrorHandling.CE.Result

let getProductsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let Products = productCollection |> getProducts
            return! Successful.OK Products next ctx
        }