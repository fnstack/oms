[<AutoOpen>]
module OMS.API.ProductCategoryHttpHandlers

open System
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open MongoDB.Bson
open OMS.Data
open OMS.Application

let createProductCategoryHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! input = ctx.BindJsonAsync<CreateProductCategoryInput>()
            if input |> isNullObject then
                return! RequestErrors.BAD_REQUEST "Incorrecte value" next ctx
            else
                let! result = input
                              |> createProductCategoryInputValidator.ValidateAsync
                return! match result.IsValid with
                        | false ->
                            let message = result |> aggregateErrorMessages
                            RequestErrors.UNPROCESSABLE_ENTITY message next ctx
                        | true ->
                            (productCategoryCollection, input)
                            |> createProductCategory
                            Successful.OK () next ctx
        }

let editProductCategoryHandler (productCategoryId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match productCategoryId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productCategoryId ->
                        task {
                            let! input = ctx.BindJsonAsync<EditProductCategoryInput>
                                             ()
                            if input |> isNullObject then
                                return! RequestErrors.BAD_REQUEST "Incorrecte value" next ctx
                            else                 
                                let! result = input
                                              |> editProductCategoryInputValidator.ValidateAsync
                                match result.IsValid with
                                | false ->
                                    let message = result |> aggregateErrorMessages
                                    return! RequestErrors.UNPROCESSABLE_ENTITY message next
                                                ctx
                                | true ->
                                    (productCategoryCollection, input)
                                    |> editProductCategory
                                    return! Successful.OK () next ctx
                        }
        }

let deleteProductCategoryHandler (productCategoryId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match productCategoryId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, productCategoryId ->
                        (productCategoryCollection, productCategoryId)
                        |> deleteProductCategory
                        |> ignore
                        Successful.OK (productCategoryId) next ctx
        }

let getProductCategoriesHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let categories = productCategoryCollection |> getProductCategories
            return! Successful.OK categories next ctx
        }
