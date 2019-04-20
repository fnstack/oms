module OMS.Domain.ProductCategoryAggregate

open FSharp.UMX
open System

module ProductCategoryEvent =
    let extractType : ProductCategoryEvent -> EventType =
        function
        | Created _ -> %"Created"
        | Renamed _ -> %"Renamed"

    let extractContext =
        function
        | Created e -> e.Context
        | Renamed e -> e.Context

    let extractEventId = extractContext >> EventContext.extractEventId

module ProductCategoryStateData =
    let init (ctx : EventContext) (productCategory : ProductCategory) =
        { NextId = ctx.EventId + %1
          Events = []
          ProductCategory = productCategory
          CreatedAt = DateTimeOffset.Now
          UpdatedAt = DateTimeOffset.Now }

    let updateContext (ctx : EventContext) =
        fun (data : ProductCategoryStateData) ->
            { data with NextId = ctx.EventId + %1
                        UpdatedAt = DateTimeOffset.Now }
