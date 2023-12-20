module OptionCode

// Creating instances of the Option<'a> type:
let myOption1 = Some (10.0)
let myOption2 = Some ("string")
let myOption3 = None

// Using pattern matching to produce a specific output based on discriminated union subtype:
let printValue opt =
    match opt with
    | Some x -> printfn "%A" x
    | None -> printfn "No value."

printfn $"{printValue myOption1}"
printfn $"{printValue myOption2}"
printfn $"{printValue myOption3}"
