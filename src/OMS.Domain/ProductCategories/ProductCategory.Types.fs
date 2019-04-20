namespace OMS.Domain

open FSharp.UMX

[<Measure>]
type productCategoryId

type ProductCategoryId = Guid<productCategoryId>

[<Measure>]
type productCategoryName

type ProductCategoryName = string<productCategoryName>

[<Measure>]
type productCategoryDescription

type ProductCategoryDescription = string<productCategoryDescription>
