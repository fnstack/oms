namespace OMS.Application

open MongoDB.Bson
open System

type CreateProductCategoryInput =
    { Name : string
      Description : string }

type EditProductCategoryInput =
    { Id : string
      Name : string
      Description : string }

[<CLIMutable>]
type ProductCategoryDto =
    { Id : ObjectId
      Name : string
      Description : string }
