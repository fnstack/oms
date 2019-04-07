namespace OMS.Application

open MongoDB.Bson
open System

type CreateProductBrandInput =
    { Name : string
      Description : string }

type EditProductBrandInput =
    { Id : string
      Name : string
      Description : string }

[<CLIMutable>]
type ProductBrandDto =
    { Id : ObjectId
      Name : string
      Description : string }
