package main

import (
	"fmt"
	"gopkg.in/guregu/null.v3"
)

func main() {
	value := null.IntFrom(64000)
	fmt.Println(value)
}