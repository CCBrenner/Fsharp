module Solver

open Cell

type T = { cM: CellManager.T; sS: SolverState.T }

let create cellList =
    let cM =
        CellManager.create cellList
    let sS =
        SolverState.create 0 1 true false
    { cM=cM; sS=sS }

let replaceCellList sR cellList = { sR with cM.CellList=cellList }

let getCurrentCell sR = sR.cM |> CellManager.getCurrentCell

let getNextCandidate sR =
    // working on the current cell
    let tempNew = (CellManager.getCurrentCell sR.cM).Values
                  |> Array.filter (fun x -> x<>ProjectVals.defaultCellValue)
    match tempNew with
    | [||] -> { sR with sS.Candidate=ProjectVals.defaultCellValue }
    | _    -> { sR with sS.Candidate=tempNew[0] }

let applyToCellListOfSolveRecord f sR =
    sR.cM.CellList |> f |> replaceCellList sR

let updateCandidates = applyToCellListOfSolveRecord Candidates.updateCandidatesFromCellList

let setValueStatusOfValueGivenToCellsWithNonDefaultValue =
    applyToCellListOfSolveRecord Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue

let removeCandidates = applyToCellListOfSolveRecord Candidates.removeCandidates

let goToNextCell sR=
    let cM = sR.cM |> CellManager.goToNextCell
    { sR with cM=cM }

let checkIsSolvable sR =
    match (sR.cM.Current = 0 && sR.sS.Candidate = 0) with
    | true -> { sR with sS.IsSolvable=false }
    | _    -> sR

let checkNoMoreCandidates sR =
    // *intended to be used after checkIsSolvable:
    match sR.cM.Current with
    | 0 ->
        // goBackToLastCellWithUntriedCandidates
        { sR with sS.NoMoreCandidates=true }
    | _ ->
        sR

let setExpectedValue sR =
    let currentCell= sR |> getCurrentCell
    let cM =
        { currentCell with Value=sR.sS.Candidate; ValueStatus=ValueStatus.Confirmed }
        |> CellManager.updateCurrentCell sR.cM
    { sR with cM=cM }

let addTriedCandidateToCurrentCell sR =
    let currentCell= sR |> getCurrentCell
    let triedCandidates = 
        currentCell.TriedCandidates
        |> ProjectVals.replaceElementInList sR.sS.Candidate sR.sS.Candidate
    let cM =
        { currentCell with TriedCandidates=triedCandidates }
        |> CellManager.updateCurrentCell sR.cM
    { sR with cM=cM }

let incrementLoopCounter sR = { sR with sS.Counter=sR.sS.Counter+1 }

let solve cellList =

    let sR = cellList |> create |> setValueStatusOfValueGivenToCellsWithNonDefaultValue

    let rec whileLoop sR =

        let sR' = sR |> getNextCandidate |> checkIsSolvable

        if not sR'.sS.IsSolvable then sR else  // returns sR

        let sR'' = sR' |> checkNoMoreCandidates

        if not sR''.sS.NoMoreCandidates then whileLoop sR' else  // returns result of "whileLoop sR'"

        let sR''' = sR'' |> setExpectedValue

        if sR'''.cM.Current = 81 then sR'' else

        let sR'''' =
            sR''' |> addTriedCandidateToCurrentCell
            |> updateCandidates
            |> goToNextCell
            |> incrementLoopCounter

        whileLoop sR''''

    whileLoop sR

(*
let performOnCellManager f solveRec =
    let cellManager = fst solveRec
    let newCM = f cellManager
    newCM, (snd solveRec)

let performOnSolverState f solveRec =
    let solverState = snd solveRec
    let newSolverState = f solverState
    (fst solveRec), newSolverState
*)
(*
let solveTwo cellList =

    // Step through each cell to try a candidate, if okay, proceed, if not, go back
    // Data-Behavior separation
    
    // Remaining and Previous
    let mutable previousCells = []
    let mutable remainingCells = cellList
    let mutable currentCell = Option.None()
    (currentCell, remainingCells) = pop remainingCells

    // Setup based on Puzzle:


    // Components:
    // 0. Setup (cells=Given;)
    // 1. CellMgmt
    // 2. Checks on Solvablity
    // 3. Candidate Update Services




    _puzzle = puzzle;
    _puzzle.Cells.Reverse();  // reversed (for creating RemainingCells stack)
    foreach (var cell in _puzzle.Cells)
    {
        if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
        {
            RemainingCells.Push(cell);
        }
    }
    _puzzle.Cells.Reverse();  // reversed back to original order

    CurrentCell = RemainingCells.Pop();

    // End setup.

    int candidate;

    int counter = 1;

    SolveHasStarted = true;

    MarkCellsWithNonZeroValuesAsGiven();

    //ConsoleRender.RenderMatrixWithMetaData(Puzzle);

    //Timer.Start();

    _puzzle.PerformCandidateElimination();

    while (true)
    {
        // get the next candidate to try
        candidate = CurrentCell.GetNextCandidate();

        // if CurrentCell is the CellId 1 and there are no other candidates to try, exit
        if (candidate == 0 && PreviousCells.Count == 0)
        {
            PuzzleIsSolvable = false;
            break;
        }

        // if there are no other candidates to try, then backtrack to previous cell
        if (candidate == 0)
        {
            // Use only one of the following at a single time:
            //GoBackToLastCellWithUntriedCandidatesIterative();
            GoBackToLastCellWithUntriedCandidatesRecursive();  // <= Optimize with F# (somehow)
            continue;
        }

        // assign return value as value of current cell 
        CurrentCell.SetExpectedValue(candidate);
        if (RemainingCells.Count == 0) break;

        // also assign same return value to TriedCandidates stack in Cell api
        CurrentCell.AddTriedCandidate(candidate);

        // rehydrate candidates for unconfirmed cells and perform elimination based on new value assignment
        _puzzle.UpdateCandidates();

        // assign CurrentCell to PreviousCells stack in Puzzle api
        GoToNextCell();

        //RaiseEventOnSolverLoopIteration();

        if (counter % 100 == 0) Console.WriteLine(counter);

        counter++;
    }

    //Timer.Stop();

    Console.WriteLine(counter);

    // return true if Puzzle is solved; false if could not be solved
    return PuzzleIsSolvable;
*)
(*
let solve cellList =
    // differing reqs:
    // 1. Need to return all values needed outside of function.
    // 2. Need all values used in function to be injected or created in function.

    let solveRec = 
        cellList
        |> Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue
        |> Candidates.removeCandidates
        |> createSolveRecord

    // recursive function
    // piped results, passing tuples where necessary (cellList is always a value)

    //while (true)
    
        // get next available candidate in cell
        // check for puzzleIsSolvable and change value accordingly
            // set to false (and have all following functions pass the data through)
        // check for noMoreCandidatesInCellToTry
            // assign cell.Value to zero (including making cell.ValueStatus=Confirmed)
            // reset triedCandidates of Cell to empty
            // move back to last cells with candidates to try

        // call function again (recursively)


        // get the next candidate to try
        //candidate = CurrentCell.GetNextCandidate();
        getNextCandidate solveRec

        // if CurrentCell is the CellId 1 and there are no other candidates to try, exit
        if (candidate == 0 && PreviousCells.Count == 0)
        {
            PuzzleIsSolvable = false;
            break;
        }

        // if there are no other candidates to try, then backtrack to previous cell
        if (candidate == 0)
        {
            // Use only one of the following at a single time:
            //GoBackToLastCellWithUntriedCandidates();  // <= Optimize with F# (somehow)
            goToLastCellWithUntriedCandidates
            continue;
        }

        // assign return value as value of current cell 
        CurrentCell.SetExpectedValue(candidate);
        if (RemainingCells.Count == 0) break;

        // also assign same return value to TriedCandidates stack in Cell api
        //CurrentCell.AddTriedCandidate(candidate);
        addTriedCandidateToCurrentCell 

        // rehydrate candidates for unconfirmed cells and perform elimination based on new value assignment
        //UpdateCandidates();
        rehydrateCandidatesOfUnconfirmedCells
        removeCandidates

        // assign CurrentCell to PreviousCells stack in Puzzle api
        //GoToNextCell();
        goToNextCell

        //counter++;
        incrementCounter
    //

    Console.WriteLine(counter);
    // return true if Puzzle is solved; false if could not be solved
    return PuzzleIsSolvable;
    *)