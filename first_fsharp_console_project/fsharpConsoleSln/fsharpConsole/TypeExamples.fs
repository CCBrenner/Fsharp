module TypeExamples

// Use F# Interactive to view the signatures of these values by 
// selecting and pressing Alt + Enter.
let testA   = float 2
let testB x = float 2
let testC x = float 2 + x
let testD x = x.ToString().Length  // dot ntoation, OOP
let testD1 x = x |> string |> String.length  // pipelines, FP
let testE (x:float) = x.ToString().Length  // specifies the domain/input is a float
let testE1 x:float = x |> string |> String.length |> float  // specifies the range/output is a float (which the compile would have assumed already)
let testE2 (x:float):int = 
    x 
    |> string 
    |> String.length  // specifies the domain/input is a float & the range/output is an int
let testF x = printfn "%s" x
let testG x = printfn "%f" x
let testH   = 2 * 2 |> ignore
let testI x = 2 * 2 |> ignore
let testJ (x:int) = 2 * 2 |> ignore
let testK   = "hello"
let testL() = "hello"
let testM x = x=x
let testN x = x 1          // hint: what kind of thing is x?
let testO x:string = x 1   // hint: what does :string modify?
    