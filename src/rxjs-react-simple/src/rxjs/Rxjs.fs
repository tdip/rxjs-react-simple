namespace Fable

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.React

open Fable.RxJs

[<AutoOpen>]
module RxJs =

    type [<AllowNullLiteral>] RxJs =
        abstract ``of``: 'a -> Observable<'a>
        abstract empty: unit -> Observable<'a>
        abstract combineLatest: Observable<'t>[] -> Observable<'t[]>

    [<ImportAll("rxjs")>]
    let RxJs: RxJs = jsNative

    [<Import("Observable", from="rxjs")>]
    let private Observable : obj = jsNative

    [<Emit("$1 instanceof $0")>]
    let private isObservableHelper (_: obj) (_: obj) = jsNative

    let isObservable (o: obj) : bool =
        isObservableHelper Observable o