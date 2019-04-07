namespace OMS.Application

open MongoDB.Bson
open System

type CreateProductInput =
    { Name : string
      Description : string
      BrandId : string
      CategoryId : string
      Price : string }

type EditProductInput =
    { Id : string
      Name : string
      Description : string
      BrandId : string
      CategoryId : string
      Price : string }

[<CLIMutable>]
type ProductDto =
    { Id : ObjectId
      Name : string
      Description : string
      BrandId : string
      CategoryId : string
      Price : string }
