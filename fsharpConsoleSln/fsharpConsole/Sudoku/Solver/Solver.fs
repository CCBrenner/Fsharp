module Solver

let getNextCandidate (sR:SolveRecord.T) =
    // working on the current cell
    let tempNew = (CellManager.getCurrentCell sR.cM).Values
                  |> Array.filter (fun x -> x<>ProjectVals.defaultCellValue)
    match tempNew with
    | [||] -> { sR with sS.Candidate=ProjectVals.defaultCellValue }
    | _    -> { sR with sS.Candidate=tempNew[0] }

let updateCandidatesFromCellList cellList =
    cellList
    |> Candidates.rehydrateCandidatesOfUnconfirmedCells
    |> Candidates.removeCandidates

let applyToCellListOfSolveRecord f (sR:SolveRecord.T) =
    sR.cM.CellList |> f |> SolveRecord.replaceCellList sR

let updateCandidates = applyToCellListOfSolveRecord updateCandidatesFromCellList

let setValueStatusOfValueGivenToCellsWithNonDefaultValue =
    applyToCellListOfSolveRecord Cell.setValueStatusOfValueGivenToCellsWithNonDefaultValue

let removeCandidates = applyToCellListOfSolveRecord Candidates.removeCandidates

//let continueIf f x =

let solveThree cellList =
    let sR = cellList |> SolveRecord.create |> setValueStatusOfValueGivenToCellsWithNonDefaultValue

    let rec whileLoop (x:SolveRecord) =
        let sR = 
            x |> getNextCandidate
            |> checkIsSolvable whileLoop
        if not sR.IsSolvable then sR else  // returns sR
        let sR' =
            sR |> checkNoMoreCandidates whileLoop
        if not sR'.NoMoreCandidates then whileLoop sR' else  // returns result of "whileLoop sR'"
        let sR'' =
            sR' |> setExpectedValue
            |> checkAnyCellsRemaining
            |> addTriedCandidateToCurrentCell
            |> updateCandidates
            |> CellManager.goToNextCell
            |> incrementLoopCounter
        ()
        //match result with
        //| 
        //| _ -> sR
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