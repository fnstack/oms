[<AutoOpen>]
module OMS.API.ProductBrandHttpHandlers

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open MongoDB.Bson
open OMS.Data
open OMS.Application
open FsToolkit.ErrorHandling.CE.Result

let createProductBrandHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! input = ctx.BindJsonAsync<CreateProductBrandInput>()
            let! result = input
                          |> createProductBrandInputValidator.ValidateAsync
            return! match result.IsValid with
                    | false ->
                        let message = result |> aggregateErrorMessages
                        RequestErrors.BAD_REQUEST message next ctx
                    | true ->
                        (productBrandCollection, input)
                        |> createProductBrand
                        Successful.OK () next ctx
        }

let editProductBrandHandler (productBrandId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match productBrandId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productBrandId ->
                        task {
                            let! input = ctx.BindJsonAsync<EditProductBrandInput>
                                             ()
                            let! result = input
                                          |> editProductBrandInputValidator.ValidateAsync
                            match result.IsValid with
                            | false ->
                                let message = result |> aggregateErrorMessages
                                return! RequestErrors.BAD_REQUEST message next
                                            ctx
                            | true ->
                                (productBrandCollection, input)
                                |> editProductBrand
                                return! Successful.OK () next ctx
                        }
        }

let deleteProductBrandHandler (productBrandId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match productBrandId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productBrandId ->
                        (productBrandCollection, productBrandId)
                        |> deleteProductBrand
                        |> ignore
                        Successful.OK (productBrandId) next ctx
        }

let getProductBrandsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let brands = productBrandCollection |> getProductBrands
            return! Successful.OK brands next ctx
        }
        
let getProductBrandByIdHandler(productBrandId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
           return! match productBrandId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est incorrect"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productBrandId ->
                        let productBrand = (productBrandCollection, productBrandId)
                                            |> getProductBrandById                     
                        Successful.OK (productBrand) next ctx
        }
        
let searchProductBrandsHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return!(
                result {
                    let! searchTerm = ctx.GetQueryStringValue "searchTerm"
                    
                    return searchTerm
                }
                |> function
                    | Ok searchTerm ->
                        let brands = (productBrandCollection, searchTerm) |> searchProductBrands
                        Successful.OK brands next ctx
                    | Error error ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx)
         
         }
