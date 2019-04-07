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



