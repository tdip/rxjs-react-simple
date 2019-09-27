namespace Fable.RxJs

open Fable.Core

[<AutoOpen>]
module Operators =

    type [<AllowNullLiteral>] Operators =
        abstract map: ('t -> 'u) -> Operator<'t -> 'u>
        abstract switchMap: ('t -> Observable<'u>) -> Operator<'t -> 'u>
        abstract tap: ('t -> unit) -> Operator<'t -> 't>

    let pipe op (os: Observable<'t>) = os.pipe op

    let (|>>) (os: Observable<'t>) op = os.pipe op

    [<ImportAll("rxjs/operators")>]
    let Operators : Operators = jsNative
