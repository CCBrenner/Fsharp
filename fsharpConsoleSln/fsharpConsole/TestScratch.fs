module TestScratch


// Test 1: How to update the label of a record.

type ValueStatus =
    | Given
    | Unconfirmed
    | Confirmed

type Cell = 
    { Id: int
      RowId: int
      ColId: int
      BlockId: int
      BlockRowId: int
      BlockColId: int
      Value: int
      ValueStatus: ValueStatus
      Values: array<int> }

let defaultCellValue = 0

let getBaseId modNum operation id = (operation (id-1) modNum)+1
let getCellLvlId = getBaseId 9
let getBlockLvlId = getBaseId 3

// constructor:
let create id =
    let values = [| for i in 0 .. 9 -> i |]
    let rowId = getCellLvlId (/) id  //let rowId = (id-1)/9+1
    let colId = getCellLvlId (%) id  //let colId = (id-1)%9+1
    let blockId = ((rowId-1)/3*3)+((colId-1)/3)+1  // "/3*3" eliminates the remainder
    let blockRowId = getBlockLvlId (/) blockId  // (blockId-1)/3+1
    let blockColId = getBlockLvlId (%) blockId  // (blockId-1)%3+1
    let cell =
        { Id=id
          RowId=rowId
          ColId=colId
          BlockId=blockId
          BlockRowId=blockRowId
          BlockColId=blockColId
          Value=defaultCellValue
          ValueStatus=Unconfirmed
          Values=values }
    cell

let singleCellInAList = [ create 1 ]

// Because lists in F# are immutable, we need to return a whole new list. Use List.map to do so:
let myFunc theList targetId replacementVal = 
    theList |> List.map (fun x -> if x.Id=targetId then { x with Value=replacementVal } else x)

let newList = myFunc singleCellInAList 1 4 // Success: Value=4


// Test 2: Can unit be stored in a list?
let someList = [(); (); ()]


// Test 3: Higher-order functions

// List to use with this test
let myList = [4;5;6]

// You can think of higher-order functions like having the body of the loop outside and then bringing it inside the loop when called.

// Seq.iter applies a function to each element in the sequence
// This:
for item in myList do
    printfn "%i" item

// is the same as this:
myList |> List.iter (fun item -> printfn "%i" item)

// and this:
let rec iter items =
    match items with 
    | [] -> ()
    | head::tail ->
        printfn "%i" head
        iter tail

iter myList

// and also this:
let myFunc = printfn "%i"
myList |> List.iter myFunc

// Seq.reduce - can be applied to monoids only (Closure, Associative, Identity)
// Add all elements of a sequence together:
let capturedResultOne = myList |> List.reduce (+)

// Multiply then instead of add them
let capturedResultTwo = myList |> List.reduce (*)

// Or concat a string list (based on how (+) handles strings normally):
let myStringList = ["one"; "two"; "three"]
let capturedResultFour = myStringList |> List.reduce (+)

// Seq.filter - is functionally the same as System.Linq.Select()
// Get all even ints in myList:
let capturedResultThree = myList |> List.filter (fun x -> x%2=0)

// or Get all odd ints in myList:
let getOdd num = num%2=1
let capturedResultThree = myList |> List.filter getOdd

// Seq.fold - takes a function value and two simple value as arguments; performs a recursive/iterative action with a starting value
// "Fold" like paper: You start with a section, then you fold the section, then you fold 
// the same length of section again, and again, again until there is no more paper left - paper = sequence, section = value

// Find sum of a list (assumes starting from the sum identifier, 0):
let foldFunc state valFromList = state + valFromList
let foldedResultOne = myList |> List.fold foldFunc 0  // '0' is the state, used as the base/starting point; = 15

let foldedResultTwo = myList |> List.fold foldFunc 10  // '10' is the state, used as the base/starting point; = 25

// Seq.collect - performs a function on each element and then combines multiple generated sequences into one sequence
// Note that the [for i in 1..n -> __] portion is expected when using Seq.collect
let list1 = [10; 20; 30]
let collectList = List.collect (fun x -> [for i in 1..3 -> x * i]) list1

let x = [[10; 20; 30]; [20; 40; 60]; [30; 60; 90]]
let capturedResultFive = List.collect id x  // combines lists without changing any values (uses the built-in identity function)


// Test 4: The Identity Function: id
// id returns the value as the result (the domain as the range) without performing any
// transformation on it (or you could say the identity transformation was performed on the input)
let yFunc = id  // view the type signature of id aliased as y
let zFunc = fun x -> x  // id is the same as this
let aaFunc x = x  // and this

// Example from Seq.collect (to essentially SKIP the function step and utilize the list combining part of Seq.collect):
let x = [[10; 20; 30]; [20; 40; 60]; [30; 60; 90]]
// All the same function value:
let capturedResultSix = List.collect id x
let capturedResultSeven = List.collect yFunc x
let capturedResultEight = List.collect zFunc x
let capturedResultNine = List.collect aaFunc x


// Test 5: DI in FP via Partial Application
// DI is normally done in OOP with class constructor parameters to capture the parameter for later use.
// DI in FP is accomplished in a similar way, but we use functions instead of classes for our DI - but how?
// We can partially apply a function value to return a function value. This implies that the starting function
// has more than one parameter, since we are getting back a function value with atleast one parameter.

// Performing DI with a function value:
let firstFunc n1 n2 = n1-n2  // useless in its practical sense
let secondFunc f s1 s2 = f s2 s1  // reverse the parameters (to demonstrate a difference)
let thirdFunc = secondFunc firstFunc  // partial application
// thirdFunc uses partial application by applying only firstFunc as the first argument of secondFunc, creating
// a new function value from the two function values. The second and third parameters of secondFunc are all that
// is left in thirdFunc.
let capturedResultTen = thirdFunc 1 2  // 1
let capturedResultEleven = firstFunc 1 2  // -1
// You could do the same thing with Seq.map, to confirm the order of the ints is being taken correctly:
let shortIntList = [1;2]
let capturedResultTwelve = List.reduce thirdFunc shortIntList


// Test 6: Mapping non-monoids to being monoids, then reducing them (otherwiseknown as "map/reduce")
// to be completed (*concept introduced in Scott Wlaschin's talk on Functional Design Patterns)
// uses Custom (-> map) CustomerStat (-> reduce) CustomerTotals as example (without actual code)