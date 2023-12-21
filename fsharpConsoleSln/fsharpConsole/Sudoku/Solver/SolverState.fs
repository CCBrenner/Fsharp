module SolverState

type T = 
    {   Candidate: int
        Counter: int
        IsSolvable: bool
        NoMoreCandidates: bool  }

let create candidate counter isSolvable noMoreCandidates  =
    {   Candidate=candidate
        Counter=counter
        IsSolvable=isSolvable
        NoMoreCandidates=noMoreCandidates   }
