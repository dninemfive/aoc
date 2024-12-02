# Advent of Code 2024 Day 2 Solution
For part 2 of this problem, i wrote some notes, including trying some proofs to analyze why my solution was not working. These are included here out of interest:
> ways it can be unsafe with only one number:
>
> `A`. one number is 0
>
> `B`. one number is > 3
>
> `C`. one number is < -3
>
> each are mutually exclusive (by contradiction: if not mutually exclusive, more than one number would be unsafe)
> 
> another condition:
> - one number violates the monotonicity criterion (has the opposite sign from the rest of the numbers)
> This is not mutually exclusive with the above, but if it were true for a number where one of the above were false then there would be at least two unsafe numbers
> 
> Therefore, i reason that once you sort the deltas, the invalid value _must_ be on one of the ends.
> 
> By contradiction:
>
> `1`. assume that a number *N* violates the criteria.
>
> `2`. assume that *N* is not an extremum of the set of deltas.
>
> `3`. if *N* is 0 and !`2`, this implies there is another number *M* which violates the monotonicity criterion.
>
> `4`. if *N* > 3 and !`2`, this implies there is another number *M* which is also greater than 3.
>
> `5`. if *N* < -3 and !`2`, this implies there is another number *M* which is also less than -3.
>
> Therefore, (*N* violates the criteria -> *N* is an extremum of the set of deltas).
> 
> By contradiction:
>
> `1`. assume that a number *N* does not violate criteria `A`, `B`, or `C`.
>
> `2`. assume that *N* violates criterion `D`. 
>
> `3`. 0 < |*N*| <= 3 (`1`, `2`)
>
> `4`. (*N* is an extremum -> all other numbers are between 0 and *N*, inclusive)
>
> `5`. *N* is an extremum -> *N* cannot be of opposite sign than the other numbers (`4`)
>
> `6`. contradiction (`2`, `5`) if *N* extremum
> 
> ---
>
> `1`. assume that a number *N* does not violate criteria `A`, `B`, or `C`.
>
> `2`. assume that *N* violates criterion `D`.
>
> `3`. assume that *N* is not an extremum
>
> ~~`4`. there exists a number *M* such that *M* = 0 or *M* > *N* (`1`, `3`)~~
>
> ~~`5`. if *M* = 0, there exists a number *L* s.t. |*L*| > |*N*| (`3`, `4`)~~
>
> ~~`4`. assume that all numbers N are between 0 and *N* (not inclusive)~~
> 
> show (or prove false the contention) that if there is only one violation, it is an extremum
> 
> you have to recalculate the deltas! it's not about removing one *delta*, it's about removing one *value*
