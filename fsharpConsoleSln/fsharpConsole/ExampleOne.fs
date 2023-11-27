module ExampleOne

let square num:int = num * num

// Use of map function and pipelines:
let formatIntList intList = 
    intList 
    |> List.map (fun i  -> i.ToString()) 
    |> String.concat ", " 

let runExample () =

    // Testing out mutability of variables:

    let inputNum = 12
    let mutable thisGuy = 7
    printfn "%d squared is: %d!\nNow this Guy: %i" inputNum (square inputNum) thisGuy

    thisGuy <- thisGuy + 3
    printfn "\nThis Guy: %i" thisGuy

    // Doing the same thing but with immutability:

    let thisGuy = 7
    printfn "\nThis Guy: %i" thisGuy

    let newGuy = thisGuy + 3
    printfn "New Guy: %i" newGuy

    // Interpolation? Yes. :
    printfn $"\n{inputNum} squared is: {square inputNum}!"
    printfn $"Now this Guy: {thisGuy}"

    // Testing what different types render like:

    let testTuplesProducer = [ 0 .. 99 ]
    printfn $"The table of squares from 0 to 99 is:"
    printfn $"{string testTuplesProducer}"
    printfn $"{formatIntList testTuplesProducer}"

    let tuple1 = (1, 2, 3)
    let tuple2 = (1, "Fred", 3.1415)
    printfn $"tuple1: {tuple1}"
    printfn $"tuple2: {tuple2}"

    let rec sumListTailRecHelper accumulator xs =
        match xs with
        | [] -> accumulator
        | y::ys -> sumListTailRecHelper (accumulator+y) ys
    let sumListTailRecursive xs = sumListTailRecHelper 0 xs
    let oneThroughTen = [1 .. 10]
    printfn $"The sum of 1 - 10 is %d{sumListTailRecursive oneThroughTen}"
