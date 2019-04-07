[<AutoOpen>]
module OMS.API.Router

open Saturn
open Giraffe

let appRouter =
    router {
        not_found_handler (text "resource not found")
        get "/" (text "welcome to OMS API")
        forward "/api/products/categories" productCategoryRouter
        forward "/api/products/brands" productBrandRouter
        forward "/api/products" productRouter 
    }
