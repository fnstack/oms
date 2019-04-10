[<AutoOpen>]
module OMS.API.ProductRouter

open Saturn
open Giraffe

let productRouter =
    router {
        not_found_handler (text "Resource not found")
        get "" getProductsHandler
        getf "/%s" getProductByIdHandler
        get "/search-results" searchProductsHandler
        post "" createProductsHandler
        putf "/%s" editProductHandler
        deletef "/%s" deleteProductHandler
    }
