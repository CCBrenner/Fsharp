module LamdaAndPatternMatchingExamples

let adderGenerator1 x y = x + y
let adderGenerator2 x   = fun y -> x + y
let adderGenerator3     = fun x -> (fun y -> x + y)

type Name = {first:string; last:string} // define a new type
let bob = {first="bob"; last="smith"}   // define a value

// single parameter style
let f1 name =                       // pass in single parameter
   let {first=f; last=l} = name     // extract in body of function
   printfn "first=%s; last=%s" f l

// match in the parameter itself
let f2 {first=f; last=l} =          // direct pattern matching
   printfn "first=%s; last=%s" f l

// test
f1 bob
f2 bob


let f (x,y,z) = x + y * z
// type is int * int * int -> int

// test
let result0 = f (1,2,3)



// create a wrapper function
let strCompare x y = System.String.Compare(x,y)

// partially apply it
let strCompareWithB = strCompare "B"

// use it with a higher order function
let result1 =
   ["D";"A";"B";"C"]
   |> List.map strCompareWithB