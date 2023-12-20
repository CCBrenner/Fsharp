module SolverState

type T = 
    {   Candidate: int
        Counter: int
        IsSolvable: bool  }

let create candidate counter isSolvable =
    {   Candidate=candidate
        Counter=counter
        IsSolvable=isSolvable   }
