namespace OMS.Domain
open System
open System

type ProductCategory =
    { Id : ProductCategoryId
      Name : ProductCategoryName
      Description : ProductCategoryDescription option }

type ProductCategoryCommand =
    | Create of ProductCategory
    | Rename of {|
                 Id : ProductCategoryId
                 NewName : ProductCategoryName |}
    | ChangeDescription of {|
                            Id : ProductCategoryId
                            NewDescription : ProductCategoryDescription |}
    | Delete of {| Id : ProductCategoryId |}

type ProductCategoryEvent =
    | Created of {| ProductCategory : ProductCategory; Context : EventContext |}
    | Renamed of {|
                 Id : ProductCategoryId
                 NewName : ProductCategoryName
                 Context : EventContext|}
//    interface type
    
type ProductCategoryEvents = ProductCategoryEvent list

type ProductCategoryStateData = {
    NextId : EventId
    Events : (EventType * EventContext) list
    ProductCategory : ProductCategory
    CreatedAt : DateTimeOffset
    UpdatedAt : DateTimeOffset
}

type ProductCategoryState =
    | NonExistent
    | Existent of ProductCategoryStateData
