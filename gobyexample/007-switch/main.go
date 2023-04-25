package main

import (
	"fmt"
	"time"
)

func main() {
	fmt.Print("Simple switch result: ")
	simpleSwitch()

	fmt.Print("Multiple Expression switch result: ")
	multipleExpressionSwitch()

	fmt.Print("Switch statement with no expression result: ")
	expressionlessSwitch()

	fmt.Println("Type switch")
	typeSwitch()
}

func simpleSwitch() {
	n := 2
	switch n {
	case 1:
		fmt.Println("one")
	case 2:
		fmt.Println("two")
	case 3:
		fmt.Println("three")
	case 4:
		fmt.Println("four")
	}
}

func multipleExpressionSwitch() {
	switch time.Now().Weekday() {
	case time.Saturday, time.Sunday:
		fmt.Println("This is weekend")
	case time.Friday:
		fmt.Println("Very close!!")
	default:
		fmt.Println("Hold your horses! No brew for you today, Mister")
	}
}

func expressionlessSwitch() {
	t := time.Now().Add(time.Hour * 24).Day()
	switch {
	case time.Now().Add(time.Second * 3600 * 24).Day() == t:
		fmt.Println("This is the most random equation I could come up with and it works")
	default:
		fmt.Println("Or maybe it doesn't...")
	}
}

func typeSwitch() {
	whatAmI := func(i interface{}) {
		switch i.(type) {
		case bool:
			fmt.Println("I am a bool")
		case int:
			fmt.Println("I am an int")
		default:
			fmt.Printf("No idea what type  %T is \n", i)
		}
	}

	whatAmI(true)
	whatAmI(1)
	whatAmI("adadsd")
}