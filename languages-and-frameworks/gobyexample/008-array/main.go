package main

import "fmt"

func main() {
	fmt.Println("Calling 'emptyArray' func...")
	emptyArray()

	fmt.Println("\nCalling 'getSetLen' func...")
	getSetLen()

	fmt.Println("\nCalling 'declareAndInitArray' func...")
	declareAndInitArray()

	fmt.Println("\nCalling 'twoDimensionalArrray' func...")
	twoDimensionalArrray()
}

func emptyArray() {
	var empty [5]int
	fmt.Println("Empty array: ", empty)
}

func getSetLen()  {
	var array [5]int
	fmt.Println("Init: ", array)


	array[4] = 200
	fmt.Println("Set index 4 to 200 ", array)
	fmt.Println("Only index 4: ", array[4])
	fmt.Println("Length: ", len(array))
}

func declareAndInitArray() {
	array := [5]int{1,2,3,4,5}
	fmt.Println("Declared array: ", array)
}

func twoDimensionalArrray() {
	var twoD [2][3]int
	for i:=0; i<2; i++ {
		for j:=0; j<3; j++ {
			twoD[i][j] = i+j
		}
	}
	fmt.Println("2d: ", twoD)
}