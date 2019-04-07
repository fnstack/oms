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
        
let getProductByIdHandler(productId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
           return! match productId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est incorrect"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productId ->
                        let product = (productCollection, productId)
                                            |> getProductById                     
                        Successful.OK (product) next ctx
        }
        
let createProductsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! input = ctx.BindJsonAsync<CreateProductInput>()
            let! result = input
                          |> createProductInputValidator.ValidateAsync
            return! match result.IsValid with                    
                    | false ->
                        let message = result |> aggregateErrorMessages
                        RequestErrors.BAD_REQUEST message next ctx
                    | true ->
                        (productCollection, input)
                        |> createProduct
                        Successful.OK () next ctx
        }
        
let deleteProductHandler (productId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match productId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est incorrecte"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productId ->
                        (productCollection, productId)
                        |> deleteProduct
                        |> ignore
                        Successful.OK (productId) next ctx
        }