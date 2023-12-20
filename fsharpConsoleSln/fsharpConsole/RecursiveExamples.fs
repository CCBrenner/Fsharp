module RecursiveExamples

// Example 1: 

// iterative version (more easy to read; preferred):
let productsList =
    [ for x in 2..4 do
         for y in 2..4 do
            yield x*y ]

// recursive version:
let product =  // not a function; is a list
    let rec outer(n1) =  // needs to be recursive bc of last line of its func (incrementing).
        let rec nested(n2) =  // also is recursive
            if n2 > 4 then [] else (n1 * n2)::(nested(n2 + 1))  // the recursive portion is being done to the end of the list
        if n1 > 4 then [] else nested(2) @ outer(n1 + 1)
    outer 2  // only the starting point; 'outer' will be called more times before the recursive function ends

// both produce: int list = [4; 6; 8; 6; 9; 12; 8; 12; 16]

// another way to write this with match expressions (easier to read):
let productE =
    let rec outer n1 = 
        let rec nested n2 = 
            match n2 > 4 with
            | true  -> []
            | false -> (n1 * n2)::(nested (n2 + 1))
        match n1 > 4 with
        | true  -> []
        | false -> nested 2 @ outer (n1 + 1)
    outer 2

// also recursive, but this time with an accumulator:
let rec outer n1 acc = 
    let rec nested n2 acc = 
        if n2 > 4 then acc else nested (n2 + 1) ((n1 * n2)::acc)  //  the recursive result is being appended to the front of the list
    if n1 > 4 then acc else outer (n1 + 1) (nested 2 acc)
let productB = outer 2 [] |> List.rev  // [4; 6; 8; 6; 9; 12; 8; 12; 16]

// another way to write the above code as one value generating code block and no external functions (tail-call recursion):
let productC = 
    let rec outer n1 acc = 
        let rec nested n2 acc = 
            if n2 > 4 then acc else nested (n2 + 1) ((n1 * n2)::acc)
        if n1 > 4 then acc else outer (n1 + 1) (nested 2 acc)
    outer 2 [] |> List.rev  // [4; 6; 8; 6; 9; 12; 8; 12; 16]
// In summary, it is better to use double-nested loops for the practical reason of readability, but it is still good
// to understand how these recursive functions achieve what they do. (They are essentially doing the same thing, except
// that the second one is taking the accuulator as a parameter while the first one is creating the accumulator inside of itself.)

// another way to write this using match expressions instead of if statements (easier to read, in my opinion):
let productD = 
    let rec outer n1 acc = 
        let rec nested n2 acc = 
            match n2 > 4 with
            | true  -> acc
            | false -> nested (n2 + 1) ((n1 * n2)::acc)
        match n1 > 4 with
        | true  -> acc
        | false -> outer (n1 + 1) (nested 2 acc)
    outer 2 [] |> List.rev  // [4; 6; 8; 6; 9; 12; 8; 12; 16]



// Playground: 
// Do the recursive equivalent of a while loop:

// #1
let myFunction a =
    let rec recF x =
        match x with
        | 6 -> x
        | _ -> 
            let newNum = (+) x 1
            recF newNum
    recF a  // tail call
let result88 = myFunction 0

// #2, V1
let myFunction0 a =
    let rec recF boolean x =
        match boolean with
        | false -> x
        | true -> 
            let newX = x + 3
            match newX with
            | a when a % 17 = 0 -> recF false newX
            | _ -> recF true newX
    recF true a  // tail call
let result87 = myFunction0 0

// #2, V2
let findLCM increment modulus start =
    let rec inner x =
        let newX = x + increment

        // This acts like a while loop check:
        match newX with
        | a when a % modulus = 0 -> newX
        | _                      -> inner newX
    inner start  // tail call
let result86 = findLCM 4 57 0

// #3
let goBackToLastCellWithCandidates () =
    let rec inner x =

        // when a cell has no more candidates to try when calling GetNextCandidate:
        // clear the TriedCandidates stack of CurrentCell until stack count == 0
        int previousValue = CurrentCell.Value;

        CurrentCell.ResetTriedCandidates();
        CurrentCell.ResetValue();

        if (previousValue != CurrentCell.Value)
        {
            _puzzle.Ledger.RecordNewTxn(CurrentCell.Id, 0, previousValue, CurrentCell.Value);
        }

        // push cell unto RemainingCells stack && Pop cell from PreviousCells stack && Assign popped cell from stack to CurrentCell property
        MoveToPreviousCell();

        // since Value becomes free, add that value to all respective cells that would have it as a candidate
        // and then eliminate all non possible candidates
        _puzzle.UpdateCandidates();

        // get the next candidate to try
        int candidate = CurrentCell.GetNextCandidate();
        let candidate = Candidates.getNext

        match sR with  // sR = SolveRecord
        // if CurrentCell is the CellId 1 and htere are no other candidates to try, exit
        | candidate=0 and PreviousCells.Length = 0 -> 
            { sR with sR.SolverState.IsSolvable=false }
        // if there are no other candidates to try, then backtrack to previous cell
        | candidate = 0 -> inner sR
        | _ -> sR
    inner _

