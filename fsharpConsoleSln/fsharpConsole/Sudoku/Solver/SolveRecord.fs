module SolveRecord

type T = { cM: CellManager.T; sS: SolverState.T }

let create cellList =
    let cM = CellManager.create cellList
    let sS = SolverState.create 0 1 true
    { cM=cM; sS=sS }

let replaceCellList sR cellList = { sR with cM.CellList=cellList }