using System;

namespace NumberTriangle
{
    class Program
    {
        //To check is a number is even or not - returns true if even and false if odd
        public static bool isEven(int x)
        {
            if (x % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Main(string[] args)
        {
            //Change array size to match the input (size and size +1 - the size +1 to add an extra 0 to allow the maxNum function to work in code below)
            int[,] oList = new int[15, 16];
            //Input the values as a string
            string input = @"215
                            192 124
                            117 269 442
                            218 836 347 235
                            320 805 522 417 345
                            229 601 728 835 133 124
                            248 202 277 433 207 263 257
                            359 464 504 528 516 716 871 182
                            461 441 426 656 863 560 380 171 923
                            381 348 573 533 448 632 387 176 975 449
                            223 711 445 645 245 543 931 532 937 541 444
                            330 131 333 928 376 733 017 778 839 168 197 197
                            131 171 522 137 217 224 291 413 528 520 227 229 928
                            223 626 034 683 839 052 627 310 713 999 629 817 410 121
                            924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
            //Convert the string into an array
            //Creates an array of each row
            var charArray = input.Split('\n');
            //Splits the columns creating a 2D array
            for (int i = 0; i < charArray.Length; i++)
            {
                var numArr = charArray[i].Trim().Split(' ');
                for (int j = 0; j < numArr.Length; j++)
                {
                    int number = Convert.ToInt32(numArr[j]);
                    oList[i, j] = number;
                }
            }

            //clone the array so we have 2 lists of the numbers for the original list (oList) to calculate the previous numbers
            //The new array is a 2D array to add the cumulative numbers into if the path works.
            int[,] list = oList.Clone() as int[,];

            //loop through each of the "row" in the array
            //-2 as we want to start on the second from bottom
            for (int i = list.GetLength(0) - 2; i >= 0; i--)
            {
                //loop through each of the "columns" in each column of the array
                for (int j = 0; j < list.GetLength(0); j++)
                {
                    //Get the previous numbers
                    int prevNumA = oList[i + 1, j];
                    int prevNumB = oList[i + 1, j + 1];

                    //The new maximum number
                    int maxNum = Math.Max(list[i, j] + list[i + 1, j], list[i, j] + list[i + 1, j + 1]);

                    int getFinNum()
                    {
                        //ignores the 0s (so will not work correctly if the input has 0's in it)
                        if (list[i, j] != 0)
                        {
                            //Checks that the number isn't a failed path
                            if (prevNumA != -9999999 || prevNumB != -9999999)
                            {
                                //checks if the number we are comparing is even - if true then we only want to add odd numbers to the total
                                if (isEven(list[i, j]))
                                {
                                    //returns -9999999 if all are even
                                    if (isEven(prevNumA) && isEven(prevNumB))
                                    {
                                        return -9999999;
                                    }
                                    //Checks if prevNumA is odd and prevNumB is even
                                    else if (!isEven(prevNumA) && isEven(prevNumB))
                                    {
                                        //returns the total from the previous row (directly below) in the array + the current number to get a new total
                                        return list[i, j] + list[i + 1, j];
                                    }
                                    //Checks if prevNumA is even and prevNumB is odd
                                    else if (isEven(prevNumA) && !isEven(prevNumB))
                                    {
                                        //returns the total from the previous row (one to the right) in the array + the current number to get a new total
                                        return list[i, j] + list[i + 1, j + 1];
                                    }
                                    //Checks if both numbers are odd
                                    else if (!isEven(prevNumA) && !isEven(prevNumB))
                                    {
                                        //returns the max of the two numbers if both are a viable path
                                        return maxNum;
                                    }
                                    else
                                    {
                                        return -9999999;
                                    }
                                }
                                //checks if the number we are comparing is odd - if true then we only want to add even numbers to the total
                                else
                                {
                                    //returns -9999999 if all are odd
                                    if (!isEven(prevNumA) && !isEven(prevNumB))
                                    {
                                        return -9999999;
                                    }
                                    //Checks if prevNumA is even and prevNumB is odd
                                    else if (isEven(prevNumA) && !isEven(prevNumB))
                                    {
                                        //returns the total from the previous row (directly below) in the array + the current number to get a new total
                                        return list[i, j] + list[i + 1, j];
                                    }
                                    //Checks if prevNumA is odd and prevNumB is even
                                    else if (!isEven(prevNumA) && isEven(prevNumB))
                                    {
                                        //returns the total from the previous row (one to the right) in the array + the current number to get a new total
                                        return list[i, j] + list[i + 1, j + 1];
                                    }
                                    //Checks if both numbers are even
                                    else if (isEven(prevNumA) && isEven(prevNumB))
                                    {
                                        //returns the max of the two numbers if both are a viable path
                                        return maxNum;
                                    }
                                    else
                                    {
                                        return -9999999;
                                    }
                                }

                            }
                            //set nodes on incorrect paths to -9999999 (e.g. if the two child nodes are -9999999)
                            return -9999999;
                        }
                        return 0;
                    }
                    //to get the final number to be printed to the screen                    
                    list[i, j] = getFinNum();
                }
            }
            //Print the final number on the screen (as the totals were added from bottom up into the array the number in the 1st position should be the maximum number)
            Console.WriteLine(list[0, 0]);
        }
    }
}
