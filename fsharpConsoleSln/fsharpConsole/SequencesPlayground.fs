module SequencesPlayground

let seqEmpty = Seq.empty  // empty sequence
let seqOne = Seq.singleton 10  // a sequence of 1 element

let seqFirst5MultiplesOf10 = Seq.init 5 (fun n -> n * 10)
seqFirst5MultiplesOf10 |> Seq.iter (fun elem -> printf "%d " elem)
// Note: unlike List.init, Seq.init does not create the sequence right away.
// You can see this in the value signature, where the Seq does not have values listed while the List does:
let seqInitExample = Seq.init 5 (fun n -> n)
seqInitExample |> printfn "%A"  
let listInitExample = List.init 5 (fun n -> n)
listInitExample |> printfn "%A"  

// Convert an array to a sequence by using a cast:
let seqFromArray1 = [| 1 .. 10 |] :> seq<int>
// Convert an array to a sequence by using Seq.ofArray:
let seqFromArray2 = [| 1 .. 10 |] |> Seq.ofArray

// Can do the same for List:
// Convert a list to a sequence by using a cast:
let seqFromList1 = [ 1 .. 10 ] :> seq<int>
// Convert a list to a sequence by using Seq.ofArray:
let seqFromList2 = [ 1 .. 10 ] |> Seq.ofList

(*
// Seq.cast - not really sure how this works yet...
let arr = ResizeArray<int>(10)
for i in 1 .. 10 do
    arr.Add(10)
let seqCast = Seq.cast arr

// Functional form:
let arr = ResizeArray<int>(10)
[ 1..10 ] |> Seq.iter (fun x -> arr.Add(10))
let seqCast0 = Seq.cast arr
*)

// Seq.initInfinite:
// Generates a theoretical infinite seq based on the input function (the infinite sequnce isn't actually created & passed)
let seqInfinite =
    Seq.initInfinite (fun index ->
        let n = float (index + 1)
        1.0 / (n * n * (if ((index + 1) % 2 = 0) then 1.0 else -1.0)))

printfn "%A" seqInfinite

// another example:
let printRandomNumbersUntilMatched matchValue maxValue =
    let randomNumberGenerator = new System.Random()
    (fun x -> randomNumberGenerator.Next(maxValue))
        |> Seq.initInfinite
        |> Seq.takeWhile ((<>) matchValue)
        |> Seq.iter (printf "%d ")
    printfn "\nFound a %d!" matchValue

let rndNumResult = printRandomNumbersUntilMatched 10 20


// Seq.takeWhile:
let listOfLetters = ["a"; "bbb"; "cc"; "d"; "eeee"; "f"; "ee"]
let letterConstraint (x:string) = x.Length < 4
let newListOfLetters = listOfLetters |> Seq.takeWhile letterConstraint
newListOfLetters |> Seq.iter (printfn "%s")


// Seq.unfold
let seq1 =
    0 // Initial state
    |> Seq.unfold (fun state ->
        if (state > 20) then
            None
        else
            Some(state, state + 1))

printfn "The sequence seq1 contains numbers from 0 to 20."
//for x in seq1 do printf "%d " x  // OOP
seq1 |> Seq.iter (printf "%d ")  // FP

let fib0 =
    (0, 1)
    |> Seq.unfold (fun state ->
        let cur, next = state
        if cur < 0 then  // overflow
            None
        else
            let next' = cur + next
            let state' = next, next'
            Some (cur, state') )

let fib1 =
    (0, 1)
    |> Seq.unfold (fun state ->
        let cur, next = state
        match cur with
        | x when x < 0 -> None  // use of 'when' here is called a "guard"
        | _ ->
            let next' = cur + next
            let state' = next, next'  // , = tuple
            Some (cur, state') )  // = int in a tuple with a tuple

printfn "\nThe sequence fib contains Fibonacci numbers."
//for x in fib do printf "%d " x // OOP
fib0 |> Seq.iter (printf "%d ")  // FP
fib1 |> Seq.iter (printf "%d ")  // FP

// Could easily turn simple value into a function value (enabling more values to be entered as input):
let fib2 =
    Seq.unfold (fun state ->
        let cur, next = state
        match cur with
        | x when x < 0 -> None  // use of 'when' here is called a "guard"
        | _ ->
            let next' = cur + next
            let state' = next, next'  // , = tuple
            Some (cur, state') )  // = int in a tuple with a tuple

(0, 1) |> fib2 |> Seq.iter (printf "%d ")



// generateInfiniteSequence generates sequences of floating point
// numbers. The sequences generated are computed from the fDenominator
// function, which has the type (int -> float) and computes the
// denominator of each term in the sequence from the index of that
// term. The isAlternating parameter is true if the sequence has
// alternating signs.
let generateInfiniteSequence fDenominator isAlternating =
    match isAlternating with
    | true  -> Seq.initInfinite (fun index ->
        1.0 /(fDenominator index) * (if (index % 2 = 0) then -1.0 else 1.0))
    | false -> Seq.initInfinite (fun index ->
        1.0 /(fDenominator index))

// The harmonic alternating series is like the harmonic series
// except that it has alternating signs.
let harmonicAlternatingSeries = generateInfiniteSequence (fun index -> float index) true

// This is the series of reciprocals of the odd numbers.
let oddNumberSeries = generateInfiniteSequence (fun index -> float (2 * index - 1)) true

// This is the series of recipocals of the squares.
let squaresSeries = generateInfiniteSequence (fun index -> float (index * index)) false

// This function sums a sequence, up to the specified number of terms.
let sumSeq length sequence =
    (0, 0.0)
    |> Seq.unfold (fun state ->
        let subtotal = snd state + Seq.item (fst state + 1) sequence
        match (fst state) with
        | x when x >= length -> None
        | _                  -> Some (subtotal, (fst state + 1, subtotal)))
        //if (fst state >= length) then None
        //else Some(subtotal, (fst state + 1, subtotal)))

// This function sums an infinite sequence up to a given value
// for the difference (epsilon) between subsequent terms,
// up to a maximum number of terms, whichever is reached first.
let infiniteSum infiniteSeq epsilon maxIteration =
    infiniteSeq
    |> sumSeq maxIteration
    |> Seq.pairwise
    |> Seq.takeWhile (fun elem -> abs (snd elem - fst elem) > epsilon)
    |> List.ofSeq
    |> List.rev
    |> List.head
    |> snd

// Compute the sums for three sequences that converge, and compare
// the sums to the expected theoretical values.
let result1 = infiniteSum harmonicAlternatingSeries 0.00001 100000
printfn "Result: %f  ln2: %f" result1 (log 2.0)

// commented 
//let pi = Math.PI  
//let result2 = infiniteSum oddNumberSeries 0.00001 10000
//printfn "Result: %f pi/4: %f" result2 (pi/4.0)

// Because this is not an alternating series, a much smaller epsilon
// value and more terms are needed to obtain an accurate result.
//let result3 = infiniteSum squaresSeries 0.0000001 1000000
//printfn "Result: %f pi*pi/6: %f" result3 (pi*pi/6.0)

