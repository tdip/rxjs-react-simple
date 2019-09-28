namespace Fable.RxJs

open System

type [<AllowNullLiteral>] Subscription =
    abstract unsubscribe: unit -> unit

module Subscription =
    let asDisposable (s: Subscription) =
        {
            new IDisposable with
                member __.Dispose() = s.unsubscribe()
        }

type [<AllowNullLiteral>] Observable<'t> =
    abstract pipe: Operator<'t -> 'u> -> Observable<'u>
    abstract subscribe: ('t -> unit) -> Subscription

and Operator<'t> = interface end