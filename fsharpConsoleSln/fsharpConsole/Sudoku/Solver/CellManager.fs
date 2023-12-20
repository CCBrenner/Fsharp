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