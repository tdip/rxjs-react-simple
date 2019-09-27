import * as ReactDom from "react-dom"
import * as React from "react"

const {wrap} = require('@tdip/rxjs-react-simple');

function Comp(props){
    return <div>Kaiser!!!</div>
}

const RxComp = wrap(Comp);

console.log("hello", RxComp);

setTimeout(
    () =>{
        ReactDom.render(
            <RxComp />,
            document.getElementById('app'));
    },
    200);

