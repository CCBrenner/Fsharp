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

let getBaseId modNum operation id = (operation (id-1) modNum)+1
let getCellLvlId = getBaseId 9
let getBlockLvlId = getBaseId 3

// constructor:
let create id =
    let values = [| for i in 0 .. 9 -> i |]
    let rowId = getCellLvlId (/) id  //let rowId = (id-1)/9+1
    let colId = getCellLvlId (%) id  //let colId = (id-1)%9+1
    let blockId = ((rowId-1)/3*3)+((colId-1)/3)+1  // "/3*3" eliminates the remainder
    let blockRowId = getBlockLvlId (/) blockId  // (blockId-1)/3+1
    let blockColId = getBlockLvlId (%) blockId  // (blockId-1)%3+1
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