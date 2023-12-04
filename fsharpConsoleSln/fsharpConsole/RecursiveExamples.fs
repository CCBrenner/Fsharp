module RecursiveExamples

// Example 1: 

// iterative version (more easy to read; preferred):
let productsList =
    [ for x in 2..4 do
         for y in 2..4 do
            yield x*y ]

// recursive version:
let product =  // not a function; is a list
  let rec outer(n1) =  // needs to be recursive bc of last line of its func (incrementing).
    let rec nested(n2) =  // also is recursive
      if n2 > 4 then [] else (n1 * n2)::(nested(n2 + 1))  // the recursive portion is being done to the end of the list
    if n1 > 4 then [] else nested(2) @ outer(n1 + 1)
  outer(2)  // only the starting point; 'outer' will be called more times before the recursive function ends

// both produce: int list = [4; 6; 8; 6; 9; 12; 8; 12; 16]

// also recursive, but this time with an accumulator:
let rec outer n1 acc = 
    let rec nested n2 acc = 
        if n2 > 4 then acc else nested (n2 + 1) ((n1 * n2)::acc)  //  the recursive result is being appended to the front of the list
    if n1 > 4 then acc else outer (n1 + 1) (nested 2 acc)
let productB = outer 2 [] |> List.rev  // [4; 6; 8; 6; 9; 12; 8; 12; 16]

// another way to write the above code as one value generating code block and no external functions (tail-call recursion):
let productC = 
    let rec outer n1 acc = 
        let rec nested n2 acc = 
            if n2 > 4 then acc else nested (n2 + 1) ((n1 * n2)::acc)
        if n1 > 4 then acc else outer (n1 + 1) (nested 2 acc)
    outer 2 [] |> List.rev  // [4; 6; 8; 6; 9; 12; 8; 12; 16]
// In summary, it is better to use double-nested loops for the practical reason of readability, but it is still good
// to understand how these recursive functions achieve what they do. (They are essentially doing the same thing, except
// that the second one is taking the accuulator as a parameter while the first one is creating the accumulator inside of itself.)