[<AutoOpen>]
module OMS.API.Validation

open System
open FluentValidation.Results

let aggregateErrorMessages (result : ValidationResult) =
    result.Errors
    |> Seq.map (fun error -> error.ErrorMessage)
    |> Seq.reduce
           (fun current failure ->
           current + failure + System.Environment.NewLine)

let isNullObject value = Object.ReferenceEquals(value, null)
