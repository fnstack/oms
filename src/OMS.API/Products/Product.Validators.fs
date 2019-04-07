[<AutoOpen>]
module OMS.API.ProductValidators

open FluentValidation
open MongoDB.Bson
open OMS.Application

type CreateProductInputValidator() =
    inherit AbstractValidator<CreateProductInput>()
    do
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore
            
        base.RuleFor(fun input -> input.BrandId).NotEmpty()
            .Must(fun value -> match value |> ObjectId.TryParse with (true, _) -> true | false, _ -> false)
        |> ignore
        base.RuleFor(fun input -> input.CategoryId).NotEmpty()
            .Must(fun x -> match x |> ObjectId.TryParse with (true, _) -> true | false, _ -> false)
        |> ignore

let createProductInputValidator =
    new CreateProductInputValidator()
    
type EditProductInputValidator() =
    inherit AbstractValidator<EditProductInput>()
    do
        base.RuleFor(fun input -> input.Id).NotEmpty()
            .Must(fun value -> match value |> ObjectId.TryParse with (true, _) -> true | false, _ -> false)
        |> ignore
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore
        base.RuleFor(fun input -> input.BrandId).NotEmpty()
            .Must(fun value -> match value |> ObjectId.TryParse with (true, _) -> true | false, _ -> false)
        |> ignore
        base.RuleFor(fun input -> input.CategoryId).NotEmpty()
            .Must(fun x -> match x |> ObjectId.TryParse with (true, _) -> true | false, _ -> false)
        |> ignore

let editProductInputValidator = new EditProductInputValidator()

