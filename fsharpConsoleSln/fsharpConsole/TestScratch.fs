module TestScratch

// Test 1: How to update the label of a record.

type ValueStatus =
    | Given
    | Unconfirmed
    | Confirmed

type Cell = 
    {   Id: int
        RowId: int
        ColId: int
        BlockId: int
        BlockRowId: int
        BlockColId: int
        Value: int
        ValueStatus: ValueStatus
        Values: array<int>   }

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
    {   Id=id
        RowId=rowId
        ColId=colId
        BlockId=blockId
        BlockRowId=blockRowId
        BlockColId=blockColId
        Value=defaultCellValue
        ValueStatus=ValueStatus.Unconfirmed
        Values=values   }

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

// List.iter applies a function to each element in the sequence.
// (List.iter returns () while List.map returns a list with the same type as the input list.)
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
let someFunc = printfn "%i"
myList |> List.iter someFunc

// List.reduce - can be applied to monoids only (Closure, Associative, Identity)
// Add all elements of a sequence together:
let capturedResultOne = myList |> List.reduce (+)

// Multiply then instead of add them
let capturedResultTwo = myList |> List.reduce (*)

// Or concat a string list (based on how (+) handles strings normally):
let myStringList = ["one"; "two"; "three"]
let capturedResultFour = myStringList |> List.reduce (+)

// List.filter - is functionally the same as System.Linq.Select()
// Get all even ints in myList:
let capturedResultThree = myList |> List.filter (fun x -> x%2=0)

// or Get all odd ints in myList:
let getOdd num = num%2=1
let capturedResultThreePointFive = myList |> List.filter getOdd

// List.fold - takes a function value and two simple value as arguments; performs a recursive/iterative action with a starting value
// "Fold" like paper: You start with a section, then you fold the section, then you fold 
// the same length of section again, and again, again until there is no more paper left - paper = sequence, section = value

// Find sum of a list (assumes starting from the sum identifier, 0):
let foldFunc state valFromList = state + valFromList
let foldedResultOne = myList |> List.fold foldFunc 0  // '0' is the state, used as the base/starting point; = 15

let foldedResultTwo = myList |> List.fold foldFunc 10  // '10' is the state, used as the base/starting point; = 25

// List.collect - performs a function on each element and then combines multiple generated sequences into one sequence
// Note that the [for i in 1..n -> __] portion is expected when using List.collect
let list1 = [10; 20; 30]
let collectList = List.collect (fun x -> [for i in 1..3 -> x * i]) list1

let x = [[10; 20; 30]; [20; 40; 60]; [30; 60; 90]]
let capturedResultFive = List.collect id x  // combines lists without changing any values (uses the built-in identity function)


// Test 4: The Identity Function: id
// id returns the value as the result (the domain as the range) without performing any
// transformation on it (or you could say the identity transformation was performed on the input)
let yFunc = id  // view the type signature of id aliased as yFunc
let zFunc = fun x -> x  // id is the same as this
let aaFunc x = x  // and this

// Example from Seq.collect (to essentially SKIP the function step and utilize the list combining part of Seq.collect):
let listInList = [[10; 20; 30]; [20; 40; 60]; [30; 60; 90]]
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


// Test 7: "Flattening" a sequence
// Repeats '1 2 3 4 5' ten times
let getSequence () = 
    [ for _ in 1..10 do yield [1; 2; 3; 4; 5] ]
let capturedResultThirteen = getSequence ()
capturedResultThirteen |> Seq.iter (printf "%A")


// Test 8: List.distinct
let someKindOfList = [1;6;3;3;5;0;2;5;7;4;9]
let capturedResultFourteen = someKindOfList |> List.distinct


// Test 9: Using List.map to get a list of values from label values of records
type someKindOfRecord = { LabelOne:string; LabelTwo:int }
let recordOne = { LabelOne="yoyo"; LabelTwo=5 }
let recordTwo = { LabelOne="yoyo"; LabelTwo=8 }
let recordThree = { LabelOne="yoyo"; LabelTwo=4 }
let listOfRecords = [ recordOne; recordTwo; recordThree ]
let capturedResultFifteen = listOfRecords |> List.map (fun x -> x.LabelTwo)

// Test 10: List.contains
let someListBoi = [1;5;7;9]
let capturedResultSixteen = someListBoi |> List.contains 6

// Test 11: List.map2 usage
type someKindOfRecordTwo = { LabelOne:string; LabelTwo:int }
let recordOneA = { LabelOne="yo"; LabelTwo=5 }
let recordTwoA = { LabelOne="yoyo"; LabelTwo=8 }
let recordThreeA = { LabelOne="yoyoyo"; LabelTwo=4 }
let recordFourA = { LabelOne="yoyo"; LabelTwo=9 }
let listOfRecordsA = [ recordOneA; recordTwoA ]
let listOfRecordsTwoA = [ recordThreeA; recordFourA ]

let capturedResultSeventeen = List.map2 (fun x y -> if x.LabelOne = y.LabelOne then y else x) listOfRecordsA listOfRecordsTwoA
// operates in a pairwise iterative manner (meaning list length equality is a requirement)

// Test 13: Higher-order functions as replacement expressions for loops: 
// bad
let printRandomNumbersUntilMatched matchValue maxValue =
    let mutable continueLooping = true  // another mutable value
    let randomNumberGenerator = new System.Random()
    while continueLooping do
        // Generate a random number between 1 and maxValue.
        let rand = randomNumberGenerator.Next(maxValue)
        printf "%d " rand
        if rand = matchValue then
            printfn "\nFound a %d!" matchValue
            continueLooping <- false

// much better
let printRandomNumbersUntilMatched0 matchValue maxValue =
    let randomNumberGenerator = new System.Random()
    let sequenceGenerator _ = randomNumberGenerator.Next(maxValue)
    let isNotMatch = (<>) matchValue

    //create and process the sequence of rands
    Seq.initInfinite sequenceGenerator
    |> Seq.takeWhile isNotMatch
    |> Seq.iter (printf "%d ")

    // done
    printfn "\nFound a %d!" matchValue

//test
printRandomNumbersUntilMatched0 10 50

// Test 14

let push tail head = head::tail

let pop content =
    match content with
    | head::tail -> [[head]; tail]
    | [] -> failwith "Stack underflow"

let pop2 (content:int list array) index =
    match content[index] with
    | head::tail -> [|[head]; tail|]
    | [] -> failwith "Stack underflow"

//
let push2 (content:int list array) index =
    match index with 
    | 0 -> content[1][0]::content[0]
    | 2 -> content[1][0]::content[2]
    | _ -> failwith "Index not accepted"
//

//let mutable previousCells :int list = []
let mutable remainingCells = [1;2;3;4;5]
let mutable currentCell = 0

let mutable somet = [|[]; [0]; [1;2]|]
let foo = pop2 somet 2
somet[1] <- foo[0]
somet[2] <- foo[1]

let foo2 = pop remainingCells
currentCell <- foo2[0][0]
remainingCells <- foo2[1]

printfn "%A" currentCell
printfn "%A" remainingCells

// data and behavior are separate
let soma = [[]; [0]; [1;2]]


// Test 15
// return word count and letter count in a tuple
let wordAndLetterCount (s:string) =
   let words = s.Split [|' '|]
   printfn "%A" words
   let letterCount = words |> Array.sumBy (fun word -> word.Length )
   (words.Length, letterCount)

//test
let catcher = wordAndLetterCount "to be or not to be"


// Test 16: Shows that tuples can be deconstructed in the parameter
let addToTuple amt (x,y,z) = (x+amt,y+amt,z+amt)
let returnedVal = addToTuple 7 (1,2,3)

printf "%s" ((1,2,3).ToString())


//Test 17: Constrution and Deconstruction of single case union types
type EmailAddress = EmailAddress of string

// using the constructor as a function
let someEmailAddr = "a" |> EmailAddress
let someEmailAddrs = ["a"; "b"; "c"] |> List.map EmailAddress

// inline deconstruction
let a' = "a" |> EmailAddress
let (EmailAddress a'') = a'

let addresses =
    ["a"; "b"; "c"]
    |> List.map EmailAddress

let addresses' =
    addresses
    |> List.map (fun (EmailAddress e) -> e)


// Test 18: Handling nested collections.
let getAllCarrierCodesResults () = [[234; 234]; [347]; [128; 903]]

let mutable allCarrierCodes :string list = [] 
for result in getAllCarrierCodesResults () do
    for carrierCode in result do
        allCarrierCodes <- (carrierCode |> string)::allCarrierCodes

// These four provide the same range for the same domain:
// (comparing 2 and 4 is quite interesting; not as ovious by looking at 1 and 3)
let allCodes1 = List.collect (List.map string) (getAllCarrierCodesResults ())  // OOP-style w/collect
let allCodes3 = List.map string (List.concat (getAllCarrierCodesResults ()))  // OOP-style w/concat
let allCodes2 = getAllCarrierCodesResults () |> List.collect (List.map string)  // FP-style w/collect
let allCodes4 = getAllCarrierCodesResults () |> List.concat |> List.map string  // FP-style w/concat

// Change type of data in a sequence/list:
let myListOne = [ 1; 2; 3; 4; 5 ]  // int list
let myNewListOne = myListOne |> List.map string  // string list
let myNewestListOne = myNewListOne |> List.map int  // int list

// Change type of collection surronding data:
let myListTwo = [ 1; 2; 3; 4; 5 ]  // int list
let myNewSeqTwo = myListTwo |> List.toSeq  // int seq

// "Seq.collect id" has the same domain and range (I/O) as "Seq.concat":
let dummyData = [[234; 234]; [347]; [128; 903]]
let allCodes0a = dummyData |> List.collect id  // this...
let allCodes1a = dummyData |> List.concat // ...& have the same I/O

// Seq.collect has the same domain and range (I/O) as Seq.map and Seq.concat combined:
let allCodes0 = dummyData |> List.collect id  // this...
let allCodes2a = dummyData |> List.map id |> List.concat // ...& have the same I/O


// Test 19: 
let test256757 = [[1;2;3;4;5]; [6]; [7;8;9;10]]

let someTestFunction1 cellList (cMId:int list list) =
    let currentCell = (cellList |> List.filter (fun x -> x = cMId[1][0]))[0]
    let prev = cellList |> List.filter (fun x -> x < cMId[1][0])
    let remaining = cellList |> List.filter (fun x -> x > cMId[1][0])
    [prev; [currentCell]; remaining]

let arg1 = [1;2;3;4;5;6;7;8;9;10]
let arg2 = [[23;42;13;14;50]; [6]; [237;84;49;140]]
let arg3 = [23;42;13;14;50;6;237;84;49;140]
let resultted = someTestFunction1 arg1 arg2
let resultted' = someTestFunction1 arg3 arg2


// Test 20: How 'if' works with returned values compared to match expressions returned values
let myFunction booleran =
    if booleran then 1 else
    let myGoal = 2
    myGoal
    // :goal here was to allow for remaining code to have the same indent amt as the
    // previous code while maintaining the same functionality as match expression (success)

let mySecondFunction booleran =
    match booleran with
    | true  -> 1
    | false ->
        let myGoal = 2
        myGoal 

myFunction true  // 1
myFunction false  // 2
mySecondFunction true  // 1
mySecondFunction false  // 2