[<AutoOpen>]
module OMS.API.ProductBrandRouter

open Saturn
open Giraffe

let productBrandRouter =
    router {
        not_found_handler (text "resource not found")
        get "" getProductBrandsHandler
        getf "/%s" getProductBrandByIdHandler
        get "/search-results" searchProductBrandsHandler
        post "" createProductBrandHandler
        putf "/%s" editProductBrandHandler
        deletef "/%s" deleteProductBrandHandler
    }
