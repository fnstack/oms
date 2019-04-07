[<AutoOpen>]
module OMS.API.ProductBrandValidators

open FluentValidation
open OMS.Application

type CreateProductBrandInputValidator() =
    inherit AbstractValidator<CreateProductBrandInput>()
    do
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore

let createProductBrandInputValidator =
    new CreateProductBrandInputValidator()

type EditProductBrandInputValidator() =
    inherit AbstractValidator<EditProductBrandInput>()
    do
        base.RuleFor(fun input -> input.Id).NotEmpty() |> ignore
        base.RuleFor(fun input -> input.Name).NotEmpty().MaximumLength(50)
            .MinimumLength(5) |> ignore

let editProductBrandInputValidator = new EditProductBrandInputValidator()
