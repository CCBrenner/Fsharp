module StackCalc

// Creating Stack single case union:
type Stack = StackContents of float list

let newStack = StackContents [ 1.0; 2.0; 3.0 ]

// Defining push:
let push x (StackContents content) =
    StackContents (x::content)  // Note: pushes to the front of the list (it's just simpler in F#)

// Defining pop:
let pop (StackContents content) =
    match content with
    | first::rest ->
        let newContent = StackContents rest
        (first, newContent)  // returns a tuple of head and tail
    | [] ->
        failwith "Stack underflow"  // this is a generic exception ("failwith")

// Numeric commands of stack calculator:
let EMPTY = StackContents []
let ONE = push 1.0
let TWO = push 2.0
let THREE = push 3.0
let FOUR = push 4.0
let FIVE = push 5.0

// Testing push:
let stackWith1 = ONE EMPTY
let stackWith1a = EMPTY |> ONE
let stackWith2 = TWO stackWith1
let stackWith2a = stackWith1a |> TWO
let stackWith3  = THREE stackWith2
let stackWith3a  = stackWith2a |> THREE

let result123 = EMPTY |> ONE |> TWO |> THREE
let result321 = EMPTY |> THREE |> TWO |> ONE

// Testing pop:
let initialList = EMPTY |> ONE |> TWO
let first, listTwo = pop initialList
let second, listThree = pop listTwo

let _ = pop EMPTY

// Common process with variable defining operation:
let binary mathFunc content =
    let x, content' = pop content
    let y, content'' = pop content'
    let result = mathFunc y x
    push result content''

// Math funcs:
let ADD = binary (+)  // missing final param means ADD is a function
let MUL = binary (*)
let SUB = binary (-)
let DIV = binary (/)

// Test ADD and MUL:
let add1and2 = EMPTY |> ONE |> TWO |> ADD
let add2and3 = EMPTY |> TWO |> THREE |> ADD
let mult2and3 = EMPTY |> TWO |> THREE |> MUL

let threeDivTwo = EMPTY |> THREE |> TWO |> DIV   // Answer: 1.5
let twoSubtractFive = EMPTY |> TWO |> FIVE |> SUB  // Answer: -3.0
let oneAddTwoSubThree = EMPTY |> ONE |> TWO |> ADD |> FOUR |> SUB // Answer: 0.0

// Unary functions take a single argument but are similar:
let unary f stack =
    let x, stack' = pop stack
    push (f x) stack'

let SQUARE = unary (fun x -> x*x)
let NEG = unary (fun x -> -x)

// Test unary functions:
let neg3 = EMPTY |> THREE |> NEG
let square2 = EMPTY |> TWO |> SQUARE

// Show func:
let SHOW content =
    let x, _ = pop content
    printfn "The answer is %f" x
    content  // return unaltered stack content

// Test it:
let test = EMPTY |> ONE |> THREE |> ADD |> TWO |> MUL |> SHOW // (1+3)*2 = 8

// More operations:
let DUP content =
    let x, _ = pop content
    push x content

let SWAP content =
    let x, content' = pop content
    let y, content'' = pop content'
    push x content'' |> push y

let START = EMPTY

// Testing:
let lestF1 = START |> ONE |> TWO |> SHOW
let testF2 = START |> ONE |> TWO |> ADD |> SHOW |> THREE |> ADD |> SHOW
let testF3 = START |> THREE |> DUP |> DUP |> MUL |> MUL |> SHOW  // 27 (:3-cubed)
let testF4 =  // in vertical form:
    START
    |> ONE 
    |> TWO 
    |> ADD 
    |> SHOW  // 3
    |> THREE 
    |> MUL 
    |> SHOW  // 9
    |> TWO 
    |> DIV 
    |> SHOW  // 9 div 2 = 4.5

let ONE_TWO_ADD =
    ONE >> TWO >> ADD

// test it
let testF5 = START |> ONE_TWO_ADD |> SHOW

// define a new function
let COMPOSED_SQUARE =
    DUP >> MUL

// test it
let testF5b = START |> TWO |> COMPOSED_SQUARE |> SHOW

// define a new function
let COMPOSED_CUBE =
    DUP >> DUP >> MUL >> MUL

// test it
let testF6 = START |> THREE |> COMPOSED_CUBE |> SHOW

// define a new function
let SUM_NUMBERS_UPTO =
    DUP      // n, n           2 items on stack
    >> ONE   // n, n, 1        3 items on stack
    >> ADD   // n, (n+1)       2 items on stack
    >> MUL   // n(n+1)         1 item on stack
    >> TWO   // n(n+1), 2      2 items on stack
    >> DIV   // n(n+1)/2       1 item on stack

// test it with sum of numbers up to 9
let testF7 = START |> THREE |> SQUARE |> SUM_NUMBERS_UPTO |> SHOW  // 45
