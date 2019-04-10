[<AutoOpen>]
module OMS.API.Config

open MongoDB.Bson
open MongoDB.Bson.Serialization
open MongoDB.Bson.Serialization.IdGenerators
open MongoDB.Bson.Serialization.Serializers
open MongoDB.Driver
open MongoDB.Driver
open OMS.Application

let mongoDbConnectString = "mongodb://localhost:27017"
let mongo = mongoDbConnectString |> MongoClient
let db = "oms_db" |> mongo.GetDatabase
let productCategoryCollection =
    "product-categories" |> db.GetCollection<ProductCategoryDto>
let productBrandCollection =
    "product-brands" |> db.GetCollection<ProductBrandDto>
let productCollection = "products" |> db.GetCollection<ProductDto>

let initDb() =
    Builders<ProductBrandDto>.IndexKeys.Text(fun x -> x.Name :> obj)
    |> productBrandCollection.Indexes.CreateOne
    |> ignore
    Builders<ProductDto>.IndexKeys.Text(fun x -> x.Name :> obj)
    |> productCollection.Indexes.CreateOne
    |> ignore
//    Builders<ProductBrandDto>.IndexKeys.Text(fun x -> x.Description :> obj)
//    |> productBrandCollection.Indexes.CreateOne |> ignore
