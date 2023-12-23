module CellManager

open Cell
open System.Linq

let pop content =
    match content with
    | head::tail -> (head, tail)
    | [] -> failwith "Stack underflow"

type T = { CellList:Cell.T list; Current:int }

let startingCell = 1

let createWithCurrentCell currentcellId cellList  = { CellList=cellList; Current=currentcellId }

let create = createWithCurrentCell startingCell

let getCell id cellList = (cellList |> List.filter (fun x-> x.Id = id))[0]  // apply Some/None once learned

let getCurrentCell cM = getCell cM.Current cM.CellList

let updateCellInCellList cell cellList = cellList |> List.map (fun x -> if x.Id = cell.Id then cell else x )

let updateCurrentCell cM cell = { cM with CellList=(cM.CellList |> updateCellInCellList cell) }

let goToNextCell cM = { cM with Current=(cM.Current+1)}

let goToPreviousCell cM = 
    let newCM = cM |> getCurrentCell |> resetTriedCandidates |> updateCurrentCell cM
    { newCM with Current=(cM.Current-1) }

let removeCandidates cM =
    let cellList = cM.CellList |> Candidates.removeCandidates
    { cM with CellList=(Candidates.removeCandidates cellList)}
(*
let goBackToLastCellWithUntriedCandidates cM =
    let rec inner x =

        // when a cell has no more candidates to try when calling GetNextCandidate:
        // clear the TriedCandidates stack of CurrentCell until stack count == 0
        int previousValue = CurrentCell.Value;

        CurrentCell.ResetTriedCandidates();
        CurrentCell.ResetValue();

        // push cell unto RemainingCells stack && Pop cell from PreviousCells stack && Assign popped cell from stack to CurrentCell property
        MoveToPreviousCell();

        // since Value becomes free, add that value to all respective cells that would have it as a candidate
        // and then eliminate all non possible candidates
        _puzzle.UpdateCandidates();

        // get the next candidate to try
        int candidate = CurrentCell.GetNextCandidate();
        let candidate = Candidates.getNext

        match sR.sS with  // sR = SolveRecord
        // if CurrentCell is the CellId 1 and htere are no other candidates to try, exit
        | Candidate = 0 && PreviousCells.Length = 0 -> 
            { sR with sR.SolverState.IsSolvable=false }
        // if there are no other candidates to try, then backtrack to previous cell
        | Candidate = 0 -> inner sR
        | _ -> sR
    inner cM
*)
(*
type T = 
    {   Previous: Cell list
        Current: Cell
        Remaining: Cell list  }

let create previous current remaining =
    {   Previous=previous
        Current=current
        Remaining=remaining  }

let createFromCellList cellList =
    let head, tail = pop cellList
    create [] head tail

let createCellManagerFromCellListAndOlderCellManager cellList cM =
    let currentCell = (cellList |> List.filter (fun x -> x.Id = cM.Current.Id))[0]
    let prev = cellList |> List.filter (fun x -> x.Id < cM.Current.Id)
    let remaining = cellList |> List.filter (fun x -> x.Id > cM.Current.Id)
    create prev currentCell remaining

let goToNextCell cM =
    let prev' = cM.Current::cM.Previous  // push
    let current', remaining' = pop cM.Remaining
    create prev' current' remaining'

let goBackToPreviousCell cM =
    let remaining' = cM.Current::cM.Remaining  // push
    let current', prev' = pop cM.Previous
    create prev' current' remaining'

let getCellListFromCellManager cM =
    cM.Previous @ ([cM.Current] @ cM.Remaining)

let removeCandidates cM =
    let cellList = getCellListFromCellManager cM
    let cellList' = Candidates.removeCandidates cellList
    createCellManagerFromCellListAndOlderCellManager cellList' cM
*)