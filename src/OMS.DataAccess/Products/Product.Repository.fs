[<AutoOpen>]
module OMS.Data.ProductRepository

open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver
open MongoDB.Driver
open OMS.Application

let getProducts (collection : IMongoCollection<ProductDto>) =
    let Products =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    Products
    
let getProductById (collection : IMongoCollection<ProductDto>, productId : ObjectId) =
    let filter = Builders<ProductDto>.Filter.Where(fun x -> x.Id = productId)
    let product =
        collection.Find(filter).ToEnumerable() |> Seq.head
    product

let createProduct (collection : IMongoCollection<ProductDto>,
                           input : CreateProductInput) =
    let id = ObjectId.GenerateNewId()

    let value =
        { ProductDto.Id = id
          Name = input.Name
          Description = input.Description
          BrandId = input.BrandId
          CategoryId = input.CategoryId
          Price = input.Price }
    value |> collection.InsertOne

let deleteProduct (collection : IMongoCollection<ProductDto>,
                           productId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = productId)