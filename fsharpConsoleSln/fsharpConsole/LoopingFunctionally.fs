module LoopingFunctionally

// The best way to avoid loops is to use the built in list and sequence functions instead.
// Almost anything you want to do can be done without using explicit loops.
// And often, as a side benefit, you can avoid mutable values as well.

// Printing something 10 times:
// bad
for i = 1 to 10 do
   printf "%i" i

// much better
[1..10] |> List.iter (printf "%i")


// Summing a List:
// bad
let sum0 list =
    let mutable total = 0    // uh-oh -- mutable value
    for e in list do
        total <- total + e   // update the mutable value
    total                    // return the total

// much better
let sum1 list = List.reduce (+) list

//test
let sumResult1 = sum1 [1..10]
let sumResult0 = sum0 [1..10]  // note: both functions take the same data


// Generating and printing a sequence of numbers:
// bad
let printRandomNumbersUntilMatched0 matchValue maxValue =
    let mutable continueLooping = true  // another mutable value
    let randomNumberGenerator = new System.Random()
    while continueLooping do
        // Generate a random number between 1 and maxValue.
        let rand = randomNumberGenerator.Next(maxValue)
        if rand = matchValue then
            printfn "\nFound a %d!" matchValue
            continueLooping <- false

// much better
let printRandomNumbersUntilMatched1 matchValue maxValue =
    let randomNumberGenerator = new System.Random()  // same
    let sequenceGenerator _ = randomNumberGenerator.Next(maxValue)  // new
    let isNotMatch = (<>) matchValue  // new
    // no mutable value or mutation statement
    // no while loop

    //create and process the sequence of rands
    Seq.initInfinite sequenceGenerator
    |> Seq.takeWhile isNotMatch
    |> Seq.iter (printf "%d ")

    // done
    printfn "\nFound a %d!" matchValue

//test
let rndNumResult1 = printRandomNumbersUntilMatched1 10 20
let rndNumResult0 = printRandomNumbersUntilMatched0 10 20  // takes the same data

// same better solution, only different:
let printRandomNumbersUntilMatched2 matchValue maxValue =
    let randomNumberGenerator = new System.Random()
    Seq.initInfinite (fun x -> randomNumberGenerator.Next(maxValue))
        |> Seq.takeWhile ((<>) matchValue)
        |> Seq.iter (printf "%d ")
    printfn "\nFound a %d!" matchValue

let rndNumResult2 = printRandomNumbersUntilMatched2 10 20
