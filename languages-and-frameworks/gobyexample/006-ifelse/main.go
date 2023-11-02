package main

import "fmt"

func main() {
	var someInteger=7;

	if someInteger < 5 {
		fmt.Println("Less than 5")
	} else if someInteger < 10 {
		fmt.Println("Less than 10")
	} else {
		fmt.Println("Whatever")
	}

	someAnotherFunction()

	someNumber := 24
	returnedVal := returnBasedOnInput(someNumber)
	fmt.Println(someNumber, " is", returnedVal)
}


func someAnotherFunction() {
	if number:=10; number < 10 {
		fmt.Println("Again less than 10")
	} else if (number < 20) {
		fmt.Println("Oh, something different. This time the number is ", number)
	} else  {
		fmt.Println("actually no, the number is ", number)
	}
}


func returnBasedOnInput(someNumber int) string {
	if someNumber < 10 {
		return fmt.Sprint("Again less than 10")
	} else if (someNumber < 20) {
		return fmt.Sprint("Oh, something different. This time the number is ", someNumber)
	} else  {
		return fmt.Sprint("actually no, the number is ", someNumber)
	}
}
