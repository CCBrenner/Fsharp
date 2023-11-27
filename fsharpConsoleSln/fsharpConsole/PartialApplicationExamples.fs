module PartialApplicationExamples

// create an "adder" by partial application of add
let add42 = (+) 42    // partial application
add42 1
add42 3

// create a new list by applying the add42 function
// to each element
[1;2;3] |> List.map add42

// create a "tester" by partial application of "less than"
let twoIsLessThan = (<) 2   // partial application
twoIsLessThan 1
twoIsLessThan 3

// create a "printer" by partial application of printfn
let printer = printfn "printing param=%i"

// loop over each element and call the printer function
[1;2;3] |> List.iter printer

// filter each element with the twoIsLessThan function
[1;2;3] |> List.filter twoIsLessThan


// an example using List.map
let add1 = (+) 1
let add1ToEach = List.map add1   // fix the "add1" function

// test
add1ToEach [1;2;3;4]


let addBaboon = (+) " Baboon"
let addBaboonToEach :(string list->string list) = List.map addBaboon   // fix the "add1" function

let family = ["Papa";"Mama";"Brotha";"Sista"]
addBaboonToEach family

// an example using List.filter
let filterEvens =
  List.filter (fun i -> i%2 = 0) // fix the filter function

// test
filterEvens [1;2;3;4]


// Using lambda functions:
List.map    (fun i -> i+1) [0;1;2;3]
List.filter (fun i -> i>1) [0;1;2;3]
List.sortBy (fun i -> -i ) [0;1;2;3]

// Using partial application functions:
let eachAdd1 = List.map (fun i -> i+1)
eachAdd1 [0;1;2;3]

let excludeOneOrLess = List.filter (fun i -> i>1)
excludeOneOrLess [0;1;2;3]

let sortDesc = List.sortBy (fun i -> -i)
sortDesc [0;1;2;3]



// create wrappers for .NET string functions
let replace oldStr newStr (s:string) =
    s.Replace(oldValue=oldStr, newValue=newStr)

let startsWith (lookFor:string) (s:string) =
    s.StartsWith(lookFor)


let result =
    "hello"
    |> replace "h" "j"
    |> startsWith "j"

["the"; "quick"; "brown"; "fox"]
    |> List.filter (startsWith "f")

// Same as "let result" above only this time using function composition:
let compositeOp = replace "h" "j" >> startsWith "j"
let result1 = compositeOp "hello"

let add x y = x + y
//(1+2) add (3+4)        // error
1+2 |> add <| 3+4        // pseudo infix

let F1 x y z = x y z  // left associative

// these are right associative:
let F2 x y z = x (y z)
let F3 x y z = y z |> x    // using forward pipe
let F4 x y z = x <| y z    // using backward pipe