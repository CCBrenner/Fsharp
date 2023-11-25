module Cell

type ValueStatus =
    | Given
    | Unconfirmed
    | Confirmed

type Cell = 
    { Id: int
      RowId: int
      ColId: int
      BlockId: int
      BlockRowId: int
      BlockColId: int
      Value: int
      ValueStatus: ValueStatus
      Values: array<int> }

let defaultCellValue = 0

// constructor:
let create id =
    let values = [| for i in 0 .. 9 -> i |]
    let rowId = id/9+1
    let colId = id%9
    let blockId = ((colId-1)/3)*(rowId/9)
    let blockRowId = (blockId)/3+1
    let blockColId = blockId%3
    let cell =
        { Id=id
          RowId=rowId
          ColId=colId
          BlockId=blockId
          BlockRowId=blockRowId
          BlockColId=blockColId
          Value=defaultCellValue
          ValueStatus=Unconfirmed
          Values=values }
    cell

let updateValue cell newVal =
    { cell with Value=newVal }
        
let eliminatePossibility cell valIndex =
    cell.Values[valIndex] = defaultCellValue

let resetValues cell =
    let resetArr = [| for i in 0 .. 9 -> i |]
    { cell with Values=resetArr }

let EliminateCandidatesForGivenAndConfirmedCells =
    ()
    
//let returnCellsFromColumn cellList colNum =
//    List.filter (fun x -> x.Id % 9 = colNum) cellList

//let returnCellsFromRow cellList rowNum =
//    List.filter (fun x -> x.Id > (rowNum-1)*9 && x.Id < rowNum*9+1) cellList