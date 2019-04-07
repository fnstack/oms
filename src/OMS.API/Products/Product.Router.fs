[<AutoOpen>]
module OMS.API.ProductRouter


open Saturn
open Giraffe

let productRouter =
    router{
        not_found_handler(text "Resource not found")
        get "" getProductsHandler
    }
