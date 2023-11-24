module CurryingExamples

// Use F# Interactive to view the value signatures of these values.

let multiParamFn (p1:int)(p2:bool)(p3:string)(p4:float)= ()  //do nothing

let intermediateFn1 = multiParamFn 42
   // intermediateFn1 takes a bool
   // and returns a new function (string -> float -> unit)
let intermediateFn2 = intermediateFn1 false
   // intermediateFn2 takes a string
   // and returns a new function (float -> unit)
let intermediateFn3 = intermediateFn2 "hello"
   // intermediateFn3 takes a float
   // and returns a simple value (unit)
let finalResult = intermediateFn3 3.141
