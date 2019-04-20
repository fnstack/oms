namespace OMS.Domain

type ProductCategory =
    { Id : ProductCategoryId
      Name : ProductCategoryName
      Description : ProductCategoryDescription option }

type ProductCategoryCommand =
    | Create of ProductCategory
    | Rename of id : ProductCategoryId * newName : ProductCategoryName
    | ChangeDescription of id : ProductCategoryId * newDescription : ProductCategoryDescription
    | Delete of id : ProductCategoryId
//type ProductCategoryEvent =
//    | Created of ProductCategory
//    | Renamed
