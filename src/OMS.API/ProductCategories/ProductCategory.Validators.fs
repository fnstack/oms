[<AutoOpen>]
module OMS.API.ProductCategoryValidators

open FluentValidation
open OMS.Application

type CreateProductCategoryInputValidator() =
    inherit AbstractValidator<CreateProductCategoryInput>()
    do
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore

let createProductCategoryInputValidator =
    new CreateProductCategoryInputValidator()

type EditProductCategoryInputValidator() =
    inherit AbstractValidator<EditProductCategoryInput>()
    do
        base.RuleFor(fun input -> input.Id).NotEmpty() |> ignore
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore

let editProductCategoryInputValidator = new EditProductCategoryInputValidator()
