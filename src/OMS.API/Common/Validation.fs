[<AutoOpen>]
module OMS.API.Validation

open FluentValidation.Results

let aggregateErrorMessages (result : ValidationResult) =
    result.Errors
    |> Seq.map (fun error -> error.ErrorMessage)
    |> Seq.reduce
           (fun current failure ->
           current + failure + System.Environment.NewLine)
