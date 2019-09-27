module Rxjs.React

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.React

let buildPropsObservable (props) = Fable.RxJs.isObservable props


let wrap (inner: obj) =
    let comp (props: 't) =
        let mutable count = 0
        let effect() = count <- count + 1
        Fable.React.HookBindings.Hooks.useEffect(effect)
        [
            ReactBindings.React.createElement(inner, props, [])
            str (sprintf "count is %i" count)
        ]
        |> Fable.React.Standard.div []
    comp

let sayHelloFable() = "Hello Fable!"