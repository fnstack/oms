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
    
let searchProducts (collection : IMongoCollection<ProductDto>, searchTerm : string) =
    let filter = Builders<ProductDto>.Filter.Text(searchTerm)
    let produts =
        collection.Find(filter).ToEnumerable() |> Seq.toArray
    produts
    
let editProduct (collection : IMongoCollection<ProductDto>,
                         input : EditProductInput) =
    let filter =
        Builders<ProductDto>
            .Filter.Eq((fun x -> x.Id), input.Id |> ObjectId.Parse)
    let update =
        Builders<ProductDto>
            .Update.Set((fun x -> x.Name), input.Name)
            .Set((fun x -> x.Description), input.Description)
            .Set((fun x -> x.BrandId), input.BrandId)
            .Set((fun x -> x.CategoryId), input.CategoryId)
            .Set((fun x -> x.Price), input.Price) 
    collection.UpdateOne(filter, update) |> ignore

let deleteProduct (collection : IMongoCollection<ProductDto>,
                           productId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = productId)