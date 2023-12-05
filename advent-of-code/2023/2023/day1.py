##########################################
## Task
##########################################
# --- Day 1: Trebuchet?! ---
# Something is wrong with global snow production, and you've been selected to take a look. The Elves have even given you a map; on it, they've used stars to mark the top fifty locations that are likely to be having problems.

# You've been doing this long enough to know that to restore snow operations, you need to check all fifty stars by December 25th.

# Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

# You try to ask why they can't just use a weather machine ("not powerful enough") and where they're even sending you ("the sky") and why your map looks mostly blank ("you sure ask a lot of questions") and hang on did you just say the sky ("of course, where do you think snow comes from") when you realize that the Elves are already loading you into a trebuchet ("please hold still, we need to strap you in").

# As they're making the final adjustments, they discover that their calibration document (your puzzle input) has been amended by a very young Elf who was apparently just excited to show off her art skills. Consequently, the Elves are having trouble reading the values on the document.

# The newly-improved calibration document consists of lines of text; each line originally contained a specific calibration value that the Elves now need to recover. On each line, the calibration value can be found by combining the first digit and the last digit (in that order) to form a single two-digit number.

# For example:

# 1abc2
# pqr3stu8vwx
# a1b2c3d4e5f
# treb7uchet
# In this example, the calibration values of these four lines are 12, 38, 15, and 77. Adding these together produces 142.

# Consider your entire calibration document. What is the sum of all of the calibration values?

##########################################
## Implementation
##########################################
def part1():
    with open('2023/day1-p1.input') as f:
        lines = f.readlines()

    sum = 0
    for line in lines:
        numbers = [int(x) for x in line if x.isdigit()]

        if len(numbers) > 0:
            answer = numbers[0].__str__()
        if len(numbers) > 1:
            answer += numbers[-1].__str__()
        else:
            answer += numbers[0].__str__()

        sum += int(answer)
        print(f"Line: {line} - Numbers: {numbers}. Choosing {answer} - Sum: {sum}")
    print(f"Sum: {sum}")

##########################################
## Task
##########################################
# Your calculation isn't quite right. It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".

# Equipped with this new information, you now need to find the real first and last digit on each line. For example:

# two1nine
# eightwothree
# abcone2threexyz
# xtwone3four
# 4nineeightseven2
# zoneight234
# 7pqrstsixteen
# In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76. Adding these together produces 281.

# What is the sum of all of the calibration values?

##########################################
## Implementation
##########################################
def part2():
    with open('2023/day1-p2.input') as f:
        lines = f.readlines()

    # list of strings
    numbers = [ "1", "2", "3", "4", "5", "6", "7", "8", "9" ]
    numbers_as_str = [ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" ]
    all_numbers = numbers + numbers_as_str

    # lines = [line for line in lines if 5 < len(line) < 10 ]
    total_sum = 0
    # Go through each line
    for line in lines:
        matrix = []
        line = line.strip()
        print(f"Line: {line}")
        # Look at the numnbers
        for num in all_numbers:

            # If number (digit or string) is inside string
            # we will build a little "matrix" of which number appeared where
            # and then we will look at the index of the number in the line
            if num in line:
                number_found = all_numbers[all_numbers.index(num)]
                index_at = line.index(num)

                if number_found in numbers:
                    number_found_as_digit = number_found
                else:
                    number_found_as_digit = numbers[numbers_as_str.index(number_found)]

                matrix += [ [index_at, number_found_as_digit] ]
                # print(f"Line: {line} - Found: {number_found} (or {number_found_as_digit}) - Index: {index_at}")
        
        # print(f"Matrix: {matrix}")
        # Sort the matrix by the index
        matrix.sort(key=lambda x: x[0])
        # print(f"Sorted Matrix: {matrix}")

        matrix_sum =matrix_to_sum(matrix);
        print(f"Matrix Sum: {matrix_sum} -- Total Sum: {total_sum}")
        total_sum += matrix_sum
    
    print(f"Total Sum: {total_sum}")


def matrix_to_sum(matrix):
    """
    Kind of like day1 implementation ,but uses a matrix to calculate the sum
    """
    sum = 0
    if len(matrix) > 0:
        answer = matrix[0][1]
    if len(matrix) > 1:
        answer += matrix[-1][1]
    else:
        answer += matrix[0][1].__str__()
    sum += int(answer)
    return sum

if __name__ == "__main__":
    print("Day 1: Trebuchet?!")
    # part1()
    part2()