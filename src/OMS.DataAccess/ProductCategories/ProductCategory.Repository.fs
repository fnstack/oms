[<AutoOpen>]
module OMS.Data.ProductCategoryRepository

open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver
open OMS.Application

let getProductCategories (collection : IMongoCollection<ProductCategoryDto>) =
    let categories =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    categories

let createProductCategory (collection : IMongoCollection<ProductCategoryDto>,
                           input : CreateProductCategoryInput) =
    let id = ObjectId.GenerateNewId()

    let value =
        { ProductCategoryDto.Id = id
          Name = input.Name
          Description = input.Description }
    value |> collection.InsertOne

let deleteProductCategory (collection : IMongoCollection<ProductCategoryDto>,
                           productCategoryId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = productCategoryId)

let editProductCategory (collection : IMongoCollection<ProductCategoryDto>,
                         input : EditProductCategoryInput) =
    let filter =
        Builders<ProductCategoryDto>
            .Filter.Eq((fun x -> x.Id), input.Id |> ObjectId.Parse)
    let update =
        Builders<ProductCategoryDto>
            .Update.Set((fun x -> x.Name), input.Name)
            .Set((fun x -> x.Description), input.Description)
    collection.UpdateOne(filter, update) |> ignore
