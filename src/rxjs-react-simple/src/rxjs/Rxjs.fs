namespace Fable

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.React

module RxJs =

    [<Import("Observable", from="rxjs")>]
    let Observable : obj = jsNative

    [<Emit("$1 instanceof $0")>]
    let private isObservableHelper (_: obj) (_: obj) = jsNative

    let isObservable (o: obj) =
        isObservableHelper Observable o

    type Operator = interface end

    type Observable<'t> =
        member __.pipe() = ()

