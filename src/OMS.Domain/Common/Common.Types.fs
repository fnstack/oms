namespace OMS.Domain

open FSharp.UMX

type Entity = private Entity of string

module Entity =
    let value (Entity entity) = entity
    let create entity = entity |> Entity

[<Measure>]
type eventId

type EventId = int<eventId>

[<Measure>]
type eventType

type EventType = string<eventType>
