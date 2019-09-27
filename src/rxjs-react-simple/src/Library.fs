module Rxjs.React

open Fable
open Fable.Core
open Fable.Core.DynamicExtensions
open Fable.Core.JsInterop
open Fable.Import
open Fable.React
open Fable.RxJs

/// <summary>
/// Given an object with properties that might be
/// observables, construct a new observable that
/// emits objects with the same structure as
/// the original object but with values
/// from the observable instad of observables
/// as properties.
/// 
/// Sine this funciton is meant to be used in the
/// 'useEffects' hook, an array of dependencies
/// is also provided.
/// </summary>
let buildPropsObservable (props: JS.Object) =
    if isObservable props
    then ([|props :> obj|], props :?> RxJs.Observable<obj>)
    else
        let values =
            Fable.Core.JS.Object.getOwnPropertyNames(props)
            |> Seq.map (fun key -> (key, props.[key]))
        let dependencies = values |> Seq.map (fun (_,v) -> v) |> Seq.toArray
        let mapProp ((prop, value): string*obj) =

            if isObservable value
            then
                (value :?> RxJs.Observable<obj>)
                |>> Operators.map (fun value -> (prop, value))
            else
                RxJs.``of``(prop, value)
        let inner =
            values
            |> Seq.map mapProp
            |> Seq.toArray
            |> RxJs.combineLatest

        let propsObservable =
            inner
            |>> Operators.map createObj

        (dependencies, propsObservable)

/// <summary>
/// Wraps a component to allow it to receive rxjs observables
/// in it's properties. This wrapper takes care of subscribing/unsubscribing
/// to the observable during the execution of it's lifecycle components
/// </summary>
let wrap (inner: obj) =
    let comp props =
        let (dependencies, propsObservable) = buildPropsObservable props
        let state = Hooks.useState None
        let subscribeToProps() =
            let subscriber value = state.update(fun _ -> Some value) 
            propsObservable.subscribe subscriber
            |> Subscription.asDisposable

        do Hooks.useEffectDisposable(subscribeToProps, dependencies)

        match state.current with
        | None -> div [] []
        | Some state -> ReactBindings.React.createElement(inner, state, [])
    comp