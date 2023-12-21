module Cell

open System.Linq

type ValueStatus =
    | Given
    | Unconfirmed
    | Confirmed

type T = 
    { Id: int
      RowId: int
      ColId: int
      BlockId: int
      //BlockRowId: int
      //BlockColId: int
      Value: int
      ValueStatus: ValueStatus
      Values: int array
      TriedCandidates: int list}

// constructor:
let create id =
    let getIdTemplate modNum operation id = (operation (id-1) modNum)+1
    let getIdNine = getIdTemplate 9
    let getIdThreeDiv = getIdTemplate 3 (/)

    let rowId = getIdNine (/) id
    let colId = getIdNine (%) id
    let blockColId = getIdThreeDiv colId
    //let blockRowId = getIdThreeDiv rowId
    let blockId = ((rowId-1)/3*3)+blockColId  // "../3*3" eliminates the remainder

    let cell =
        { Id=id
          RowId=rowId
          ColId=colId
          BlockId=blockId
          //BlockRowId=blockRowId
          //BlockColId=blockColId
          Value=ProjectVals.defaultCellValue
          ValueStatus=Unconfirmed
          Values=[|0..9|]
          TriedCandidates=[] }  // Values[0] is only a placeholder for indexing purposes.
    cell

let createCellList () = [ for i in 1 .. 81 -> create i ]

let updateValue cell newVal =
    { cell with Value=newVal }
        
let eliminatePossibility cell valIndex =
    cell.Values[valIndex] = ProjectVals.defaultCellValue

let resetValues cell =
    let resetArr = [| for i in 0 .. 9 -> i |]
    { cell with Values=resetArr }

let resetTriedCandidates cell = { cell with TriedCandidates=[] }

let EliminateCandidatesForGivenAndConfirmedCells =
    ()
    
// GET functions:
let getCellsOfRow rowId = List.filter (fun x -> x.RowId=rowId)
let getCellsOfColumn colId = List.filter (fun x -> x.ColId=colId)
let getCellsOfBlock blockId = List.filter (fun x -> x.BlockId=blockId)
//let getCellsOfBlockRow blockRowId = List.filter (fun x -> x.BlockRowId = blockRowId)
//let getCellsOfBlockColumn blockColId = List.filter (fun x -> x.BlockColId = blockColId)

let private rowIdMatch rowId = List.filter (fun x -> x.RowId=rowId)
let private colIdMatch colId = List.filter (fun x -> x.ColId=colId)
let getCellViaMatrix (rowId, colId) cellList = (cellList |> rowIdMatch rowId |> colIdMatch colId).FirstOrDefault()

let setValueStatusOfValueGivenToCellsWithNonDefaultValue cellList =
    // (used only at the beginning of the solve algorithm & with tests)
    let localFunc x =
        if x.Value<>ProjectVals.defaultCellValue
        then { x with ValueStatus=ValueStatus.Given }
        else x
    cellList |> List.map localFunc

let getCandidates cell =
    let candidates =
        cell.Values 
        |> Array.toList 
        |> List.distinct 
        |> List.filter (fun x -> x<>0)
    candidates

(*  Not currently used (is complete); can be uncommented if use for BlockRow and BlockColumn become apparent.
let private blockFuncTemplate rowOrCol blockRowId =
    match blockRowId with
    | 1 -> (1, 2, 3)
    | 2 -> (4, 5, 6)
    | 3 -> (7, 8, 9)
    | _ -> failwith $"block{ rowOrCol }Id out of bounds."

let getBlocksOfBlockRow = blockFuncTemplate "Row"
let getBlocksOfBlockCol blockColId =
    match blockColId with
    | 1 -> (1, 4, 7)
    | 2 -> (2, 5, 8)
    | 3 -> (3, 6, 9)
    | _ -> failwith "blockColId out of bounds."

let getRowsOfBlockRow = blockFuncTemplate "Row"
let getColsOfBlockCol = blockFuncTemplate "Col"
*)
