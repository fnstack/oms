[<AutoOpen>]
module OMS.Domain.Aggregate

open System

module EventContext =
    let create eventId = { EventId = eventId }
    let extractEventId (ctx : EventContext) = ctx.EventId
