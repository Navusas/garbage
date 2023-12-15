fn main() {
    let answer = add(10, 20);
    println!("{} is your answer!", answer);


    answer = 20;
    println!("Hello, my new answer is: {}", answer);

    answer = 40;
    println!("Hello, my new answer is: {}", answer);

    println!("5-19 is {}", remove(5,19));
}

fn add(x: i32, y: i32) -> i32 {
    return x + y;
}
fn remove(x: i32, y: i32) -> i32 {
    return x - y;
}