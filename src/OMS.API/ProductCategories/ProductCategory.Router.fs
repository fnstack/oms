[<AutoOpen>]
module OMS.API.ProductCategoryRouter

open Saturn
open Giraffe

let productCategoryRouter =
    router {
        not_found_handler (text "resource not found")
        get "" getProductCategoriesHandler
        post "" createProductCategoryHandler
        putf "/%s" editProductCategoryHandler
        deletef "/%s" deleteProductCategoryHandler
    }
