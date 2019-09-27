import * as ReactDom from "react-dom"
import * as React from "react"
import * as Rx from "rxjs"
import * as Op from "rxjs/operators"

const {wrap} = require('@tdip/rxjs-react-simple');

function Comp(props){
    return <div>{props.count}</div>
}

const RxComp = wrap(Comp);

const os = Rx.range(0,100)
    .pipe(
        Op.mergeMap(v => Rx.timer(v*1000).pipe(Op.map(_ => v))));

setTimeout(
    () =>{
        ReactDom.render(
            <RxComp count={os} />,
            document.getElementById('app'));
    },
    200);

