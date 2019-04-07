[<AutoOpen>]
module OMS.Data.ProductBrandRepository

open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver
open MongoDB.Driver
open OMS.Application

let getProductBrands (collection : IMongoCollection<ProductBrandDto>) =
    let brands =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    brands

let getProductBrandById (collection : IMongoCollection<ProductBrandDto>, brandId : ObjectId) =
    let filter = Builders<ProductBrandDto>.Filter.Where(fun x -> x.Id = brandId)
    let brand =
        collection.Find(filter).ToEnumerable() |> Seq.head
    brand

let searchProductBrands (collection : IMongoCollection<ProductBrandDto>, searchTerm : string) =
    let filter = Builders<ProductBrandDto>.Filter.Text(searchTerm)
    let brands =
        collection.Find(filter).ToEnumerable() |> Seq.toArray
    brands

let createProductBrand (collection : IMongoCollection<ProductBrandDto>,
                           input : CreateProductBrandInput) =
    let id = ObjectId.GenerateNewId()

    let value =
        { ProductBrandDto.Id = id
          Name = input.Name
          Description = input.Description }
    value |> collection.InsertOne

let deleteProductBrand (collection : IMongoCollection<ProductBrandDto>,
                           productBrandId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = productBrandId)

let editProductBrand (collection : IMongoCollection<ProductBrandDto>,
                         input : EditProductBrandInput) =
    let filter =
        Builders<ProductBrandDto>
            .Filter.Eq((fun x -> x.Id), input.Id |> ObjectId.Parse)
    let update =
        Builders<ProductBrandDto>
            .Update.Set((fun x -> x.Name), input.Name)
            .Set((fun x -> x.Description), input.Description)
    collection.UpdateOne(filter, update) |> ignore
