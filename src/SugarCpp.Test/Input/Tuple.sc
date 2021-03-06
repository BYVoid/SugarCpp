﻿import "stdio.h"
       "tuple"

using std::tuple

tuple<T, T> sort<T>(a: T, b: T)
    return a < b ? (a, b) : (b, a)

int main()
    a := 10
    b := 1
    (a, b) = sort(a, b)
    printf("%d %d\n", a, b)
    (a, b) = (b, a)
    printf("%d %d\n", a, b)